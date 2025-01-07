using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.Repository
{
    public class GameRepository : IGameRepository
    {
        private const string LocalVersion = "LocalVersion";
        private const string RemoteVersion = "RemoteVersion";

        private readonly string _filePath = $"{Application.streamingAssetsPath}/GameState.txt";
        private readonly GameClient _client;

        public GameRepository(GameClient client)
        {
            _client = client;
        }

        public async UniTask<(bool, int)> SetState(Dictionary<string, string> state)
        {
            var json = JsonConvert.SerializeObject(state);
            var version = Mathf.Max(PlayerPrefs.GetInt(LocalVersion), PlayerPrefs.GetInt(RemoteVersion));

            version++;

            var remoteSaveStatus = await SaveRemoteVersion(json, version);
            var localSaveStatus = await SaveLocalVersion(json, version);

            return (remoteSaveStatus || localSaveStatus, version);

            // byte[] bytes = Encoding.UTF8.GetBytes(json);
            // byte[] encryptedBytes = AesEncryptor.Encrypt(bytes, _aesPassword, _aesSalt);
            // File.WriteAllBytes(_filePath, encryptedBytes);
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
            {
                var hasRemoteVersion = await _client.HasVersion(version);

                if (!hasRemoteVersion)
                    SaveRemoteVersion(json, version).Forget();
            }
            else
            {
                return await GetRemoteVersion(version);
            }

            return (true, JsonConvert.DeserializeObject<Dictionary<string, string>>(json));
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetRemoteVersion(int version)
        {
            Dictionary<string, string> state = null;

            (bool status, string state) result = await _client.Load(version);

            if (result.status)
            {
                if (PlayerPrefs.GetInt(LocalVersion) < version)
                    SaveLocalVersion(result.state, version).Forget();

                state = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.state);
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