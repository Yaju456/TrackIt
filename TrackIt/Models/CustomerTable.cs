using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class CustomerTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Province { get; set; }

    public string? District { get; set; }

    public string? GauPalikaorMunicipality { get; set; }

    public virtual ICollection<StockTable> StockTables { get; set; } = new List<StockTable>();
}
