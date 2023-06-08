using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AIF.Models;
using Microsoft.Extensions.Configuration;

namespace AIF.Data;

public partial class AifDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AifDatabaseContext(DbContextOptions<AifDatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}