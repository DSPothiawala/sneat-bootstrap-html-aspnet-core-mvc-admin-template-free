﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Models;

public partial class CoreDataContext : DbContext
{
    public CoreDataContext(DbContextOptions<CoreDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserName, "IX_Users").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AndroidDevice).IsUnicode(false);
            entity.Property(e => e.AndroidId)
                .IsUnicode(false)
                .HasColumnName("AndroidID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Creatdate).HasColumnType("datetime");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsAutoSignIn).HasDefaultValue(false);
            entity.Property(e => e.IsBiometric).HasDefaultValue(false);
            entity.Property(e => e.IsScreenShot).HasDefaultValue(false);
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pin).HasColumnName("PIN");
            entity.Property(e => e.SpersonId).HasColumnName("SPersonID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}