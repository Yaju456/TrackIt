using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class StockTable
{
    public int Id { get; set; }

    public string? SerialNumber { get; set; }

    public int OrderId { get; set; }

    public string InStock { get; set; } = null!;

    public int ProductId { get; set; }

    public int? SalesId { get; set; }

    public int? CustomerId { get; set; }

    public virtual CustomerTable? Customer { get; set; }

    public virtual OrderTable Order { get; set; } = null!;

    public virtual ProductTable Product { get; set; } = null!;

    public virtual SalesTable? Sales { get; set; }
}
