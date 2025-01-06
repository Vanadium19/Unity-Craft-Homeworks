using Game.Scripts.App.Data;
using SampleGame.Gameplay;

namespace Game.Scripts.App.SaveLoad.Serializers
{
    public class ResourceBagSerializer : GameSerializer<ResourceBag, ResourceBagData>
    {
        public ResourceBagSerializer(ResourceBag service) : base(service, nameof(ResourceBag))
        {
        }

        protected override ResourceBagData Serialize(ResourceBag service)
        {
            return new ResourceBagData
            {
                Type = (int)service.Type,
                Current = service.Current,
                Capacity = service.Capacity
            };
        }

        protected override void Deserialize(ResourceBag service, ResourceBagData data)
        {
            throw new System.NotImplementedException();
        }
    }
}