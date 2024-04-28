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

        [Required(ErrorMessage ="Quantity is requried")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage ="Rate is requierd")]
        public int? Rate{ get; set; }

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
