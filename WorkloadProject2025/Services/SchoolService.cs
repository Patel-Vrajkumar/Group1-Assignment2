using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class SchoolService : ISchoolService
    {
        //These lines give me acces to an instance of my database context
        //injected through dependency injection
        ApplicationDbContext _context;
        public SchoolService(ApplicationDbContext db)
        {
            _context = db;
        }
        public async Task<School> AddAsync(School school, CancellationToken cancellationToken = default)
        {
            if (school == null)
                throw new ArgumentNullException();
            if(school.Name.Trim() == "")
            {
                throw new Exception("School must have a name");
            }
            //to add records into a database with EF we just pass in our object and save changes
            _context.Schools.Add(school);
            //Commit to the db
            await _context.SaveChangesAsync();
            //Return the record
            return school;

        }
        public Task<List<School>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            //Empty list should be okay
            return _context.Schools.ToListAsync();
        }

        public Task<School?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            //LINQ is a language that lets you write queries in C#
            return _context.Schools.FirstOrDefaultAsync(school => school.Id == id);
        }
    }
}
