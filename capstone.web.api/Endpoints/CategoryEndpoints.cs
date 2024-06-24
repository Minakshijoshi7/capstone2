using capstone.web.api;
using capstone.web.api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Capstone.web.api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/categories", [Authorize] async (AppDbContext db) =>
            {
                return Results.Ok(await db.Categories.ToListAsync());
            });

            endpoints.MapGet("/api/categories/{id}", [Authorize] async (int id, AppDbContext db) =>
            {
                var category = await db.Categories.FindAsync(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });

            endpoints.MapPost("/api/categories", [Authorize] async (Category category, AppDbContext db) =>
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Created($"/api/categories/{category.Id}", category);
            });

            endpoints.MapPut("/api/categories/{id}", [Authorize] async (int id, Category updatedCategory, AppDbContext db) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null)
                {
                    return Results.NotFound();
                }

                category.Name = updatedCategory.Name;

                await db.SaveChangesAsync();
                return Results.Ok(category);
            });

            endpoints.MapDelete("/api/categories/{id}", [Authorize] async (int id, AppDbContext db) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is not null)
                {
                    db.Categories.Remove(category);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.NotFound();
            });
        }
    }
}
