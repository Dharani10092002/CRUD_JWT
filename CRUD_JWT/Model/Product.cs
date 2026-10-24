using System.ComponentModel.DataAnnotations;

namespace CRUD_JWT.Model
{
    public class Product
    {
        
        public int? Product_Id { get; set; }
        [Key]
        public string? Product_Name { get; set; }
        public decimal? Price { get; set; }
    }
}
