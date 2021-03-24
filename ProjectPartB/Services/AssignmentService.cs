using ProjectPartB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPartB.Services
{
    class AssignmentService : Assignment
    {
        private readonly string query = "SELECT * FROM Assignments";

        public List<Assignment> SetList() => GetAll<Assignment>(query);

        public List<Assignment> SetListAnalytic(List<Assignment> assignments)
        {
            List<Course> courses = new CourseService().GetList();
            List<Student> students = new StudentService().GetList();
            new Enroll().SetEnrollment(courses, students);
            List<Trainer> trainers = new TrainerService().SetList();
            new Courses_Trainers().SetCrs_Tr(courses, trainers);
            new Course_Assignment().GetCourse_Assignment(courses, assignments);
            return assignments;
        }

        public void Create()
        {
            Assignment assignment = new Assignment();
            do
            {
                assignment.Title = TitleInsert();
                if (Exit(assignment.Title)) { return; }
            } while (CheckEquality(assignment));
            assignment.Description = DescriptionInsert();
            SubmissionInsert(assignment);
            MarksInsert(assignment);
            CreateData<Assignment>(assignment, "Assignments");
        }

        public string TitleInsert()
        {
            Console.Clear();
            Console.WriteLine("You can quit if you type 'exit'");
            string title;
            do
            {
                Console.Write("Give a title to your assignment:\n> ");
                title = Console.ReadLine().Trim();
                if (Exit(title)) { return "exit"; }
                Console.Write($"You gave this title {title}\nDo you want to proceed ? <Y> or <N>?:\n");
            } while (!YesOrNo());
            return title;
        }
        public string DescriptionInsert()
        {
            Console.Clear();
            string description;
            Console.Write("Give Description for the Assignment\n>");
            description = Console.ReadLine();
            return description.Trim();
        }
        public void SubmissionInsert(Assignment assignment)
        {
            Console.Clear();
            Console.WriteLine($"Give the Submission Date ({this.Title})");
            Console.Write($"From: {DateTime.Now.AddDays(1):d} To: {DateTime.Now.AddMonths(12):d} \n\n (dd/MM/yyyy)\n>");
            assignment.SubmissionDate = Dates(DateTime.Now, DateTime.Now.AddMonths(12));
            if (assignment.SubmissionDate.DayOfWeek == DayOfWeek.Saturday || assignment.SubmissionDate.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("Submission cannot be at weekend!Do you want to be next week?<Y> or <N>?\nIf <N> it will be at previous.");
                if (YesOrNo())
                {
                    assignment.SubmissionDate = assignment.SubmissionDate.AddDays(2);
                }
            }
        }
        public void MarksInsert(Assignment assignment)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("Give marks for the assignment.Max Total value is 100");
                Console.Write("The maximum mark for the oral presentation is:\n\n------\n>");
                assignment.OralMark = Double(0, 100);
                Console.Write($"The max of oral mark is:{assignment.OralMark}.The maximum mark for the paper of the assginment is {100 - assignment.OralMark}" +
                    $"\nDo you want to proceed? <Y> or <N> ?");
            } while (!YesOrNo());
            assignment.TotalMark = 100 - assignment.OralMark;
        }

        public bool CheckEquality(Assignment assignment)
        {
            List<Assignment> assignments = new List<Assignment>();
            assignments = GetAll<Assignment>(query);
            Assignment other = assignments.FirstOrDefault(x => x.Title == assignment.Title);
            if (other is Assignment)
            {
                Console.WriteLine("Assignment already exists!\n\n");
                Console.Write("Press any button to continue...");
                Console.ReadKey();
                return true;
            }
            return false;
        }


    }
}
