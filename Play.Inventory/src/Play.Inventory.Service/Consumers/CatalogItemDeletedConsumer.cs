using MassTransit;
using Play.Catalog.Constracts;
using Play.Common;
using Play.Inventory.Service.Entities;
using System.Threading.Tasks;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemDeletedConsumer(IRepository<CatalogItem> repository) 
        {
            this._repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
        {
            var message = context.Message;
            var item = await _repository.GetEntityByIdAsync(message.ItemId);

            if(item != null)
            {
                return;
            }

            await _repository.RemoveEntityAsync(message.ItemId);
        }
    }
}
