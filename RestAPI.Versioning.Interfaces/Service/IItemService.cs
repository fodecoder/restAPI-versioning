using RestAPI.Versioning.Interfaces.Model;

namespace RestAPI.Versioning.Interfaces.Service
{
    public interface IItemService
    {
        // CRUD operations
        public Task CreateItemAsync( Item element );
        public Task<Item> ReadItemAsync( Guid id );
        public Task UpdateItemAsync( Item element );
        public Task DeleteItemAsync( Guid id );

        // List
        public Task<IEnumerable<Item>> GetItemsAsync( string name , int limit );
    }
}
