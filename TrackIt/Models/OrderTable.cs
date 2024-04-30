using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class OrderTable
{
    public int Id { get; set; }

    public DateTime Arival { get; set; }

    public int Quantity { get; set; }

    public int Rate { get; set; }

    public int VendorId { get; set; }

    public int ProductId { get; set; }

    public virtual ProductTable Product { get; set; } = null!;

    public virtual ICollection<StockTable> StockTables { get; set; } = new List<StockTable>();

    public virtual VendorTable Vendor { get; set; } = null!;
}
