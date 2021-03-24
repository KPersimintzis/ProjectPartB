using ProjectPartB.Data;
using System;
using System.Collections.Generic;

namespace ProjectPartB.Entities
{
    class Assignment : DataManagement
    {
        public int AssignmentId { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }

        public double OralMark { get; set; }
        public double TotalMark { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
