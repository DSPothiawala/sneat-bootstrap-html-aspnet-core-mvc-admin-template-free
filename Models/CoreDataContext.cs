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

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Controller)
                .HasMaxLength(50)
                .HasColumnName("controller");
            entity.Property(e => e.Moduleid).HasColumnName("moduleid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Namelink).HasColumnName("namelink");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Subnameclass)
                .HasMaxLength(100)
                .HasColumnName("subnameclass");
            entity.Property(e => e.Target).HasColumnName("target");

            entity.HasOne(d => d.Module).WithMany(p => p.Menus)
                .HasForeignKey(d => d.Moduleid)
                .HasConstraintName("FK_menu_module");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("module");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icon)
                .HasMaxLength(200)
                .HasColumnName("icon");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NameLink)
                .HasMaxLength(200)
                .HasColumnName("nameLink");
            entity.Property(e => e.Nameclass)
                .HasMaxLength(200)
                .HasColumnName("nameclass");
            entity.Property(e => e.Namehref).HasColumnName("namehref");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Subname)
                .HasMaxLength(200)
                .HasColumnName("subname");
            entity.Property(e => e.Sumcss)
                .HasMaxLength(200)
                .HasColumnName("sumcss");
            entity.Property(e => e.Target).HasColumnName("target");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserName, "IX_Users").IsUnique();

            entity.HasIndex(e => e.Creatdate, "IX_Users_CreateDate").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Creatdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
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