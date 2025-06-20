﻿using System;
using System.Collections.Generic;

namespace Winnosuke.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? image_url { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Garage> Garages { get; set; } = new List<Garage>();
}
