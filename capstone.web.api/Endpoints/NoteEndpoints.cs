using capstone.web.api;
using capstone.web.api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
//using Capstone.web.api.Entities;

namespace Capstone.web.api.Endpoints
{
    public static class NoteEndpoints
    {
        public static void MapNoteEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/notes", [Authorize] async (AppDbContext db) =>
            {
                return Results.Ok(await db.Notes.Include(n => n.Category).ToListAsync());
            });

            endpoints.MapGet("/api/notes/{id}", [Authorize] async (int id, AppDbContext db) =>
            {
                var note = await db.Notes.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
                return note is not null ? Results.Ok(note) : Results.NotFound();
            });

            endpoints.MapPost("/api/notes", [Authorize] async (Note note, AppDbContext db) =>
            {
                db.Notes.Add(note);
                await db.SaveChangesAsync();
                return Results.Created($"/api/notes/{note.Id}", note);
            });

            endpoints.MapPut("/api/notes/{id}", [Authorize] async (int id, Note updatedNote, AppDbContext db) =>
            {
                var note = await db.Notes.FindAsync(id);
                if (note is null)
                {
                    return Results.NotFound();
                }

                note.Title = updatedNote.Title;
                note.Content = updatedNote.Content;
                note.CategoryId = updatedNote.CategoryId;
                note.UpdatedAt = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return Results.Ok(note);
            });

            endpoints.MapDelete("/api/notes/{id}", [Authorize] async (int id, AppDbContext db) =>
            {
                var note = await db.Notes.FindAsync(id);
                if (note is not null)
                {
                    db.Notes.Remove(note);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
                return Results.NotFound();
            });

            endpoints.MapGet("/api/notes/search", [Authorize] async (string query, AppDbContext db) =>
            {
                var notes = await db.Notes
                    .Include(n => n.Category)
                    .Where(n => n.Title.Contains(query) || n.Content.Contains(query))
                    .ToListAsync();
                return Results.Ok(notes);
            });
        }
    }
}

