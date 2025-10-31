using Microsoft.EntityFrameworkCore;
using WorkloadProject2025.Data;
using WorkloadProject2025.Data.Models;

namespace WorkloadProject2025.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        
        public CourseService(ApplicationDbContext db)
        {
            _context = db;
        }
        
        public async Task<Course> AddAsync(Course course, CancellationToken cancellationToken = default)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));
                
            if (string.IsNullOrWhiteSpace(course.Name))
                throw new Exception("Course must have a name");
                
            _context.Courses.Add(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course;
        }
        
        public Task<List<Course>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Courses.ToListAsync(cancellationToken);
        }
        
        public Task<Course?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Courses.FirstOrDefaultAsync(course => course.Id == id, cancellationToken);
        }
    }
}