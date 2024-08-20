using Labb_3___API.Data;
using Labb_3___API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Labb_3___API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Retun all people
            app.MapGet("/people", async (ApplicationDbContext context) =>
            {
                var people = await context.People
                .Include(p => p.PeopleWithInterests)
                    .ThenInclude(i => i.Interest)
                    .ThenInclude(i => i.Links)
                .ToListAsync();
                if (people == null || !people.Any())
                {
                    return Results.NotFound("No people found");
                }
                return Results.Ok(people);
            });

            // Create people
            app.MapPost("/people", async (People people, ApplicationDbContext context) =>
            {
                context.People.Add(people);
                await context.SaveChangesAsync();
                return Results.Created($"/people/{people.Id}", people);
            });

            // Get people by Id
            app.MapGet("/people/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var people = await context.People
                .Include(p => p.PeopleWithInterests)
                    .ThenInclude(pi => pi.Interest)
                    .ThenInclude(i => i.Links)
                .FirstOrDefaultAsync(p => p.Id == id);
                if (people == null)
                {
                    return Results.NotFound("People not found");
                }
                return Results.Ok(people);
            });

            // Edit people
            app.MapPut("/people/{id:int}", async (int id, People updatedPeople, ApplicationDbContext context) =>
            {
                var people = await context.People.FindAsync(id);
                if (people == null)
                {
                    return Results.NotFound("People not found");
                }
                people.FirstName = updatedPeople.FirstName;
                people.LastName = updatedPeople.LastName;
                people.Email = updatedPeople.Email;
                people.PhoneNumber = updatedPeople.PhoneNumber;
                context.People.Update(people);
                await context.SaveChangesAsync();
                return Results.Ok(people);
            });

            // Delete people
            app.MapDelete("/people/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var people = await context.People.FindAsync(id);

                if (people == null)
                {
                    return Results.NotFound("People not found");
                }
                context.People.Remove(people);
                await context.SaveChangesAsync();
                return Results.Ok($"People with ID: {id} was deleted");
            });

            // Return all interests
            app.MapGet("/interests", async (ApplicationDbContext context) =>
            {
                var interests = await context.Interests.ToListAsync();
                if (interests == null || !interests.Any())
                {
                    return Results.NotFound("No interests found");
                }
                return Results.Ok(interests);
            });

            // Create interest
            app.MapPost("/interests", async (Interest interest, ApplicationDbContext context) =>
            {
                context.Interests.Add(interest);
                await context.SaveChangesAsync();
                return Results.Created($"/interests/{interest.Id}", interest);
            });

            // Delete interest
            app.MapDelete("/interests/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var interest = await context.Interests.FindAsync(id);

                if (interest == null)
                {
                    return Results.NotFound("Interest not found");
                }
                context.Interests.Remove(interest);
                await context.SaveChangesAsync();
                return Results.Ok($"Interest with ID: {id} was deleted");
            });

            // Return all PeopleWithInterests
            app.MapGet("/peoplewithinterests", async (ApplicationDbContext context) =>
            {
                var peoplewithinterests = await context.PeopleWithInterests
                .Include(pi => pi.Interest)
                .ToListAsync();
                if (peoplewithinterests == null || !peoplewithinterests.Any())
                {
                    return Results.NotFound("No peoplewithinterests found");
                }
                return Results.Ok(peoplewithinterests);
            });

            // Create PeopleWithInterests
            app.MapPost("/peoplewithinterests", async (PeopleWithInterest peopleWithInterests, ApplicationDbContext context) =>
            {
                context.PeopleWithInterests.Add(peopleWithInterests);
                await context.SaveChangesAsync();
                return Results.Created($"/peoplewithinterests/{peopleWithInterests.Id}", peopleWithInterests);
            });

            // Delete PeopleWithInterests
            app.MapDelete("/peoplewithinterests/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var peopleWithInterests = await context.PeopleWithInterests.FindAsync(id);

                if (peopleWithInterests == null)
                {
                    return Results.NotFound("PeopleWithInterests not found");
                }
                context.PeopleWithInterests.Remove(peopleWithInterests);
                await context.SaveChangesAsync();
                return Results.Ok($"PeopleWithInterests with ID: {id} was deleted");
            });

            // Create Link
            app.MapPost("/links", async (Link link, ApplicationDbContext context) =>
            {
                context.Links.Add(link);
                await context.SaveChangesAsync();
                return Results.Created($"/links/{link.Id}", link);
            });

            // Delete Link
            app.MapDelete("/links/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var link = await context.Links.FindAsync(id);

                if (link == null)
                {
                    return Results.NotFound("Link not found");
                }
                context.Links.Remove(link);
                await context.SaveChangesAsync();
                return Results.Ok($"Link with ID: {id} was deleted");
            });

            app.Run();
        }
    }
}
