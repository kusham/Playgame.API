using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Catalog.Service.Controllers
{
    //https://localhost:5001/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemRepository;
        private static int _requestCounter = 0;

        public ItemsController(IRepository<Item> itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        //GET /items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        {
            _requestCounter++;
            Console.WriteLine($"Request counter: {_requestCounter} : Starting....");

            if(_requestCounter <= 2)
            {
                Console.WriteLine($"Request counter: {_requestCounter} : Delaying....");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
            if (_requestCounter <= 4)
            {
                Console.WriteLine($"Request counter: {_requestCounter} : 500 (Internal Sever Error)");
                return StatusCode(500);
            }
            var items = (await _itemRepository.GetAllAsync()).Select(item => item.AsDto());
            Console.WriteLine($"Request counter: {_requestCounter} : 200 (OK)");
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetEntityByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _itemRepository.CreateEntityAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemRepository.GetEntityByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _itemRepository.UpdateEntityAsync(existingItem);

            return NoContent();
        }
        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _itemRepository.GetEntityByIdAsync(id);
            if(existingItem == null)
            {
                return NotFound();
            }

            await _itemRepository.RemoveEntityAsync(id);
            return NoContent();
        }
    }
        
}
