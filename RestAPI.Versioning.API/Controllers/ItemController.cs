using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Versioning.Interfaces.Model;
using RestAPI.Versioning.Interfaces.Service;

namespace Inventory.API.Controllers
{
    [ApiController]
    [ApiVersion ( 1.0 , Deprecated = true )]
    [ApiVersion ( 2.0 )]
    [Route ( "v{version:apiVersion}/[controller]" )]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        private readonly ILogger<ItemController> _logger;

        public ItemController( ILogger<ItemController> logger , IItemService itemService )
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpPost]
        public async Task CreateItem( [FromBody] Item item )
        {
            _logger.Log ( LogLevel.Debug , $"Created Item: {item}" );
            await _itemService.CreateItemAsync ( item );
        }

        [HttpGet ( "{id}" )]
        public Task<Item> ReadItem( [FromRoute] Guid id )
        {
            _logger.Log ( LogLevel.Debug , $"Requested Item: {id}" );
            return _itemService.ReadItemAsync ( id );
        }

        [HttpGet ( "{id}" ), MapToApiVersion ( "2.0" )]
        public async Task<Item> ReadItem2( [FromRoute] Guid id )
        {
            _logger.Log ( LogLevel.Debug , $"Requested Item: {id}" );
            var retValue = await _itemService.ReadItemAsync ( id );
            retValue.Description += " API version 2.0";
            return retValue;
        }

        [HttpPut]
        public async Task UpdateItem( [FromBody] Item item )
        {
            _logger.Log ( LogLevel.Debug , $"Updated Item: {item}" );
            await _itemService.UpdateItemAsync ( item );
        }

        [HttpDelete ( "{id}" )]
        public async Task DeleteItem( Guid id )
        {
            _logger.Log ( LogLevel.Debug , $"Deleted Item: {id}" );
            await _itemService.DeleteItemAsync ( id );
        }
    }
}
