using System;
using System.Collections.Generic;
using ProjectPartB.Data;


namespace ProjectPartB.Entities
{
    class Student : DataManagement
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal TuitionFee { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
