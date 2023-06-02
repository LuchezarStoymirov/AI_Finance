using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AIF.Models;

namespace AIF.Data;

public partial class AifDatabaseContext : DbContext
{
    public AifDatabaseContext(DbContextOptions<AifDatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=AIF_Database;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}