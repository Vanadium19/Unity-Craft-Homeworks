using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.SaveLoad;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.Repository
{
    public class GameRepository : IGameRepository
    {
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
            var version = PlayerPrefsManager.GetCurrentVersion();

            json = _encryptor.Encrypt(json);
            version++;

            var remoteSaveStatus = await SaveRemoteVersion(json, version);
            var localSaveStatus = await SaveLocalVersion(json, version);

            return (remoteSaveStatus || localSaveStatus, version);
        }

        public async UniTask<(bool, Dictionary<string, string>)> GetState(int version)
        {
            return PlayerPrefsManager.GetLocalVersion() == version
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

            var state = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(json));

            return (state != null, state);
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetRemoteVersion(int version)
        {
            Dictionary<string, string> state = null;

            (bool status, string state) result = await _client.Load(version);

            if (result.status)
            {
                if (PlayerPrefsManager.GetLocalVersion() < version)
                    SaveLocalVersion(result.state, version).Forget();

                state = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(result.state));
            }

            return (state != null, state);
        }

        private async UniTask<bool> SaveLocalVersion(string json, int version)
        {
            var saveTask = File.WriteAllTextAsync(_filePath, json);

            await saveTask;

            var status = saveTask.IsCompletedSuccessfully;

            if (status)
                PlayerPrefsManager.SetLocalVersion(version);

            return status;
        }

        private async UniTask<bool> SaveRemoteVersion(string json, int version)
        {
            var status = await _client.Save(version, json);

            if (status)
                PlayerPrefsManager.SetRemoteVersion(version);

            return status;
        }

        private async UniTaskVoid ResaveRemoteVersion(int version, string json)
        {
            var hasRemoteVersion = await _client.HasVersion(version);

            if (!hasRemoteVersion)
                SaveRemoteVersion(json, version).Forget();
        }
    }
}