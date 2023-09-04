using ShipManagement.Repositories;
using ShipManagement.Repositories.Models;

public class ShipContextSeed
{
    public static void SeedShips(ShipManagementContext context)
    {
        if (context.Ships.Any())
        {
            return; // Data is already seeded
        }

        var ships = new List<Ship>
            {
                new Ship
                {
                    Name = "Ship1",
                    UniqueShipId = "SHIP-REG-NO-1",
                },
                new Ship
                {
                    Name = "Ship2",
                    UniqueShipId = "SHIP-REG-NO-2",
                },
                // Add more ships as needed
            };

        context.Ships.AddRange(ships);
        context.SaveChanges();
        var random = new Random();

        var shipStatuses = new List<ShipStatus>();
        foreach (var ship in ships)
        {
            for (int i = 0; i < 5; i++) // Generate 5 random ShipStatus entries for each ship
            {
                var status = new ShipStatus
                {
                    ShipId = ship.Id,
                    Velocity = random.Next(1, 100), // Generate random velocity between 1 and 100
                    Latitude = random.NextDouble() * 180 - 90, // Generate random latitude between -90 and 90
                    Longitude = random.NextDouble() * 360 - 180, // Generate random longitude between -180 and 180
                    LastCheckedIn = DateTime.Now // Generate timestamps at 15-minute intervals
                };

                shipStatuses.Add(status);
            }
        }

        context.ShipStatuses.AddRange(shipStatuses);
        context.SaveChanges();
    }

    public static void SeedPorts(ShipManagementContext context)
    {
        if (context.Ports.Any())
        {
            return; // Data is already seeded
        }

        var ports = new List<Port>
{
            new Port
            {
                Name = "Port1",
                Latitude = 40.7128,
                Longitude = -74.0060,
                RegistrationDateTime = DateTime.Now
            },
            new Port
            {
                Name = "Port2",
                Latitude = 34.0522,
                Longitude = -118.2437,
                RegistrationDateTime = DateTime.Now
            },
            new Port
            {
                Name = "Port3",
                Latitude = 50.2311,
                Longitude = -50.2437,
                RegistrationDateTime = DateTime.Now
            },
            new Port
            {
                Name = "Port4",
                Latitude = -80.0522,
                Longitude = -118.2437,
                RegistrationDateTime = DateTime.Now
            },
            // Add more ports as needed
        };

        context.Ports.AddRange(ports);
        context.SaveChanges();
    }
}