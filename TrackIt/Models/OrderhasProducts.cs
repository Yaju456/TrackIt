﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackIt.Models
{
    public class OrderhasProducts
    {
        [Key]
        public int Id { get; set; }

        public int Order_id { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(Order_id))]
        public OrderClass? Order {  get; set; }

        public int Product_id { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(Product_id))]
        public ProductClass? Product { get; set; }
        public int Quantity { get; set; }
    }
}
