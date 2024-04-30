using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameNp { get; set; }

    public int? Imucode { get; set; }
}
