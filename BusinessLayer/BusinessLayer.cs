using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IStandardRepository _standardRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public BusinessLayer()
        {
            _standardRepository = new StandardRepository();
            _teacherRepository = new TeacherRepository();
            _courseRepository = new CourseRepository();
        }

        #region Standard
        public IEnumerable<Standard> GetAllStandards()
        {
            return _standardRepository.GetAll();
        }

        public Standard GetStandardByID(int id)
        {
            return _standardRepository.GetById(id);
        }

        public Standard GetStandardByName(string name)
        {
            return _standardRepository.GetSingle(
                s => s.StandardName.Equals(name),
                s => s.Students);
        }

        public void AddStandard(Standard standard)
        {
            _standardRepository.Insert(standard);
        }

        public void UpdateStandard(Standard standard)
        {
            _standardRepository.Update(standard);
        }

        public void RemoveStandard(Standard standard)
        {
            _standardRepository.Delete(standard);
        }
        #endregion

        #region Teacher
        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAll();
        }

        public Teacher GetTeacherByID(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public Teacher GetTeacherByName(string name)
        {
            return _teacherRepository.GetSingle(
                s => s.TeacherName.Equals(name),
                s => s.Standard);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.Insert(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.Update(teacher);
        }

        public void RemoveTeacher(Teacher teacher)
        {
            _teacherRepository.Delete(teacher);
        }
        #endregion

        #region Course
        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Course GetCourseByID(int id)
        {
            return _courseRepository.GetById(id);
        }

        public Course GetCourseByName(string name)
        {
            return _courseRepository.GetSingle(
                s => s.CourseName.Equals(name),
                s => s.Teacher);
        }

        public void AddCourse(Course course)
        {
            _courseRepository.Insert(course);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }

        public void RemoveCourse(Course course)
        {
            _courseRepository.Delete(course);
        }
        #endregion
    }
}