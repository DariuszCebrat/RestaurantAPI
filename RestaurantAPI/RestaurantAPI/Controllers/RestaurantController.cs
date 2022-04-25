using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;


namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    
    public class RestaurantController : ControllerBase
    {
        private RestaurantDbContext _db;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurant = _mapper.Map<Restaurant>(dto);
           await _db.Restaurants.AddAsync(restaurant);
            await _db.SaveChangesAsync();

            return Created($"/api/restaurant/{restaurant.Id}",null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _db.Restaurants
                .Include(x=>x.Address)
                .Include(x=>x.Dishes)
                .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurant = _db.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x=>x.Id == id);
            if (restaurant is null)
            {
                return NotFound();
            }
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
        }



    }
}
