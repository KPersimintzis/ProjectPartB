using System;
using System.Collections.Generic;
using ProjectPartB.Data;

namespace ProjectPartB.Entities
{
    class Trainer : DataManagement
    {
        public int TrainerId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SubjectId { get; set; }

        public string Subject { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();


    }
}
