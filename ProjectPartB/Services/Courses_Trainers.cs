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
    class Courses_Trainers : DataManagement
    {
        private readonly string query = "SELECT * FROM Courses_Trainers";
        public int CourseId { get; set; }
        public int TrainerId { get; set; }

        public void SetCrs_Tr(List<Course> courses, List<Trainer> trainers)
        {
            List<Courses_Trainers> courses_Trainers = new List<Courses_Trainers>();
            courses_Trainers = GetAll<Courses_Trainers>(query);
            foreach (Courses_Trainers crs_tr in courses_Trainers)
            {
                Course course = courses.FirstOrDefault(x => x.CourseId == crs_tr.CourseId);
                Trainer trainer = trainers.FirstOrDefault(x => x.TrainerId == crs_tr.TrainerId);
                if (course is Course && trainer is Trainer)
                {
                    course.Trainers.Add(trainer);
                    trainer.Courses.Add(course);
                }
            }
        }

        public void SetCrs_TrainerId(Trainer trainer)
        {
            List<Courses_Trainers> courses_Trainers = new List<Courses_Trainers>();
            courses_Trainers = GetAll<Courses_Trainers>(query);
            List<Course> courses = new CourseService().GetList();
            new CourseView().Display(courses);
            do
            {
                int temp;
                bool check = false;
                Courses_Trainers crs_Tr = new Courses_Trainers();
                do
                {
                    Console.Write("\nPress 0 to go back\n\nSelect Course :------\n>");
                    temp = IntegerId<Course>(courses);
                    if (temp == 0)
                    {
                        Console.WriteLine("Process Terminated"); return;
                    }
                    crs_Tr = courses_Trainers.FirstOrDefault(x => x.TrainerId == trainer.TrainerId && x.CourseId == temp);
                    Course course = courses.FirstOrDefault(x => x.CourseId == temp);
                    if (course is Course)
                    {
                        if (crs_Tr is Courses_Trainers)
                        {
                            check = true;
                            Console.WriteLine("Trainer already trains this course!Try again");
                        }
                        if (course.Stream != trainer.Subject)
                        {
                            check = true;
                            Console.WriteLine("Trainer is not familiar with the subject of the course!Try again");
                        }
                    }
                } while (check);

                Courses_Trainers courses_Trainers1 = new Courses_Trainers();
                courses_Trainers1.CourseId = temp;
                courses_Trainers1.TrainerId = trainer.TrainerId;
                CreateData<Courses_Trainers>(courses_Trainers1, "Enroll");
                Console.Write("Do you want to add course to this student?:<Y> or <N>?:\n>");
            } while (YesOrNo());
        }

    }
}
