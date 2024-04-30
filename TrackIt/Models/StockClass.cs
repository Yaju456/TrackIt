using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackIt.Models
{
    public class StockClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? serial_number { get; set; }

        [Required]
        public int Order_id { get; set; }
        [ValidateNever]
        [ForeignKey("Order_id")]
        public OrderClass? Order { get; set; }

        public int? Client_id { get; set; }
        [ValidateNever]
        [ForeignKey("Client_id")]
        public ClinetClass? Client { get; set;}

        [Required]
        [StringLength(1)]
        public string? InStock { get; set; }

        [Required]
        public int Product_id{ get; set; }
        [ValidateNever]
        [ForeignKey("Product_id")]
        public ProductClass? Product { get; set;}

        public int? Sales_id { get; set; }
        [ValidateNever]
        [ForeignKey("Sales_id")]
        public SalesClass? Sales { get; set; }
    }
}
