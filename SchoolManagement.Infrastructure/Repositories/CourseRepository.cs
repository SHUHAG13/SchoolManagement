using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _context;
        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task AddCourseAsync(Course course)
        {
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"An exception is occured when adding a Course {ex.Message}",ex);
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    throw new Exception($"Course with ID {id} not found.");
                }
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

            } catch (Exception ex) {
                throw new Exception($"An exception is occured when deleting a Course {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                return await _context.Courses.Include(c=>c.Students).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception is occured when retrieving all Courses {ex.Message}", ex);

            }
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            try
            {
                return await _context.Courses
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception is occured when retrieving a Course by ID {id} {ex.Message}", ex);
            }
        }

        public async Task UpdateCourseAsync(Course course)
        {
            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception is occured when updating a Course {ex.Message}", ex);
            }
        }
    }
}
