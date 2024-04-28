using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackIt.Models;

namespace TrackIt.ViewModel
{
    public class SalesVM
    {
        public SalesClass?  TheirSales{ get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? Client { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? Product { get; set; }
    }
}
