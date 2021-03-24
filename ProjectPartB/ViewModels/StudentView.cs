using ProjectPartB.Entities;
using ProjectPartB.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectPartB.ViewModels
{

    class StudentView
    {
        public void Display(List<Student> students)
        {
            Console.WriteLine("Students\n");
            students.ForEach(a => Console.WriteLine($"\nID {a.StudentId}. FullName: {a.FirstName} {a.LastName}\n" +
                $"Date of Birth: {a.DateOfBirth:dd/MM/yyyy}\nTuition Fee: {a.TuitionFee:0.00}\u20AC\n"));
        }

        public void DisplayById(Student student)
        {
            Console.Clear();
            Console.WriteLine("FullName \t TuitionFee");
            Console.WriteLine($"{student.FirstName} {student.LastName}\t {student.TuitionFee:0.00}\u20AC\n");
            Console.WriteLine("\nCourses:");
            Console.WriteLine($"Course Title\t Stream \tType \t Starting    \t Ending Date: ");
            foreach (Course course in student.Courses)
            {
                Console.Write($"{course.Title} \t{course.Stream}\t {course.Type} \t {course.StartDate:d} \t{course.EndDate:d}\n");
            }
            Console.WriteLine("\nAssignment:\nTitle\t Description\t Submission Date");
            foreach (Assignment assignment in student.Assignments)
            {
                Console.Write($"{assignment.Title}\t{assignment.Description}\t{assignment.SubmissionDate}\n");
            }
        }

        public void DisplayMoreThanAClass(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("FullName \t TuitionFee");
            foreach (Student student in students)
            {
                if (student.Courses.Count() >= 2)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName}\t {student.TuitionFee:0.00}\u20AC\n");
                }
            }
            /* Αλλοι τροποι επιλυσης:
             * 1. Να καλεσουμε μεσω της StudentService και το αντιστοιχο query και να δημιουργησουμε λιστα
             * 2. Μεσω του enroll να φτιαξουμε αλλη λιστα μεσω linq ή να καλουμε query και να δημιουργησουμε λιστα
            */

        }
    }
}
