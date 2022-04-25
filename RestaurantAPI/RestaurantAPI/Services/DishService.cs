using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        Task<int> Create(int restaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int DishId);
        List<DishDto> GetAll(int restaurantId);
        Task RemoveAll(int restaurantId);
        Task RemoveById(int restaurantId, int dishId);
    }

    public class DishService : IDishService
    {
        private RestaurantDbContext _db;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public DishDto GetById(int restaurantId,int DishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _db.Dishes.FirstOrDefault(x => x.Id == DishId);
            if (dish is null || dish.RestaurantId != restaurantId) throw new NotFoundException("Dish not found");

            var dishDto = _mapper.Map<DishDto>(dish);

            return   dishDto;

        }
        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishDto = _mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDto;
        }

        public async Task<int> Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            await _db.Dishes.AddAsync(dishEntity);
            await _db.SaveChangesAsync();
            return dishEntity.Id;


        }
        public async Task RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            _db.Dishes.RemoveRange(restaurant.Dishes);
            await _db.SaveChangesAsync();
        }
        public async Task RemoveById(int restaurantId,int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
           var dishToDelete = restaurant.Dishes.FirstOrDefault(x => x.Id == dishId);
            if (dishToDelete is null) throw new NotFoundException("Dish not found");
            _db.Dishes.Remove(dishToDelete);
            await _db.SaveChangesAsync();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _db.Restaurants.Include(r => r.Dishes).FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null) throw new NotFoundException("restaurant not found");
            return restaurant;
        }
    }
}
