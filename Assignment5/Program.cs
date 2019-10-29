using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class Program
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        static void Main(string[] args)
        {
            bool cont = true;
            int decision = 0;
            int id = 0;
            string line = "";
            string name = "";
            string description = "";
            while (cont == true)
            {
                Console.WriteLine("Please select an option:\n" +
                    "(1) Add Teacher\n" +
                    "(2) Update Teacher\n" +
                    "(3) Delete Teacher\n" +
                    "(4) Add Course\n" +
                    "(5) Update Course Name\n" +
                    "(6) Update Course Teacher\n" +
                    "(7) Delete Course\n" +
                    "(8) Display all courses taught by a teacher\n" +
                    "(9) Display all teachers\n" +
                    "(10) Display all courses\n" +
                    "(11) Display all standards\n" +
                    "(12) Exit\n");
                line = Console.ReadLine();
                decision = int.Parse(line);
                Console.Clear();
                DisplayAllTeachers();
                if (decision == 1)
                {
                    Console.WriteLine("Please enter a name");
                    name = Console.ReadLine();
                    Console.WriteLine("Please enter a description");
                    description = Console.ReadLine();
                    AddTeacher(name, description);
                    DisplayAllTeachers();
                    Console.ReadKey();
                }
                else if (decision == 2)
                {
                    Console.WriteLine("Please enter the ID of the teacher you would like to update");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    UpdateTeacher(id);
                    DisplayAllTeachers();
                    Console.ReadKey();
                }
                else if (decision == 3)
                {
                    Console.WriteLine("Please enter the ID of the teacher you would like to remove");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    RemoveTeacher(id);
                    DisplayAllTeachers();
                    Console.ReadKey();
                }
                else if (decision == 4)
                {
                    Console.WriteLine("Please enter a name");
                    name = Console.ReadLine();
                    Console.WriteLine("Please enter a teacher ID");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    AddCourse(name, id);
                    DisplayAllCourses();
                    Console.ReadKey();
                }
                else if (decision == 5)
                {
                    DisplayAllCourses();
                    Console.WriteLine("Please enter an course ID");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    Console.WriteLine("Please enter a new name");
                    name = Console.ReadLine();
                    UpdateCourse(id, name);
                    DisplayAllCourses();
                    Console.ReadKey();
                }
                else if (decision == 6)
                {
                    Console.WriteLine("Please enter the ID of the Teacher");
                    line = Console.ReadLine();
                    int tid = int.Parse(line);
                    DisplayAllCourses();
                    Console.WriteLine("Please enter the ID of the course you would like to add to the teacher");
                    line = Console.ReadLine();
                    int cid = int.Parse(line);
                    AddExistingCourseToTeacher(tid, cid);
                    DisplayCoursesByTeacher(tid);
                    Console.ReadKey();
                }
                else if (decision == 7)
                {
                    DisplayAllCourses();
                    Console.WriteLine("Please enter the ID of the course you would like to remove");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    RemoveCourse(id);
                    DisplayAllCourses();
                    Console.ReadKey();
                }
                else if (decision == 8)
                {
                    Console.WriteLine("Please enter the ID of the teacher whose courses you would like to view");
                    line = Console.ReadLine();
                    id = int.Parse(line);
                    DisplayCoursesByTeacher(id);
                    Console.ReadKey();
                }
                else if (decision == 9)
                {
                    //DisplayAllTeachers();
                    Console.ReadKey();
                }
                else if (decision == 10)
                {
                    DisplayAllCourses();
                    Console.ReadKey();
                }
                else if (decision == 11)
                {
                    DisplayAllStandard();
                    Console.ReadKey();
                }
                else if (decision == 12)
                {
                    cont = false;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    Console.ReadKey();
                }
                Console.Clear();
            }


        }

        #region Teacher
        public static void AddTeacher(string name, string description)
        {
            Teacher teacher = new Teacher();
            Standard standard = new Standard();

            standard.StandardName = name;
            standard.Description = description;
            unitOfWork.StandardRepository.Insert(standard);
            unitOfWork.Save();
            teacher.TeacherName = name;
            teacher.StandardId = standard.StandardId;
            unitOfWork.TeacherRepository.Insert(teacher);
            unitOfWork.Save();
        }

        public static void UpdateTeacher(int id)
        {
            Teacher teacher;
            try
            {
                teacher = unitOfWork.TeacherRepository.GetById(id);
                Console.WriteLine("Please enter a new name {0}", id);
                teacher.TeacherName = Console.ReadLine();
                unitOfWork.TeacherRepository.Update(teacher);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }

        }

        public static void RemoveTeacher(int id)
        {
            try
            {
                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);
                Standard standard = unitOfWork.StandardRepository.GetById(teacher.StandardId ?? -1);
                unitOfWork.StandardRepository.Delete(standard);
                unitOfWork.TeacherRepository.Delete(teacher);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }
        }

        #endregion

        #region Course
        public static void AddCourse(string name, int id)
        {
            try
            {
                Course course = new Course();
                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);
                course.CourseName = name;
                course.TeacherId = id;
                unitOfWork.CourseRepository.Insert(course);
                unitOfWork.Save();
            }
            catch(Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }

        }

        public static void AddExistingCourseToTeacher(int tid, int cid)
        {
            try
            {
                Course course = unitOfWork.CourseRepository.GetById(cid);
                course.TeacherId = tid;
                unitOfWork.CourseRepository.Insert(course);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }

        }
        public static void RemoveCourse(int id)
        {
            try
            {
                Course course = unitOfWork.CourseRepository.GetById(id);
                unitOfWork.CourseRepository.Delete(course);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }
        }

        public static void UpdateCourse(int id, string name)
        {
            Course course;
            try
            {
                course = unitOfWork.CourseRepository.GetById(id);
                course.CourseName = name;
                unitOfWork.CourseRepository.Update(course);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }

        }
        
        #endregion

        #region Queries
        public static void DisplayAllStandard()
        {
            IEnumerable<Standard> standards = unitOfWork.StandardRepository.GetAll();
            foreach (Standard s in standards)
            {
                if (s != null)
                {
                    Console.WriteLine("Standard ID: {0}, Name: {1}, Description: {2}", s.StandardId, s.StandardName, s.Description);
                }
            }

        }

        public static void DisplayAllTeachers()
        {
            IEnumerable<Teacher> teachers = unitOfWork.TeacherRepository.GetAll();
            foreach (Teacher t in teachers)
            {
                if (t != null)
                {
                    Console.WriteLine("Teacher ID: {0},\t Name: {1},\t Standard ID: {2}\t", t.TeacherId, t.TeacherName, t.StandardId);
                }
            }
        }

        public static void DisplayCoursesByTeacher(int id)
        {
            try
            {
                Teacher teacher = unitOfWork.TeacherRepository.GetById(id);
                IEnumerable<Course> courses = unitOfWork.CourseRepository.GetAll();
                foreach (Course c in courses)
                {
                    if (c.TeacherId == id)
                        Console.WriteLine("Teacher ID: {0}, Course ID: {1}, CourseName: {2}", c.TeacherId, c.CourseId, c.CourseName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }
        }

        public static void DisplayAllCourses()
        {
            IEnumerable<Course> courses = unitOfWork.CourseRepository.GetAll();
            foreach (Course c in courses)
            {
                if (c != null)
                    Console.WriteLine("Teacher ID: {0}, Course Name: {1}, Course ID: {2}", c.TeacherId, c.CourseName, c.CourseId);
            }
        }
        #endregion

    }
}

