﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackIt.Models
{
    public class BillClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Customer_id { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(Customer_id))]
        public CustomerClass? Customer { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public double total { get; set; }

        [Required]
        public DateTime? Date {  get; set; } 

        public double? payment { get; set; }
    }
}
