using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody]UpdateRestaurantDto dto,[FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var isUpdated =  _restaurantService.Update(id,dto).Result;
            if (isUpdated) return Ok() ;
            else return NotFound();
           
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
          var isDeleted =   _restaurantService.Delete(id).Result;

            if (isDeleted) return NoContent();
            else return NotFound();
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          var id =   _restaurantService.Create(dto).Result;

            return Created($"/api/restaurant/{id}",null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
           
            return Ok(_restaurantService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);
           
            if (restaurant is null)
            {
                return NotFound();
            }
         
            return Ok(restaurant);
        }



    }
}
