using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreditCardValidator.Data.Models;

public partial class CreditCardValidatorContext : DbContext
{
    public CreditCardValidatorContext()
    {
    }

    public CreditCardValidatorContext(DbContextOptions<CreditCardValidatorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardValidation> CardValidations { get; set; }

    public virtual DbSet<LogException> LogExceptions { get; set; }

    public virtual DbSet<LogRequest> LogRequests { get; set; }

    public virtual DbSet<LogResponse> LogResponses { get; set; }

    public virtual DbSet<LogStep> LogSteps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CIN7-MANOJ;Database=CreditCardValidator;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC079BA2CC1D");

            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<CardValidation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StartingNumber).HasMaxLength(5);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Card).WithMany(p => p.CardValidations)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardId_CardValidations_Cards");
        });

        modelBuilder.Entity<LogException>(entity =>
        {
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<LogRequest>(entity =>
        {
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<LogResponse>(entity =>
        {
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<LogStep>(entity =>
        {
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
