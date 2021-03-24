using ProjectPartB.Data;
using ProjectPartB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectPartB.Services
{
    class StudentService : Student
    {
        private readonly string query = "SELECT * FROM Students";

        public List<Student> GetList()
        {
            List<Student> students = new List<Student>();
            students = GetAll<Student>(query);
            return students;
        }
        public List<Student> GetListAnalytic(List<Student> students)
        {
            List<Course> courses = new CourseService().GetList();
            new Enroll().SetEnrollment(courses, students);
            List<Assignment> assignments = new AssignmentService().SetList();
            new Grade().GetAssignmentPerStudent(students, assignments);
            return students;
        }

        public void Create()
        {
            Student student = new Student();
            do
            {
                NameInsert(student);
                if (Exit(student.FirstName) || Exit(student.LastName)) { return; }
            } while (CheckEquality(student));
            DatesInsert(student);

            CreateData(student, "Students");
        }

        public void NameInsert(Student student)
        {
            Console.Clear();
            Console.WriteLine("You can quit if you type 'exit'");
            do
            {
                Console.WriteLine("Give First Name:");
                student.FirstName = String().Trim();
                if (Exit(student.FirstName)) { student.FirstName = "exit"; return; }
                Console.Write($"You gave this first name {student.FirstName}\nDo you want to proceed? <Y> or <N>?:\n");
            } while (!YesOrNo());
            do
            {
                Console.WriteLine("Give Last Name:");
                student.LastName = String().Trim();
                if (Exit(student.LastName)) { student.LastName = "exit"; return; }
                Console.Write($"You gave this last name {student.LastName}\nDo you want to proceed? <Y> or <N>?:\n");
            } while (!YesOrNo());
        }

        public void DatesInsert(Student student)
        {
            Console.Clear();
            Console.WriteLine($"Give the Date of Birth for {student.FirstName} {student.LastName}");

            Console.Write($"From: {DateTime.Now.AddYears(-80):d} To: {DateTime.Now.AddYears(-18):d} \n\n (dd/MM/yyyy)\n>");
            student.DateOfBirth = Dates(DateTime.Now.AddYears(-80), DateTime.Now.AddYears(-18));
        }

        private bool CheckEquality(Student student)
        {
            List<Student> students = new List<Student>();
            students = GetAll<Student>(query);
            Student other = students.FirstOrDefault(x => x.FirstName == student.FirstName && x.LastName == student.LastName);
            if (other is Student)
            {
                Console.WriteLine("Student already exists!\n\n");
                Console.Write("Press any button to continue...");
                Console.ReadKey();
                return true;
            }
            return false;
        }


    }
}
