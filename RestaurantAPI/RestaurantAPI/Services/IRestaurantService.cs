using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        Task<int> Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, UpdateRestaurantDto dto);
    }
}