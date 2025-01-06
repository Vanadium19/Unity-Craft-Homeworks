using System.Collections.Generic;
using Game.Scripts.App.Data;
using Modules.Entities;

namespace Game.Scripts.App
{
    public static class EntityExtension
    {
        public static EntityData Serialize(this Entity entity, Dictionary<string, string> components)
        {
            return new EntityData
            {
                Id = entity.Id,
                Name = entity.Name,
                Position = entity.transform.position,
                Rotation = entity.transform.rotation,
                Components = components,
            };
        }
    }
}