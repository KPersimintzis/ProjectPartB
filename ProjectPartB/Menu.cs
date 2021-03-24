using System;
using System.Collections.Generic;
using System.Linq;
using ProjectPartB.Data;
using ProjectPartB.Entities;
using ProjectPartB.Services;
using ProjectPartB.ViewModels;

namespace ProjectPartB
{
    class Menu : InputUser
    {
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\tPrivate School");
            Console.WriteLine("Select an option from menu: ");
            Console.Write("0.Exit\n1.Courses\n2.Trainers\n3.Students\n4.Assignments\n\n------\n>");
            int temp = Integer(0, 4);
            switch (temp)
            {

                case 0:
                    {
                        Console.WriteLine("\nExiting...\n");
                        Console.WriteLine("Are you sure?<Y> or <N>");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...\n");
                            break;
                        }
                        return;
                    }
                case 1:
                    {
                        Console.Clear();
                        CoursesMenu();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        TrainersMenu();
                        return;
                    }
                case 3:
                    {
                        Console.Clear();
                        StudentsMenu();
                        break;
                    }
                case 4:
                    {
                        Console.Clear();
                        AssignmentsMenu();
                        break;
                    }
            }
        }
        public void CoursesMenu()
        {
            Console.Clear();
            Console.WriteLine("Select an option from Courses menu: ");
            Console.Write("0.Back to Main Menu\n1.Insert\n2.Display\n3.Exit\n\n------\n>");
            int temp = Integer(0, 3);
            switch (temp)
            {
                case 0:
                    {
                        MainMenu();
                        break;
                    }
                case 1:
                    {
                        new CourseService().Create();
                        Console.Write("Press any button to continue...");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        List<Course> courses = new CourseService().GetList();
                        CourseView courseView = new CourseView();
                        courseView.Display(courses);
                        Console.Write("If you want more info about each course please type its id:\nPress 0 to go back\n\n------\n>");
                        temp = IntegerId<Course>(courses);
                        if (temp != 0)
                        { courses = new CourseService().GetListAnalytic(courses); }
                        while (temp != 0)
                        {
                            Course course = courses.FirstOrDefault(x => x.CourseId == temp);
                            courseView.DisplayById(course);
                            Console.WriteLine("Press any button to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            courseView.Display(courses);
                            Console.Write("If you want more info about each course please type its id:\nPress 0 to go back\n\n------\n>");
                            temp = IntegerId<Course>(courses);
                        }
                        CoursesMenu();
                        break;
                    }

                case 3:
                    {
                        Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...\n");
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            CoursesMenu();
                            break;
                        }
                    }
            }
        }
        public void TrainersMenu()
        {
            Console.Clear();
            Console.WriteLine("Select an option from Trainers menu: ");
            Console.Write("0.Back to Main Menu\n1.Insert\n2.Display\n3.Exit\n\n------\n>");
            int temp = Integer(0, 3);
            switch (temp)
            {
                case 0:
                    {
                        MainMenu();
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Select an option from Insert menu: ");
                        Console.Write("0.Back to Main Menu\n1.Create new Trainer" +
                            "\n2.Insert a Trainer in Course" +
                            "\n3.Exit\n\n------\n>");
                        int temp1 = Integer(0, 3);
                        switch (temp1)
                        {
                            case 0: { MainMenu(); break; }
                            case 1:
                                {
                                    new TrainerService().Create();
                                    Console.Write("Press any button to continue...");
                                    Console.ReadKey();
                                    MainMenu();
                                    break;
                                }
                            case 2:
                                {
                                    Console.Clear();
                                    List<Trainer> trainers = new TrainerService().SetList();
                                    TrainerView trainerView = new TrainerView();
                                    trainerView.Display(trainers);
                                    do
                                    {
                                        Console.Write("Choose a trainer from id to insert in a course:\nPress 0 to go back\n\n------\n>");
                                        temp = IntegerId<Trainer>(trainers);
                                        if (temp == 0)
                                        { Console.WriteLine("Process Terminated"); MainMenu(); }
                                        Trainer trainer = trainers.FirstOrDefault(x => x.TrainerId == temp);
                                        Console.Clear();
                                        Console.Write($"Trainer: {trainer.TrainerId} {trainer.FirstName} {trainer.LastName} {trainer.Subject}\nChoose a course:\n\n------\n>");
                                        new Courses_Trainers().SetCrs_TrainerId(trainer);
                                        Console.Write("Do you want to add trainer to other course?:<Y> or <N>?:\n>");
                                    } while (YesOrNo());
                                    MainMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                    {
                                        Console.WriteLine("\nExiting...\n");
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        StudentsMenu();
                                        break;
                                    }
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        List<Trainer> trainers = new TrainerService().SetList();
                        TrainerView trainerView = new TrainerView();
                        trainerView.Display(trainers);
                        Console.Write("If you want more info about each trainer please type its id:\nPress 0 to go back\n\n------\n>");
                        temp = IntegerId<Trainer>(trainers);
                        if (temp != 0) { trainers = new TrainerService().SetListAnalytic(trainers); }
                        while (temp != 0)
                        {
                            Trainer trainer = trainers.FirstOrDefault(x => x.TrainerId == temp);
                            trainerView.DisplayById(trainer);
                            Console.WriteLine("Press any button to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            trainerView.Display(trainers);
                            Console.Write("If you want more info about each trainer please type its id:\nPress 0 to go back\n\n------\n>");
                            temp = IntegerId<Trainer>(trainers);
                        }
                        MainMenu();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...\n");
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            TrainersMenu();
                            break;
                        }
                    }
            }
        }
        public void StudentsMenu()
        {
            Console.Clear();
            Console.WriteLine("Select an option from Students menu: ");
            Console.Write("0.Back to Main Menu\n1.Insert\n2.Display\n3.Exit\n\n------\n>");
            int temp = Integer(0, 3);
            switch (temp)
            {
                case 0:
                    {
                        MainMenu();
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Select an option from Insert menu: ");
                        Console.Write("0.Back to Main Menu\n1.Create new Student" +
                            "\n2.Insert a Student in Course" +
                            "\n3.Exit\n\n------\n>");
                        int temp1 = Integer(0, 3);
                        switch (temp1)
                        {
                            case 0: { MainMenu(); break; }
                            case 1:
                                {
                                    new StudentService().Create();
                                    Console.Write("Press any button to continue...");
                                    Console.ReadKey();
                                    MainMenu();
                                    break;
                                }
                            case 2:
                                {
                                    Console.Clear();
                                    List<Student> students = new StudentService().GetList();
                                    StudentView studentView = new StudentView();
                                    studentView.Display(students);
                                    do
                                    {
                                        Console.Write("Choose a student from id to insert in a course:\nPress 0 to go back\n\n------\n>");
                                        temp = IntegerId<Student>(students);
                                        if (temp == 0)
                                        { Console.WriteLine("Process Terminated"); MainMenu(); }
                                        Student student = students.FirstOrDefault(x => x.StudentId == temp);
                                        Console.Clear();
                                        Console.Write($"Student: {student.StudentId} {student.FirstName} {student.LastName}\nChoose a course:\n\n------\n>");
                                        new Enroll().SetEnrollmentById(student);
                                        Console.Write("Do you want to add course to other student?:<Y> or <N>?:\n>");
                                    } while (YesOrNo());
                                    MainMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                    {
                                        Console.WriteLine("\nExiting...\n");
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        StudentsMenu();
                                        break;
                                    }
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Select an option from Display menu: ");
                        Console.Write("0.Back to Main Menu\n1.Display all the Students" +
                            "\n2.Display the Student who belong to more than a course" +
                            "\n3.Exit\n\n------\n>");
                        int temp2 = Integer(0, 3);
                        switch (temp2)
                        {
                            case 0: { MainMenu(); break; }
                            case 1:
                                {
                                    Console.Clear();
                                    List<Student> students = new StudentService().GetList();
                                    StudentView studentView = new StudentView();
                                    studentView.Display(students);
                                    Console.Write("If you want more info about each student please type its id:\nPress 0 to go back\n\n------\n>");
                                    temp = IntegerId<Student>(students);
                                    if (temp != 0) { students = new StudentService().GetListAnalytic(students); }
                                    while (temp != 0)
                                    {
                                        Student student = students.FirstOrDefault(x => x.StudentId == temp);
                                        studentView.DisplayById(student);
                                        Console.Write("Press any button to continue...");
                                        Console.ReadKey();
                                        Console.Clear();
                                        studentView.Display(students);
                                        Console.Write("If you want more info about each student please type its id:\nPress 0 to go back\n\n------\n>");
                                        temp = IntegerId<Student>(students);
                                    }
                                    MainMenu();
                                    break;
                                }
                            case 2:
                                {
                                    List<Student> students = new StudentService().GetList();
                                    students = new StudentService().GetListAnalytic(students);
                                    StudentView studentView = new StudentView();
                                    studentView.DisplayMoreThanAClass(students);
                                    Console.WriteLine("Press any button to continue...");
                                    Console.ReadKey();
                                    StudentsMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                    {
                                        Console.WriteLine("\nExiting...\n");
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        StudentsMenu();
                                        break;
                                    }
                                }
                        }
                        break;
                    }

                case 3:
                    {
                        Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...\n");
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            StudentsMenu();
                            break;
                        }
                    }
            }
        }
        public void AssignmentsMenu()
        {
            Console.Clear();
            Console.WriteLine("Select an option from Assignments menu: ");
            Console.Write("0.Back to Main Menu\n1.Insert\n2.Display\n3.Exit\n\n------\n>");
            int temp = Integer(0, 3);
            switch (temp)
            {
                case 0:
                    {
                        MainMenu();
                        break;
                    }
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Select an option from Insert menu: ");
                        Console.Write("0.Back to Main Menu\n1.Create new Assignment" +
                            "\n2.Assign an Assignment to a course" +
                            "\n3.Assign an Assignment to a student" +
                            "\n4.Exit\n\n------\n>");
                        int temp1 = Integer(0, 3);
                        switch (temp1)
                        {
                            case 0: { MainMenu(); break; }
                            case 1:
                                {
                                    new AssignmentService().Create();
                                    Console.Write("Press any button to continue...");
                                    Console.ReadKey();
                                    MainMenu();
                                    break;
                                }
                            case 2:
                                {
                                    Console.Clear();
                                    List<Assignment> assignments = new AssignmentService().SetList();
                                    AssignmentView assignmentView = new AssignmentView();
                                    assignmentView.Display(assignments);
                                    do
                                    {
                                        Console.Write("Choose an assignment id to insert in a course:\nPress 0 to go back\n\n------\n>");
                                        temp = IntegerId<Assignment>(assignments);
                                        if (temp == 0)
                                        {
                                            Console.WriteLine("Process Terminated"); MainMenu();
                                        }

                                        Assignment assignment = assignments.FirstOrDefault(x => x.AssignmentId == temp);
                                        Console.Clear();
                                        new Course_Assignment().InsertCourseByAssignment(assignment);
                                        Console.Write("Do you want to add other assignment to other course?:<Y> or <N>?:\n>");
                                    } while (YesOrNo());
                                    MainMenu();
                                    break;
                                }
                            case 3:
                                {
                                    Console.Clear();
                                    List<Assignment> assignments = new AssignmentService().SetList();
                                    AssignmentView assignmentView = new AssignmentView();
                                    assignmentView.Display(assignments);
                                    do
                                    {
                                        Console.Write("Choose an assignment id to insert in a student:\nPress 0 to go back\n\n------\n>");
                                        IntegerId<Assignment>(assignments);
                                        if (temp == 0)
                                        {
                                            Console.WriteLine("Process Terminated"); MainMenu();
                                        }
                                        Assignment assignment = assignments.FirstOrDefault(x => x.AssignmentId == temp);
                                        Console.Clear();
                                        new Grade().InsertAssignmentPerCoursePerStudent(assignment);
                                        Console.Write("Do you want to add other assignment to other student?:<Y> or <N>?:\n>");
                                    } while (YesOrNo());
                                    Console.ReadLine();
                                    MainMenu();
                                    break;
                                }





                            case 4:
                                {
                                    Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                                    {
                                        Console.WriteLine("\nExiting...\n");
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        StudentsMenu();
                                        break;
                                    }
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        List<Assignment> assignments = new AssignmentService().SetList();
                        AssignmentView assignmentView = new AssignmentView();
                        assignmentView.Display(assignments);
                        Console.Write("If you want more info about each assignment please type its id:\nPress 0 to go back\n\n------\n>");
                        temp = IntegerId<Assignment>(assignments);
                        if (temp != 0) { assignments = new AssignmentService().SetListAnalytic(assignments); }
                        while (temp != 0)
                        {
                            Assignment assignment = assignments.FirstOrDefault(x => x.AssignmentId == temp);
                            assignmentView.DisplayById(assignment);
                            Console.Write("Press any button to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            assignmentView.Display(assignments);
                            Console.Write("If you want more info about each assignmnet please type its id:\nPress 0 to go back\n\n------\n>");
                            temp = IntegerId<Assignment>(assignments);
                        }
                        MainMenu();
                        break;

                    }
                case 3:
                    {
                        Console.WriteLine("Are you sure want to close the program? <Y> or <N>?");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nExiting...\n");
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            AssignmentsMenu();
                            break;
                        }
                    }
            }
        }
    }

}