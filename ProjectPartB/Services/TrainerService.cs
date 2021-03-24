using ProjectPartB.Data;
using ProjectPartB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPartB.Services
{
    class TrainerService : Trainer
    {
        private readonly string query = "SELECT * FROM Trainers";

        public List<Trainer> SetList()
        {
            List<Trainer> trainers = new List<Trainer>();
            trainers = GetAll<Trainer>(query);
            List<Subject> subjects = new SubjectService().SetList();
            foreach (Trainer trainer in trainers)
            {
                Subject subject = subjects.FirstOrDefault(x => x.SubjectId == trainer.SubjectId);
                if (subject is Subject)
                {
                    trainer.Subject = subject.SubjectName;
                }
            }
            return trainers;
        }
        public List<Trainer> SetListAnalytic(List<Trainer> trainers)
        {
            List<Course> courses = new CourseService().GetList();
            new Courses_Trainers().SetCrs_Tr(courses, trainers);
            return trainers;
        }

        public void Create()
        {
            Trainer trainer = new Trainer();
            do
            {
                NameInsert(trainer);
                if (Exit(trainer.FirstName) || Exit(trainer.LastName)) { return; }
            } while (CheckEquality(trainer));
            trainer.SubjectId = SubjectInsert();
            if (trainer.SubjectId == 0) { return; }
            CreateData(trainer, "Trainers");
        }


        public void NameInsert(Trainer trainer)
        {
            Console.Clear();
            Console.WriteLine("You can quit if you type 'exit'");
            do
            {
                Console.Write("Give First Name:\n");
                trainer.FirstName = String().Trim();
                if (Exit(trainer.FirstName)) { trainer.FirstName = "exit"; return; }
                Console.Write($"You gave this first name {trainer.FirstName}\nDo you want to proceed? <Y> or <N>?:\n");
            } while (!YesOrNo());
            do
            {
                Console.Write("Give Last Name:\n");
                trainer.LastName = String().Trim();
                if (Exit(trainer.LastName)) { trainer.LastName = "exit"; return; }
                Console.Write($"You gave this last name {trainer.LastName}\nDo you want to proceed? <Y> or <N>?:\n");
            } while (!YesOrNo());
        }

        public int SubjectInsert()
        {
            Console.Clear();
            Console.WriteLine("Choose the subject for the trainer:");
            List<Subject> subjects = new SubjectService().SetList();
            subjects.ForEach(x => Console.WriteLine($"{x.SubjectId}.{x.SubjectName}"));
            Console.Write("------\n>");
            int temp = IntegerId<Subject>(subjects);
            return temp;
        }

        private bool CheckEquality(Trainer trainer)
        {
            List<Trainer> trainers = new List<Trainer>();
            trainers = GetAll<Trainer>(query);
            Trainer other = trainers.FirstOrDefault(x => x.FirstName == trainer.FirstName && x.LastName == trainer.LastName);
            if (other is Trainer)
            {
                Console.WriteLine("Trainer already exists!\n\n");
                Console.Write("Press any button to continue...");
                Console.ReadKey();
                return true;
            }
            return false;
        }

    }
}
