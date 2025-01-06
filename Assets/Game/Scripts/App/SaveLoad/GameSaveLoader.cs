using System;
using System.Collections.Generic;
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

        public void Save()
        {
            Debug.Log("Save!");

            var state = new Dictionary<string, string>();

            foreach (var serializer in _serializers)
                serializer.Serialize(state);

            _repository.SetState(state);
        }

        public void Load()
        {
            Debug.Log("Load!");
            
            var state = _repository.GetState();

            foreach (var serializer in _serializers)
                serializer.Deserialize(state);
        }
    }
}