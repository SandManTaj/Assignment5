﻿using BusinessLayer;
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
        private static IBusinessLayer businessLayer = new BusinessLayer.BusinessLayer();
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
                Console.WriteLine("Would you like to (1) change teachers, (2) change courses, (3) make a query, or (4) exit?");
                line = Console.ReadLine();
                decision = int.Parse(line);
                if (decision == 1)
                {
                    Console.WriteLine("Would you like to (1) add, (2) delete, or (3) update a teacher?");
                    line = Console.ReadLine();
                    decision = int.Parse(line);
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
                        DisplayAllTeachers();
                        Console.WriteLine("Please enter the ID of the teacher you would like to remove");
                        line = Console.ReadLine();
                        id = int.Parse(line);
                        RemoveTeacher(id);
                        DisplayAllTeachers();
                        Console.ReadKey();
                    }
                    else if (decision == 3)
                    {
                        DisplayAllTeachers();
                        Console.WriteLine("Please enter the ID of the teacher you would like to update");
                        line = Console.ReadLine();
                        id = int.Parse(line);
                        UpdateTeacher(id);
                        DisplayAllTeachers();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey();
                    }
                }
                else if (decision == 2)
                {
                    Console.WriteLine("Would you like to (1) add, (2) delete, or (3) update?");
                    line = Console.ReadLine();
                    decision = int.Parse(line);
                    if (decision == 1)
                    {
                        DisplayAllTeachers();
                        Console.WriteLine("Please enter a name");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter a teacher ID");
                        line = Console.ReadLine();
                        id = int.Parse(line);
                        AddCourse(name, id);
                        DisplayAllCourses();
                        Console.ReadKey();
                    }
                    else if (decision == 2)
                    {
                        DisplayAllCourses();
                        Console.WriteLine("Please enter the ID of the course you would like to remove");
                        line = Console.ReadLine();
                        id = int.Parse(line);
                        RemoveCourse(id);
                        DisplayAllCourses();
                        Console.ReadKey();
                    }
                    else if (decision == 3)
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
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey();
                    }
                }
                else if (decision == 3)
                {
                    Console.WriteLine("Would you like to (1) Display all courses taught by Teacher using TeacherID, \n " +
                        "(2) display all Teachers, (3) display All Standards, (4) Display All Courses?");
                    line = Console.ReadLine();
                    decision = int.Parse(line);
                    if (decision == 1)
                    {
                        DisplayAllTeachers();
                        Console.WriteLine("Please enter the ID of the teacher whose courses you would like to view");
                        line = Console.ReadLine();
                        id = int.Parse(line);
                        DisplayCoursesByTeacher(id);
                        Console.ReadKey();
                    }
                    else if (decision == 2)
                    {
                        DisplayAllTeachers();
                        Console.ReadKey();
                    }
                    else if (decision == 3)
                    {
                        DisplayAllStandard();
                        Console.ReadKey();
                    }
                    else if (decision == 4)
                    {
                        DisplayAllCourses();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey();
                    }
                }
                else if (decision == 4)
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
            businessLayer.AddStandard(standard);
            teacher.TeacherName = name;
            teacher.StandardId = standard.StandardId;
            businessLayer.AddTeacher(teacher);
        }

        public static void UpdateTeacher(int id)
        {
            Teacher teacher;
            try
            {
                teacher = businessLayer.GetTeacherByID(id);
                Console.WriteLine("Please enter a new name");
                teacher.TeacherName = Console.ReadLine();
                businessLayer.UpdateTeacher(teacher);
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
                Teacher teacher = businessLayer.GetTeacherByID(id);
                businessLayer.RemoveTeacher(teacher);
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
                Teacher teacher = businessLayer.GetTeacherByID(id);
                course.CourseName = name;
                course.TeacherId = id;
                businessLayer.AddCourse(course);
            }
            catch(Exception e)
            {
                Console.WriteLine("This ID does not exist");
            }

        }
        public static void RemoveCourse(int id)
        {
            try
            {
                Course course = businessLayer.GetCourseByID(id);
                businessLayer.RemoveCourse(course);
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
                course = businessLayer.GetCourseByID(id);
                course.CourseName = name;
                businessLayer.UpdateCourse(course);
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
            IEnumerable<Standard> standards = businessLayer.GetAllStandards();
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
            IEnumerable<Teacher> teachers = businessLayer.GetAllTeachers();
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
                Teacher teacher = businessLayer.GetTeacherByID(id);
                IEnumerable<Course> courses = businessLayer.GetAllCourses();
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
            IEnumerable<Course> standards = businessLayer.GetAllCourses();
            foreach (Course c in standards)
            {
                if (c != null)
                    Console.WriteLine("Teacher ID: {0}, Course Name: {1}, Course ID: {2}", c.TeacherId, c.CourseName, c.CourseId);
            }
        }
        #endregion

    }
}

