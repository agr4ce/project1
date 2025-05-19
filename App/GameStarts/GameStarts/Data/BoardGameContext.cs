using System;
using System.Collections.Generic;
using GameStarts.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStarts.Data;

public partial class BoardGameContext : DbContext
{
    public BoardGameContext()
    {
    }

    public BoardGameContext(DbContextOptions<BoardGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addition> Additions { get; set; }

    public virtual DbSet<BoardGame> BoardGames { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Purshase> Purshases { get; set; }

    public virtual DbSet<PurshasedAddition> PurshasedAdditions { get; set; }

    public virtual DbSet<PurshasedBoardGame> PurshasedBoardGames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer($"data source={Properties.Settings.Default.ServerName};initial catalog = {Properties.Settings.Default.BDName};persist security info=True;user id={Properties.Settings.Default.BDUserLogin};password={Properties.Settings.Default.Password};TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addition>(entity =>
        {
            entity.HasKey(e => e.IdAddition);

            entity.ToTable("Addition");

            entity.HasIndex(e => e.Name, "UQ_Addition_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdBoardGameNavigation).WithMany(p => p.Additions)
                .HasForeignKey(d => d.IdBoardGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addition_TableGame");
        });

        modelBuilder.Entity<BoardGame>(entity =>
        {
            entity.HasKey(e => e.IdBoardGame).HasName("PK_TableGame");

            entity.ToTable("BoardGame");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.MinCountPlayers).HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(7, 2)");

            entity.HasMany(d => d.IdCategories).WithMany(p => p.IdBoardGames)
                .UsingEntity<Dictionary<string, object>>(
                    "BoardGameCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TableGameCategory_Category"),
                    l => l.HasOne<BoardGame>().WithMany()
                        .HasForeignKey("IdBoardGame")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TableGameCategory_TableGame"),
                    j =>
                    {
                        j.HasKey("IdBoardGame", "IdCategory").HasName("PK_TableGameCategory");
                        j.ToTable("BoardGameCategory");
                        j.HasIndex(new[] { "IdBoardGame", "IdCategory" }, "UQ_BoardGame_Category").IsUnique();
                        j.IndexerProperty<int>("IdCategory").HasColumnName("idCategory");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("Category", tb => tb.HasTrigger("trDeleteCategory"));

            entity.HasIndex(e => e.Name, "UQ_Name").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Purshase>(entity =>
        {
            entity.HasKey(e => e.IdPurshases);

            entity.Property(e => e.BuyersName).HasMaxLength(60);
            entity.Property(e => e.BuyersPatronymic).HasMaxLength(60);
            entity.Property(e => e.BuyersSurname).HasMaxLength(60);
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(11);
        });

        modelBuilder.Entity<PurshasedAddition>(entity =>
        {
            entity.HasKey(e => new { e.IdAddition, e.IdPurshases });

            entity.ToTable("PurshasedAddition", tb => tb.HasTrigger("trChangeAdditionCount"));

            entity.HasIndex(e => new { e.IdAddition, e.IdPurshases }, "UQ_Addition_Purshases").IsUnique();

            entity.HasOne(d => d.IdAdditionNavigation).WithMany(p => p.PurshasedAdditions)
                .HasForeignKey(d => d.IdAddition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurshasedAddition_Addition");

            entity.HasOne(d => d.IdPurshasesNavigation).WithMany(p => p.PurshasedAdditions)
                .HasForeignKey(d => d.IdPurshases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurshasedAddition_Purshases");
        });

        modelBuilder.Entity<PurshasedBoardGame>(entity =>
        {
            entity.HasKey(e => new { e.IdBoardGame, e.IdPurshases }).HasName("PK_PurshasedTableGame");

            entity.ToTable("PurshasedBoardGame", tb => tb.HasTrigger("trChangeBoardGameCount"));

            entity.HasIndex(e => new { e.IdBoardGame, e.IdPurshases }, "UQ_BoardGame_Purshases").IsUnique();

            entity.HasOne(d => d.IdBoardGameNavigation).WithMany(p => p.PurshasedBoardGames)
                .HasForeignKey(d => d.IdBoardGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurshasedTableGame_TableGame");

            entity.HasOne(d => d.IdPurshasesNavigation).WithMany(p => p.PurshasedBoardGames)
                .HasForeignKey(d => d.IdPurshases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurshasedTableGame_Purshases");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
