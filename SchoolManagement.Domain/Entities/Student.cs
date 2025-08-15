using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Email { get; set; }

        //Foreign key to Course
        public int CourseId { get; set; }
        //Navigation property to Course
        public Course Course { get; set; } = default!;
    }
}
