using MassTransit;
using Play.Catalog.Constracts;
using Play.Common;
using Play.Inventory.Service.Entities;
using System.Threading.Tasks;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository) 
        {
            this._repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;
            var item = await _repository.GetEntityByIdAsync(message.ItemId);

            if(item == null)
            {
            item = new CatalogItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };
            await _repository.CreateEntityAsync(item);
            }
            else
            {
                item.Name = message.Name;
                item.Description = message.Description;

                await _repository.UpdateEntityAsync(item);
            }
        }
    }
}
