using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPartB.Data;
using ProjectPartB.Entities;
namespace ProjectPartB.Services
{
    class CourseService : Course
    {
        private readonly string query = "SELECT * FROM Courses";

        public List<Course> GetList()
        {
            List<Course> courses = new List<Course>();
            courses = GetAll<Course>(query);
            List<Stream> streams = new StreamService().SetList();
            List<TypeCourse> types = new TypeService().SetList();
            foreach (Course c in courses)
            {
                Stream str = streams.FirstOrDefault(x => x.StreamId == c.StreamId);
                if (str is Stream)
                {
                    c.Stream = str.StreamName;
                }
                foreach (TypeCourse type in types)
                {
                    if (c.TypeId == type.TypeId)
                    {
                        c.Type = type.TypeName;
                        break;
                    }
                }
            }
            return courses;
        }
        public List<Course> GetListAnalytic(List<Course> courses)
        {
            List<Student> students = new StudentService().GetList();
            new Enroll().SetEnrollment(courses, students);
            List<Trainer> trainers = new TrainerService().SetList();
            new Courses_Trainers().SetCrs_Tr(courses, trainers);
            List<Assignment> assignments = new AssignmentService().SetList();
            new Course_Assignment().GetCourse_Assignment(courses, assignments);
            return courses;
        }
        public void Create()
        {
            Course course = new Course();
            do
            {
                course.Title = TitleInsert();
                if (Exit(course.Title)) { return; }
                course.StreamId = StreamInsert();
                if (course.StreamId == 0) { return; }
                course.TypeId = TypeInsert();
                if (course.TypeId == 0) { return; }
            } while (CheckEquality(course));
            DatesInsert(course);
            CreateData(course, "Courses");
        }
        public string TitleInsert()
        {
            Console.Clear();
            Console.WriteLine("You can quit if you type 'exit'");
            string title;
            do
            {
                Console.Write("Give a title to your course:\n> ");
                title = Console.ReadLine().Trim();
                if (Exit(title)) { return "exit"; }
                Console.Write($"You gave this title {title}\nDo you want to proceed ? <Y> or <N>?:\n");
            } while (!YesOrNo());
            return title;
        }
        public int StreamInsert()
        {
            Console.Clear();
            List<Stream> streams = new StreamService().SetList();
            Console.WriteLine($"Choose Stream for your Course({Title})");
            streams.ForEach(x => Console.WriteLine($"{x.StreamId}.{x.StreamName}"));
            Console.Write("------\n>");
            int temp = IntegerId<Stream>(streams);
            return temp;
        }
        public int TypeInsert()
        {
            Console.Clear();
            List<TypeCourse> typeCourses = new TypeService().SetList();
            Console.WriteLine($"Choose Type for your Course({Title})");
            typeCourses.ForEach(x => Console.WriteLine($"{x.TypeId}.{x.TypeName}"));
            Console.Write("------\n>");
            int temp = IntegerId<TypeCourse>(typeCourses);
            return temp;
        }
        public void DatesInsert(Course course)
        {
            Console.Clear();
            Console.WriteLine($"Choose Starting Date for your Course ({course.Title})");

            Console.Write($"From: {DateTime.Now:d} To: {DateTime.Now.AddMonths(12):d} \n\n (dd/MM/yyyy)\n>");
            course.StartDate = Dates(DateTime.Now, DateTime.Now.AddMonths(12));

            Console.Clear();
            Console.WriteLine($"Choose Ending Date for your Course ({course.Title})");

            Console.Write($"From: {course.StartDate:d} To: {course.StartDate.AddMonths(12):d} \n\n (dd/MM/yyyy)\n>");
            course.EndDate = Dates(course.StartDate, course.StartDate.AddMonths(12));
        }
        public bool CheckEquality(Course course)
        {
            List<Course> courses = new List<Course>();
            courses = GetAll<Course>(query);
            Course other = courses.FirstOrDefault(x => x.Title == course.Title &&
                                x.StreamId == course.StreamId && x.TypeId == course.TypeId);
            if (other is Course)
            {
                Console.WriteLine("Course already exists!\n\n");
                Console.Write("Press any button to continue...");
                Console.ReadKey();
                return true;
            }
            return false;
        }

    }
}

