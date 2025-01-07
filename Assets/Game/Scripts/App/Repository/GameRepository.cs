using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.Repository
{
    public class GameRepository : IGameRepository
    {
        private const string LocalVersion = "LocalVersion";
        private const string RemoteVersion = "RemoteVersion";

        private readonly string _filePath;
        private readonly GameClient _client;
        private readonly Encryptor _encryptor;

        public GameRepository(GameClient client, Encryptor encryptor, string filePath)
        {
            _client = client;
            _encryptor = encryptor;
            _filePath = filePath;
        }

        public async UniTask<(bool, int)> SetState(Dictionary<string, string> state)
        {
            var json = JsonConvert.SerializeObject(state);
            var version = Mathf.Max(PlayerPrefs.GetInt(LocalVersion), PlayerPrefs.GetInt(RemoteVersion));

            json = _encryptor.Encrypt(json);
            version++;

            var remoteSaveStatus = await SaveRemoteVersion(json, version);
            var localSaveStatus = await SaveLocalVersion(json, version);

            return (remoteSaveStatus || localSaveStatus, version);
        }

        public async UniTask<(bool, Dictionary<string, string>)> GetState(int version)
        {
            return PlayerPrefs.GetInt(LocalVersion) == version
                ? await GetLocalVersion(version)
                : await GetRemoteVersion(version);
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetLocalVersion(int version)
        {
            var loadTask = File.ReadAllTextAsync(_filePath);
            var json = await loadTask;

            if (loadTask.IsCompletedSuccessfully)
                ResaveRemoteVersion(version, json).Forget();
            else
                return await GetRemoteVersion(version);

            return (true, JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(json)));
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetRemoteVersion(int version)
        {
            Dictionary<string, string> state = null;

            (bool status, string state) result = await _client.Load(version);

            if (result.status)
            {
                if (PlayerPrefs.GetInt(LocalVersion) < version)
                    SaveLocalVersion(result.state, version).Forget();

                state = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(result.state));
            }

            return (result.status, state);
        }

        private async UniTask<bool> SaveRemoteVersion(string json, int version)
        {
            var status = await _client.Save(version, json);

            if (status)
            {
                PlayerPrefs.SetInt(RemoteVersion, version);
                PlayerPrefs.Save();
            }

            return status;
        }

        private async UniTaskVoid ResaveRemoteVersion(int version, string json)
        {
            var hasRemoteVersion = await _client.HasVersion(version);

            if (!hasRemoteVersion)
                SaveRemoteVersion(json, version).Forget();
        }

        private async UniTask<bool> SaveLocalVersion(string json, int version)
        {
            var saveTask = File.WriteAllTextAsync(_filePath, json);

            await saveTask;

            var status = saveTask.IsCompletedSuccessfully;

            if (status)
            {
                PlayerPrefs.SetInt(LocalVersion, version);
                PlayerPrefs.Save();
            }

            return status;
        }
    }
}