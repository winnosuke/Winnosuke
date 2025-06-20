﻿using System;
using System.Collections.Generic;

namespace Winnosuke.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? GarageId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Garage? Garage { get; set; }

    public virtual User? User { get; set; }
}
