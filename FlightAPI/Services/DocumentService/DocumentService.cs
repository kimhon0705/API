using FlightAPI.DatabaseContext;
using FlightAPI.Models;
using FlightAPI.Services.DocumentService.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightAPI.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightDbContext _dbContext;
        public DocumentService(FlightDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Document>>? GetAllDocument()
        {
            var documents = await _dbContext.Documents.ToListAsync();
            return documents;
        }
        public async Task<Document>? GetDocumentById(int id)
        {
            var documentFound = await _dbContext.Documents.Include(f => f.DocumentFiles).FirstOrDefaultAsync(x => x.Id == id);
            if (documentFound is null)
                return null;
            return documentFound;
        }
        public async Task<List<Document>>? GetDocumentBySearch(string search)
        {
            List<Document> documents = await _dbContext.Documents.Where(f => f.Name.Contains(search) || f.CreateDate.ToString().Contains(search)).ToListAsync();

            if (documents.Count() == 0)
                return null;
            return documents;
        }
        public async Task<Document>? AddDocument([FromForm] AddDocumentDTO document)
        {
            var newDocument = new Document()
            {
                Name = document.Name,
                Note = document.Note,
                CreateDate = DateTime.Now,
                Version = Convert.ToString(1.0),
                FlightID = document.FlightID,
                Document_TypeID = document.Document_TypeID,
                UserID = document.UserID,
                GroupID = document.GroupID
            };
            if (document.FormFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", document.FormFile.FileName);
                using (var stream = File.Create(path))
                {
                    await document.FormFile.CopyToAsync(stream);
                }
                newDocument.Url = "/files/" + document.FormFile.FileName;
            }
            else
            {
                newDocument.Url = "";
            }

            _dbContext.Documents.Add(newDocument);
            await _dbContext.SaveChangesAsync();

            return newDocument;
        }
        public async Task<List<Document>>? DeleteDocument(int id)
        {
            var documentFound = await _dbContext.Documents.FindAsync(id);
            if (documentFound == null)
                return null;
            _dbContext.Documents.Remove(documentFound);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Documents.ToListAsync();
        }

    }
}
