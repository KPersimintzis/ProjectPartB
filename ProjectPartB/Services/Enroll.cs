using ProjectPartB.Data;
using ProjectPartB.Entities;
using ProjectPartB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPartB.Services
{
    class Enroll : DataManagement
    {
        string query = "SELECT * FROM Enroll";
        public int StudentId { get; set; }
        public int CourseId { get; set; }


        public void SetEnrollment(List<Course> courses, List<Student> students)
        {
            List<Enroll> enrolls = new List<Enroll>();
            enrolls = GetAll<Enroll>(query);
            foreach (Enroll enroll in enrolls)
            {
                Student student = students.FirstOrDefault(x => x.StudentId == enroll.StudentId);
                Course course = courses.FirstOrDefault(x => x.CourseId == enroll.CourseId);
                if (student is Student && course is Course)
                {
                    student.Courses.Add(course);
                    course.Students.Add(student);
                }
            }
        }

        public void SetEnrollmentById(Student student)
        {
            List<Enroll> enrolls = new List<Enroll>();
            enrolls = GetAll<Enroll>(query);
            List<Course> courses = new CourseService().GetList();
            new CourseView().Display(courses);
            do
            {
                int temp;
                Enroll enroll = new Enroll();
                do
                {
                    Console.Write("Select Course to Enroll:\nPress 0 to go back\n\n------\n>");
                    temp = IntegerId<Course>(courses);
                    if (temp == 0)
                    { Console.WriteLine("Process Terminated"); return; }
                    enroll = enrolls.FirstOrDefault(x => x.StudentId == student.StudentId && x.CourseId == temp);
                    if (enroll is Enroll) { Console.WriteLine("Student is already enrolled in this course!Try again"); }
                } while (enroll is Enroll);

                Enroll enroll1 = new Enroll();
                enroll1.CourseId = temp;
                enroll1.StudentId = student.StudentId;
                CreateData<Enroll>(enroll1, "Enroll");
                Console.Write("Do you want to add course to this student?:<Y> or <N>?:\n>");
            } while (YesOrNo());
        }
    }
}

