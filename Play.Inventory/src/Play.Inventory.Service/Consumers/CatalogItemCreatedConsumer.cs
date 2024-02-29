using MassTransit;
using Play.Catalog.Constracts;
using Play.Common;
using Play.Inventory.Service.Entities;
using System.Threading.Tasks;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> repository) 
        {
            this._repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message = context.Message;
            var item = await _repository.GetEntityByIdAsync(message.ItemId);
            if(item != null)
            {
                return;
            }

            item = new CatalogItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };
            await _repository.CreateEntityAsync(item);
        }
    }
}
