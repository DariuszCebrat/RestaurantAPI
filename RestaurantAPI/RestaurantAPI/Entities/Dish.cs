using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Entities
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal  Price { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
