using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class LocalBody
{
    public int Id { get; set; }

    public int DistrictId { get; set; }

    public string Name { get; set; } = null!;

    public string NameNp { get; set; } = null!;

    public bool IsMunicipality { get; set; }

    public int DisplayOrder { get; set; }

    public int? Imucode { get; set; }

    public virtual District District { get; set; } = null!;
}
