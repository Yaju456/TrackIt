using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class ProductTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Company { get; set; } = null!;

    public int? InStock { get; set; }

    public string? ImgUrl { get; set; }

    public string Modal { get; set; } = null!;

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual ICollection<SalesTable> SalesTables { get; set; } = new List<SalesTable>();

    public virtual ICollection<StockTable> StockTables { get; set; } = new List<StockTable>();
}
