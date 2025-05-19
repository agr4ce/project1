using System;
using System.Collections.Generic;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStarts.Data;

public partial class OrderContext : DbContext
{
    public OrderContext()
    {
    }

    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MonthOrder> MonthOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer($"data source={Properties.Settings.Default.ServerName};initial catalog = {Properties.Settings.Default.BDName};persist security info=True;user id={Properties.Settings.Default.BDUserLogin};password={Properties.Settings.Default.Password};TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MonthOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MonthOrders");

            entity.Property(e => e.AdditionGamesPrice).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.BoardGamesPrice).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.Buyer).HasMaxLength(182);
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.OrderPrice).HasColumnType("decimal(38, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
