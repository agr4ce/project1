using System;
using System.Collections.Generic;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStarts.Data;

public partial class UserContext : DbContext
{
    public UserContext()
    {
    }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer($"data source={Properties.Settings.Default.ServerName};initial catalog = {Properties.Settings.Default.BDName};persist security info=True;user id={Properties.Settings.Default.BDUserLogin};password={Properties.Settings.Default.Password};TrustServerCertificate=True");
    //=> optionsBuilder.UseSqlServer("data source=DESKTOP-3R89RCL;initial catalog = ProjectKr;persist security info=True;user id=FVST;password=174321;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.IdRoleUser);

            entity.ToTable("RoleUser");

            entity.HasIndex(e => e.NameRoleUser, "UQ_RoleUser").IsUnique();

            entity.Property(e => e.IdRoleUser).HasColumnName("idRoleUser");
            entity.Property(e => e.NameRoleUser).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login);

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ_UserLogin").IsUnique();

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(30);

            entity.HasOne(d => d.IdRoleUserNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRoleUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_RoleUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
