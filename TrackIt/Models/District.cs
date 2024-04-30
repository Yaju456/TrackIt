using System;
using System.Collections.Generic;

namespace TrackIt.Models;

public partial class District
{
    public int Id { get; set; }

    public int? ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string NameNp { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public int? Imucode { get; set; }

    public virtual ICollection<LocalBody> LocalBodies { get; set; } = new List<LocalBody>();
}
