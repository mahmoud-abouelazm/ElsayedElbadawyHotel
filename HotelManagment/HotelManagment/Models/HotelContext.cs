﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagment.Models;

public partial class HotelContext : DbContext
{
    public HotelContext()
    {
    }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<GuestBookRoom> GuestBookRooms { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceOrder> ServiceOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => new { e.BillNumber, e.BookingId, e.GuestId }).HasName("PK__Bill__108EF3557F97E180");

            entity.Property(e => e.BillNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Booking).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BookingId__571DF1D5");

            entity.HasOne(d => d.Guest).WithMany(p => p.Bills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__GuestId__5812160E");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.NationalNum).HasName("PK__Guest__292B5686C38D3784");

            entity.Property(e => e.NationalNum).ValueGeneratedNever();
        });

        modelBuilder.Entity<GuestBookRoom>(entity =>
        {
            entity.HasKey(e => new { e.GuestId, e.BookingId, e.RoomId }).HasName("PK__GuestBoo__F349EB85E5E5C321");

            entity.Property(e => e.CheckIn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.GuestBookRooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GuestBook__Booki__412EB0B6");

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestBookRooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GuestBook__Guest__403A8C7D");

            entity.HasOne(d => d.Room).WithMany(p => p.GuestBookRooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GuestBook__RoomI__4222D4EF");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Reservat__73951AED5F48487E");

            entity.Property(e => e.Valid).HasDefaultValue(true);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863939FD7F8D8E");

            entity.Property(e => e.Availability).HasDefaultValue(true);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A7B6EA57E");
        });

        modelBuilder.Entity<ServiceOrder>(entity =>
        {
            entity.HasKey(e => new { e.ServiceId, e.BookingId, e.GuestId, e.OrderDate }).HasName("PK__ServiceO__3599CB98DD35991C");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceOr__Booki__48CFD27E");

            entity.HasOne(d => d.Guest).WithMany(p => p.ServiceOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceOr__Guest__49C3F6B7");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceOr__Servi__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}