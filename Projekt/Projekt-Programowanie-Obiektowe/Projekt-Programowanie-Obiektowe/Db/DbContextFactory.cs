using Microsoft.EntityFrameworkCore;
using System;
using Projekt-Programowanie-Obiektowe.Data.Context;

namespace Projekt_Programowanie_Obiektowe.Db;

public static class DbContextFactory
{
    private const string Conn =
        "Host=localhost;Port=5432;Database=transport_db;Username=postgres;Password=postgres";

    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(Conn)
            .EnableSensitiveDataLogging()
            .Options;

        return new AppDbContext(options);
    }
}
