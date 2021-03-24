using ProjectPartB.Data;
using ProjectPartB.Entities;
using ProjectPartB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB.Services
{
    class Course_Assignment : DataManagement
    {
        private readonly string query = "SELECT * FROM Courses_Assignments";
        public int CourseId { get; set; }
        public int AssignmentId { get; set; }

        public List<Course_Assignment> GetList() => GetAll<Course_Assignment>(query);


        public void GetCourse_Assignment(List<Course> courses, List<Assignment> assignments)
        {
            List<Course_Assignment> courses_Assignments = new List<Course_Assignment>();
            courses_Assignments = GetAll<Course_Assignment>(query);
            foreach (Course_Assignment crs_as in courses_Assignments)
            {
                Course course = courses.FirstOrDefault(x => x.CourseId == crs_as.CourseId);
                Assignment assignment = assignments.FirstOrDefault(x => x.AssignmentId == crs_as.AssignmentId);
                if (course is Course && assignment is Assignment)
                {
                    course.Assignments.Add(assignment);
                    assignment.Courses.Add(course);
                }
            }
        }

        public void InsertCourseByAssignment(Assignment assignment)
        {
            List<Course_Assignment> crs_asslist = new List<Course_Assignment>();
            crs_asslist = GetAll<Course_Assignment>(query);
            List<Course> courses = new CourseService().GetList();
            new CourseView().Display(courses);
            do
            {
                Console.Write($"Assignment: {assignment.AssignmentId} {assignment.Title} {assignment.Description}\n\n------\n>");
                Console.WriteLine("Choose a course:\n");
                int temp;
                Course_Assignment courses_Assignment = new Course_Assignment();
                do
                {
                    Console.Write("Select Course to Assign:\nPress 0 to go back\n\n------\n>");
                    temp = IntegerId<Course>(courses);
                    if (temp == 0)
                    { Console.WriteLine("Process Terminated"); return; }
                    courses_Assignment = crs_asslist.FirstOrDefault(x => x.AssignmentId == assignment.AssignmentId && x.CourseId == temp);
                    if (courses_Assignment is Course_Assignment) { Console.WriteLine("Assign is already assigned in this course!Try again"); }
                } while (courses_Assignment is Course_Assignment);

                Course_Assignment courses_Assignment1 = new Course_Assignment();
                courses_Assignment1.AssignmentId = assignment.AssignmentId;
                courses_Assignment1.CourseId = temp;
                CreateData<Course_Assignment>(courses_Assignment1, "Courses_Assignments");
                Console.Write("Do you want to add more courses to this assignment?:<Y> or <N>?:\n>");
            } while (YesOrNo());
        }

    }
}

