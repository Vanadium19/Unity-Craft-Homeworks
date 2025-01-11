using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.App.Repository
{
    public class GameRepository : IGameRepository
    {
        private const string Version = "Version";

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
            var version = PlayerPrefs.GetInt(Version);

            json = _encryptor.Encrypt(json);
            version++;

            var status = await _client.Save(version, json);

            if (status)
                SaveLocalVersion(version, json).Forget();

            return (status, version);
        }

        public async UniTask<(bool, Dictionary<string, string>)> GetState(int version)
        {
            return PlayerPrefs.GetInt(Version) == version
                ? await GetLocalVersion(version)
                : await GetRemoteVersion(version);
        }

        private async UniTaskVoid SaveLocalVersion(int version, string json)
        {
            await File.WriteAllTextAsync(_filePath, json);

            PlayerPrefs.SetInt(Version, version);
            PlayerPrefs.Save();
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetLocalVersion(int version)
        {
            var json = await File.ReadAllTextAsync(_filePath);

            var state = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(json));

            return (state != null, state);
        }

        private async UniTask<(bool, Dictionary<string, string>)> GetRemoteVersion(int version)
        {
            Dictionary<string, string> state = null;
            (bool status, string json) result = await _client.Load(version);

            if (result.status)
                state = JsonConvert.DeserializeObject<Dictionary<string, string>>(_encryptor.Decrypt(result.json));

            return (state != null, state);
        }
    }
}