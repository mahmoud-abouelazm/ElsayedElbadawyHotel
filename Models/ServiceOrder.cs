﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ElSayedHotel.Models;

[PrimaryKey("ServiceId", "ReservationNumber")]
[Table("ServiceOrder")]
public partial class ServiceOrder
{
    [Key]
    public int ServiceId { get; set; }

    [Key]
    public int ReservationNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    public int? Amount { get; set; }

    [ForeignKey("ReservationNumber")]
    [InverseProperty("ServiceOrders")]
    public virtual Reservation ReservationNumberNavigation { get; set; }

    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceOrders")]
    public virtual Service Service { get; set; }
}