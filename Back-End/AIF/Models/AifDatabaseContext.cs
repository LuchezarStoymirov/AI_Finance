using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AIF.Models;

public partial class AifDatabaseContext : DbContext
{
    public AifDatabaseContext()
    {
    }

    public AifDatabaseContext(DbContextOptions<AifDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=AIF_Database;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
