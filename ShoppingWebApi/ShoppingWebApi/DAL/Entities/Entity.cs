using System.ComponentModel.DataAnnotations;

namespace ShoppingWebApi.DAL.Entities
{
    public class Entity
    {
        [Key]//DataNotations:sirve para "decorar la propiedad"

        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
