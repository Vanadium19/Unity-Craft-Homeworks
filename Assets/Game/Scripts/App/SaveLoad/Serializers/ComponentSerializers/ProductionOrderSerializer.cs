using System.Linq;
using System.Runtime.Serialization;
using Modules.Entities;
using SampleGame.App.Data;
using SampleGame.Gameplay;
using Zenject;

namespace SampleGame.App.SaveLoad.Serializers
{
    public class ProductionOrderSerializer : ComponentSerializer<ProductionOrder, ProductionOrderData>
    {
        [Inject]
        private EntityCatalog _catalog;

        protected override ProductionOrderData Serialize(ProductionOrder service)
        {
            return new ProductionOrderData { ConfigNames = service.Queue.Select(config => config.Name).ToArray() };
        }

        protected override void Deserialize(ProductionOrder service, ProductionOrderData data)
        {
            EntityConfig[] configs = new EntityConfig[data.ConfigNames.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                if (!_catalog.FindConfig(data.ConfigNames[i], out EntityConfig config))
                    throw new SerializationException();

                configs[i] = config;
            }

            service.Queue = configs;
        }
    }
}