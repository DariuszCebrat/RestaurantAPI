using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _db;
        private readonly IMapper _mapper;
      

        public RestaurantService(RestaurantDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
     
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _db.Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .FirstOrDefault(x => x.Id == id);
            if (restaurant == null) return null;
            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _db.Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;

        }
        public async Task<int> Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync();
            return restaurant.Id;
        }
    }
}
