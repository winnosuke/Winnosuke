using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Winnosuke.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;

    [StringLength(255)]
    public string EmailConfirmationToken { get; set; }

    public virtual ICollection<AdminActivity> AdminActivities { get; set; } = new List<AdminActivity>();

    public virtual ICollection<Appointment> AppointmentTechnicians { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentUsers { get; set; } = new List<Appointment>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<TechnicalReport> TechnicalReports { get; set; } = new List<TechnicalReport>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
