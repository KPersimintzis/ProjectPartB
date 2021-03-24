using System;
using System.Collections.Generic;
using ProjectPartB.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB.Entities
{
    class Course : DataManagement
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StreamId { get; set; }
        public int TypeId { get; set; }

        public string Stream { get; set; }
        public string Type { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
