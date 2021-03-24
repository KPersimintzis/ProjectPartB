using ProjectPartB.Entities;
using ProjectPartB.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjectPartB.Data;

namespace ProjectPartB.ViewModels
{
    class AssignmentView : InputUser
    {
        public void Display(List<Assignment> assignments)
        {
            Console.WriteLine("Assignments\n");
            assignments.ForEach(a => Console.Write($"\nID {a.AssignmentId}. Title: {a.Title}\nDescription: {a.Description}\n" +
                $"Max Oral: {a.OralMark} Max Total: {a.TotalMark}\nSubmission Date: {a.SubmissionDate:dd/MM/yyyy}\n"));
        }

        public void DisplayById(Assignment assignment)
        {
            Console.Clear();
            Console.Write($"\nID {assignment.AssignmentId}. Title: {assignment.Title}\nDescription: {assignment.Description}\n" +
                $"Max Oral: {assignment.OralMark} Max Total: {assignment.TotalMark}\nSubmission Date: {assignment.SubmissionDate}\n\n");
            Console.WriteLine($"Course \t\t\t\t Starting    \t Ending Date: ");
            foreach (Course a in assignment.Courses)
            {
                Console.WriteLine($"{a.CourseId} {a.Title} {a.Stream} {a.Type} \t\t {a.StartDate:d} \t{a.EndDate:d}");
            };
        }
    }
}
