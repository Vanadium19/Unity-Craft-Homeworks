using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.Repository;
using Game.Scripts.App.SaveLoad.Serializers;
using Modules.Entities;
using UnityEngine;

namespace Game.Scripts.App.SaveLoad
{
    public class GameSaveLoader : IGameSaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly IGameSerializer[] _serializers;

        public GameSaveLoader(IGameRepository repository, IGameSerializer[] serializers)
        {
            _repository = repository;
            _serializers = serializers;
        }

        public async UniTask<(bool, int)> Save()
        {
            var state = new Dictionary<string, string>();

            foreach (var serializer in _serializers)
                serializer.Serialize(state);

            return await _repository.SetState(state);
        }

        public async UniTask<bool> Load(int version)
        {
            if (version <= 0)
                return false;

            (bool status, Dictionary<string, string> state) result = await _repository.GetState(version);

            if (result.status)
            {
                foreach (var serializer in _serializers)
                {
                    serializer.Deserialize(result.state);
                }
            }

            return result.status;
        }
    }
}