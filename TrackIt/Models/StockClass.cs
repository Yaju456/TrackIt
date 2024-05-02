﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackIt.Models
{
    public class StockClass
    {
        [Key]
        public int Id { get; set; }

        public string? serial_number { get; set; }

         [Required]
        public int Order_id { get; set; }
        [ValidateNever]
        [ForeignKey("Order_id")]
        public OrderClass? Order { get; set; }
        public int? Customer_id { get; set; }
        [ValidateNever]
        [ForeignKey("Customer_id")]
        public CustomerClass? Customer { get; set; }
        [Required]
        [StringLength(1)]
        public string? InStock { get; set; }

        [Required]
        public int Product_id { get; set; }
        [ValidateNever]
        [ForeignKey("Product_id")]
        public ProductClass? Product { get; set; }
    }
}
