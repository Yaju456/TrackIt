using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrackIt.Models
{
    public class SalesClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Sales_Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public int Client_id { get; set; }

        [ValidateNever]
        [ForeignKey("Clinent_id")]
        public ClinetClass? Clinet { get; set; }

        [Required]
        public int Product_id { get; set; }
        [ValidateNever]
        [ForeignKey("Product_id")]
        public ProductClass? Product { get; set; }
    }
}
