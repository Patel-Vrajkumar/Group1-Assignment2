using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class FacultyService : IFacultyService
    {
        ApplicationDbContext _context;
        public FacultyService(ApplicationDbContext db)
        {
            _context = db;
        }

        public async Task<Faculty> AddAsync(Faculty faculty, CancellationToken cancellationToken = default)
        {
            if (faculty == null)
                throw new ArgumentNullException();
            if(faculty.Email.Trim() == "")
            {
                throw new Exception("Email must be entered");
            }
            _context.Faculty.Add(faculty);

           await _context.SaveChangesAsync();

            return faculty;   
        }

        public Task<List<Faculty>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Faculty
                .Include(f => f.Department)
                .ToListAsync();
        }

        public Task<Faculty?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Faculty.FirstOrDefaultAsync(faculty => faculty.Email == email);
        }

        public async Task<Faculty> UpdateAsync(Faculty faculty, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Faculty.FirstOrDefaultAsync(f => f.Email == faculty.Email);
            if (existing == null)
                throw new InvalidOperationException("Faculty not found");

            existing.FirstName = faculty.FirstName;
            existing.LastName = faculty.LastName;
            existing.PhoneNumber = faculty.PhoneNumber;
            existing.DepartmentId = faculty.DepartmentId;
            existing.DateHired = faculty.DateHired;
            existing.Status = faculty.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(string email, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Faculty.FirstOrDefaultAsync(f => f.Email == email);
            if (existing == null)
                return;

            _context.Faculty.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
