﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagment.Models;

[Table("Guest")]
public partial class Guest
{
    [Key]
    public int NationalNum { get; set; }

    public long PhoneNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [InverseProperty("Guest")]
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    [InverseProperty("Guest")]
    public virtual ICollection<GuestBookRoom> GuestBookRooms { get; set; } = new List<GuestBookRoom>();

    [InverseProperty("Guest")]
    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}