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
    class Grade : DataManagement
    {
        private readonly string query = "SELECT * FROM Grades";
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double OralGrade { get; set; }
        public double TotalGrade { get; set; }

        public void GetAssignmentPerStudent(List<Student> students, List<Assignment> assignments)
        {
            List<Grade> grades = new List<Grade>();
            grades = GetAll<Grade>(query);
            foreach (Grade grade in grades)
            {
                Student student = students.FirstOrDefault(x => x.StudentId == grade.StudentId);
                Assignment assignment = assignments.FirstOrDefault(x => x.AssignmentId == grade.AssignmentId);
                if (assignment is Assignment && student is Student)
                {
                    student.Assignments.Add(assignment);
                }

            }
        }

        public void InsertAssignmentPerCoursePerStudent(Assignment assignment) // λυπαμαι για οτι ακολουθει.
        {
            List<Grade> grades = new List<Grade>();
            grades = GetAll<Grade>(query);
            List<Student> students = new StudentService().GetList();
            students = new StudentService().GetListAnalytic(students);
            List<Course_Assignment> courses_Assignments = new Course_Assignment().GetList();
            List<Course> courses = new CourseService().GetList();
            courses = new CourseService().GetListAnalytic(courses);
            do
            {
                Console.Write($"Assignment: {assignment.AssignmentId} {assignment.Title} {assignment.Description}\n\n------\n>");
                int tempst;
                Grade grade = new Grade();
                new StudentView().Display(students);
                Console.Write("Select Student to Assign:\nPress 0 to go back\n\n------\n>");
                tempst = IntegerId<Student>(students);
                if (tempst == 0)
                { Console.WriteLine("Process Terminated"); return; }
                Student student = students.FirstOrDefault(x => x.StudentId == tempst);


                if (student.Courses.Count() == 0)
                {
                    Console.WriteLine("Student is not enrolled to any course!Process terminated!");
                }
                else if (student.Courses.Count() == 1)
                {
                    int tempcourid0;
                    tempcourid0 = student.Courses[0].CourseId;
                    Course_Assignment course_Assignment1 = courses_Assignments.FirstOrDefault(x => x.CourseId == tempcourid0 && x.AssignmentId == assignment.AssignmentId);
                    if (course_Assignment1 is Course_Assignment)
                    {
                        grade.CourseId = tempcourid0;
                    }
                    else
                    {
                        Console.WriteLine("Assignment is not assigned to this course.Process terminated");
                        return;
                    }
                }
                else
                {
                    int listsize = student.Courses.Count();
                    int temp;
                    int[] tempcourid = new int[listsize];
                    for (int i = 0; i < listsize; i++)
                    {
                        Console.WriteLine("Student is enrolled to course(s):");
                        CourseView courseView = new CourseView();
                        courseView.Display(student.Courses);
                        while (true)
                        {
                            Console.Write("Please choose in which one the assignment will be assigned!\nPress 0 to go back\n\n------\n>");
                            temp = IntegerId<Course>(student.Courses);
                            if (temp == 0)
                            { Console.WriteLine("Process Terminated"); return; }
                            if (!tempcourid.Contains(temp))
                            {

                                tempcourid[i] = temp;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Already tried this course with no success!");
                            }

                        }
                        Course_Assignment course_Assignment = courses_Assignments.FirstOrDefault(x => x.CourseId == tempcourid[i] && x.AssignmentId == assignment.AssignmentId);
                        Grade grade1 = grades.FirstOrDefault(x => x.CourseId == tempcourid[i] && x.AssignmentId == assignment.AssignmentId && x.StudentId == tempst);
                        if (grade1 is Grade) { Console.WriteLine($"Already assigned to {student.FirstName} {student.LastName} for this course!"); }
                        else if (course_Assignment is Course_Assignment)
                        {
                            grade.CourseId = tempcourid[i];
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Assignment is NOT assigned to this course.Try again!");
                        }

                    }
                }
                if (grade.CourseId != 0)
                {
                    grade.AssignmentId = assignment.AssignmentId;
                    grade.StudentId = tempst;


                    CreateData<Grade>(grade, "Grades");
                }
                else
                {
                    Console.WriteLine("Process Failed");
                }
                Console.Write("Do you want to add more students to this assignment?:<Y> or <N>?:\n>");
            } while (YesOrNo());
        }
    }
}
