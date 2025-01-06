using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.App.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly string _filePath = $"{Application.streamingAssetsPath}/GameState.txt";
        
        public void SetState(Dictionary<string, string> state)
        {
            string json = JsonConvert.SerializeObject(state);

            // byte[] bytes = Encoding.UTF8.GetBytes(json);
            // byte[] encryptedBytes = AesEncryptor.Encrypt(bytes, _aesPassword, _aesSalt);
            // File.WriteAllBytes(_filePath, encryptedBytes);
            
            File.WriteAllText(_filePath, json);
        }

        public Dictionary<string, string> GetState()
        {
            return new();
        }
    }
}