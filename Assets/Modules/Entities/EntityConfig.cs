using UnityEngine;

namespace Modules.Entities
{
    //Don't modify
    [CreateAssetMenu(
        fileName = "EntityConfig",
        menuName = "Modules/Entities/New Entity Config"
    )]
    public sealed class EntityConfig : ScriptableObject
    {
        [field: SerializeField]
        internal Entity Prefab { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public float Size { get; private set; }

        [field: SerializeField]
        public EntityType Type { get; private set; }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            this.Name = Prefab ? Prefab.name : string.Empty;
            if (Prefab) Prefab._config = this;
        }
#endif
    }
}