using ProjectPartB.Entities;
using System;
using System.Collections.Generic;

namespace ProjectPartB.ViewModels
{
    class TrainerView
    {
        public void Display(List<Trainer> trainers)
        {
            Console.WriteLine("Id FullName\t Subject");
            trainers.ForEach(x => Console.WriteLine($"{x.TrainerId} {x.FirstName} {x.LastName}\t {x.Subject}"));
        }

        public void DisplayById(Trainer trainer)
        {
            Console.Clear();
            Console.WriteLine("Id FullName\t Subject");
            Console.WriteLine($"{trainer.TrainerId} {trainer.FirstName} {trainer.LastName}\t {trainer.Subject}");
            Console.WriteLine($"Course Title\t Stream \tType \t Starting    \t Ending Date: ");
            foreach (Course course in trainer.Courses)
            {
                Console.WriteLine($"{course.Title} \t{course.Stream}\t {course.Type} \t {course.StartDate:d} \t{course.EndDate:d}");
            }
        }
    }
}
