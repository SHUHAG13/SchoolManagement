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
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;
        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task AddStudentAsync(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw new Exception($"An exception occurred when adding a Student: {ex.Message}", ex);
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    throw new Exception($"Student with ID {id} not found.");
                }
                 _context.Students.Remove(student);
                 await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred when deleting a Student: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            try
            {
                return await _context.Students.Include(s => s.Course).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred when retrieving all Students: {ex.Message}", ex);
            }
        }

        public Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                return _context.Students.Include(s => s.Course).FirstOrDefaultAsync(s => s.Id == id);

            }
            catch(Exception ex)
            {
                throw new Exception($"An exception occurred when retrieving a Student by ID: {ex.Message}", ex);
            }   
        }



        public Task UpdateStudentAsync(Student student)
        {
            try
            {
                _context.Students.Update(student);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred when updating a Student: {ex.Message}", ex);
            }
        }
    }
}
