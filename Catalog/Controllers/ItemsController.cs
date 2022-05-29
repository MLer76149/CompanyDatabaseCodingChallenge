using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    // GET/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;
        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
            // SQLItemsRepository.OpenConnection();
            // SQLItemsRepository.SqlRead();            
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }
        

        /* [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = SQLItemsRepository.SqlRead();

            return items;
        }*/

        // GET/itmes/id        
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        /*[HttpPost("{Name}, {Price}")]
        public void PostItem(string Name, decimal Price)
        {
            repository.PostItem(Name, Price);
        }*/

        /*[HttpPost("{Name}, {Price}")]
        public void SqlPostItem(string Name, decimal Price)
        {
            SQLItemsRepository.SqlSafe(Name, Price);
        }*/


        // POST / items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            //return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
            return item.AsDto();
        }
    }
}