using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace SampleGame.App.Repository
{
    public class GameClient
    {
        private const string SaveMethod = "save?version=";
        private const string LoadMethod = "load?version=";
        private const int SuccessStatus = 200;

        private readonly string _uri;

        public GameClient(string uri)
        {
            _uri = uri;
        }

        public async UniTask<bool> Save(int version, string json)
        {
            UnityWebRequest request = UnityWebRequest.Put($"{_uri}/{SaveMethod}{version}", json);

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
            UnityWebRequest request = UnityWebRequest.Get($"{_uri}/{LoadMethod}{version}");

            try
            {
                await request.SendWebRequest();
            }
            catch
            {
                return (false, null);
            }

            if (request.responseCode != SuccessStatus)
                return (false, null);

            var json = request.downloadHandler.text;

            return (json != null, json);
        }
    }
}