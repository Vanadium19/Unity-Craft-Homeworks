using UnityEngine;

namespace Modules.Entities
{
    //Don't modify
    [CreateAssetMenu(
        fileName = "EntityCatalog",
        menuName = "Modules/Entities/New Entity Catalog"
    )]
    public sealed class EntityCatalog : ScriptableObject
    {
        [SerializeField]
        private EntityConfig[] _configs;
        
        public bool FindConfig(string name, out EntityConfig config)
        {
            for (int i = 0, count = _configs.Length; i < count; i++)
            {
                config = _configs[i];
                if (config.Name == name)
                    return true;
            }

            config = default;
            return false;
        }
    }
}