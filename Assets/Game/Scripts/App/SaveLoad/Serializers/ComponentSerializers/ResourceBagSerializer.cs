using SampleGame.App.Data;
using SampleGame.Common;
using SampleGame.Gameplay;

namespace SampleGame.App.SaveLoad.Serializers
{
    public class ResourceBagSerializer : ComponentSerializer<ResourceBag, ResourceBagData>
    {
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
            service.Type = (ResourceType)data.Type;
            service.Current = data.Current;
            service.Capacity = data.Capacity;
        }
    }
}