using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    
    public class RestaurantController : ControllerBase
    {
        private RestaurantDbContext _db;
        public RestaurantController(RestaurantDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _db.Restaurants.ToList();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute]int id)
        {
            var restaurant = _db.Restaurants.FirstOrDefault(x=>x.Id == id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

    }
}
