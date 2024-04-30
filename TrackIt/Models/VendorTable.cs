using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class VendorTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
}
