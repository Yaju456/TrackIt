using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackIt.Models
{
    public class OrderClass
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Arival Date is required")] 
        public DateTime? Arival{ get; set; }

        [Range(1,100)]
        [Required(ErrorMessage ="Quantity is requried")]
        public int? Quantity { get; set; }

        public int? In_Stock{ get; set; }

        [Required]
        public int vendor_id{ get; set; }

        [ValidateNever]
        [ForeignKey("vendor_id")]
        public VendorClass? vendor { get; set; }

        [Required]
        public int Product_id { get; set; }
        [ValidateNever]
        [ForeignKey("Product_id")]
        public ProductClass? Product { get; set; }
    }
}
