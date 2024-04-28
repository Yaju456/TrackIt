using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackIt.Models;

namespace TrackIt.ViewModel
{
    public class OrderVM
    {
        public OrderClass? myorder { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? Seller { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? Product { get; set; }
    }
}
