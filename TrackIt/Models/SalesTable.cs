using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class SalesTable
{
    public int Id { get; set; }

    public DateTime SalesDate { get; set; }

    public int Quantity { get; set; }

    public int Rate { get; set; }

    public string ClientId { get; set; } = null!;

    public string? ClinentId { get; set; }

    public int ProductId { get; set; }

    public virtual AspNetUser? Clinent { get; set; }

    public virtual ProductTable Product { get; set; } = null!;

    public virtual ICollection<StockTable> StockTables { get; set; } = new List<StockTable>();
}
