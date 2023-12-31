﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
{
    private IConfiguration BuildConfiguration(string basePath)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        return new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true, false)
            .Build();
    } // TODO maybe move to common

    private string FindRootDirectoryName(DirectoryInfo baseDir, int depthThreshold = 5)
    {
        DirectoryInfo dir = baseDir;
        int depth = 0;

        while (++depth < depthThreshold && dir?.Name != "bin")
        {
            dir = dir?.Parent;
        }

        return dir?.Parent?.FullName;
    } // TODO maybe move to common

    public PostgresContext CreateDbContext(string[] args)
    {
        string path = FindRootDirectoryName(Directory.GetParent(AppContext.BaseDirectory));
        IConfiguration config = BuildConfiguration(path);

        string connectionString = config.GetConnectionString("Postgres");

        var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PostgresContext(optionsBuilder.Options);
    }
}