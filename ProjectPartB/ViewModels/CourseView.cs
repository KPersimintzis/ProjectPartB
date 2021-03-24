using ProjectPartB.Entities;
using ProjectPartB.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPartB.ViewModels
{
    class CourseView
    {
        public void Display(List<Course> courses)
        {
            Console.WriteLine("Id Title\t Stream\t Type\t StartDate EndDate");
            courses.ForEach(x => Console.WriteLine($"{x.CourseId} {x.Title}\t {x.Stream}\t {x.Type}\t{x.StartDate:d} {x.EndDate:d}"));
        }

        public void DisplayById(Course course)
        {
            Console.Clear();
            Console.WriteLine("Id Title\t Stream\t Type\t StartDate EndDate");
            Console.WriteLine($"{course.CourseId} {course.Title}\t {course.Stream}\t {course.Type}\t{course.StartDate:d} {course.EndDate:d}");
            Console.WriteLine($"\nStudents:");
            foreach (Student a in course.Students)
            {
                Console.Write($"\t {a.FirstName} {a.LastName}\n");
            }
            Console.WriteLine("\nTrainers:");
            foreach (Trainer a in course.Trainers)
            {
                Console.Write($"\t {a.FirstName} {a.LastName}\n");
            }
            Console.WriteLine("Assignments:");
            foreach (Assignment a in course.Assignments)
            {
                Console.Write($"\t    {a.Title}\t{a.Description}\n");
            }
        }
    }
}
