using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext db, IMapper mapper,ILogger<RestaurantService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Update(int id,UpdateRestaurantDto dto)
        {
            var restaurantToEdit = _db.Restaurants.FirstOrDefault(x=>x.Id == id);
            if (restaurantToEdit == null) throw new NotFoundException("restaurant not found");
            restaurantToEdit.Name = dto.Name;   
            restaurantToEdit.Description = dto.Description; 
            restaurantToEdit.HasDelivery = dto.HasDelivery; 
            await _db.SaveChangesAsync();
       

        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _db.Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .FirstOrDefault(x => x.Id == id);
            if (restaurant == null) throw new NotFoundException("restaurant not found");
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
        public async Task Delete(int id)
        {
            _logger.LogError($"Restaurant with id:{id} DELETE action invoke");
            var restaurant = _db.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is  null) throw new NotFoundException("restaurant not found");
            _db.Restaurants.Remove(restaurant);
           await _db.SaveChangesAsync();
      
        }
    }
}
