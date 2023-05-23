using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AIF.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;


namespace AIF.Data;

public partial class AifDatabaseContext : DbContext
{
    public AifDatabaseContext()
    {
    }

    private readonly IConfiguration _configuration;

    public AifDatabaseContext(DbContextOptions<AifDatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }


    public DbSet<DemoModel> Demo { get; set; }
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
