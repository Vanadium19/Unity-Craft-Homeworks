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
        private readonly EntityWorld _entityWorld;

        public GameSaveLoader(IGameRepository repository, EntityWorld entityWorld)
        {
            _repository = repository;
            _entityWorld = entityWorld;
        }

        private IEnumerable<IGameSerializer> GetSerializers()
        {
            var entities = _entityWorld.GetAll();
            var serializers = new List<IGameSerializer>(entities.Count);

            foreach (var entity in entities)
                serializers.Add(new EntitySerializer(entity));

            return serializers;
        }

        public void Save()
        {
            Debug.Log("Save!");

            var state = new Dictionary<string, string>();

            foreach (var serializer in GetSerializers())
                serializer.Serialize(state);

            _repository.SetState(state);
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}