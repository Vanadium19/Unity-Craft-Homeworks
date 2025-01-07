using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Scripts.App.Repository
{
    public class GameClient
    {
        private const string Url = "http://127.0.0.1:8888";
        private const string SaveMethod = "save?version=";
        private const string LoadMethod = "load?version=";

        public async UniTask<bool> Save(int version, string json)
        {
            UnityWebRequest request = UnityWebRequest.Put($"{Url}/{SaveMethod}{version}", json);

            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                return false;
            }

            return request.result == UnityWebRequest.Result.Success;
        }

        public async UniTask<(bool, string)> Load(int version)
        {
            UnityWebRequest request = UnityWebRequest.Get($"{Url}/{LoadMethod}{version}");

            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                return (false, null);
            }

            if (request.responseCode != 200)
                return (false, null);

            var json = request.downloadHandler.text;

            return json == null ? (false, null) : (true, json);
        }

        public async UniTask<bool> HasVersion(int version)
        {
            UnityWebRequest request = UnityWebRequest.Get($"{Url}/{LoadMethod}{version}");

            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                return false;
            }

            return request.responseCode == 200;
        }
    }
}