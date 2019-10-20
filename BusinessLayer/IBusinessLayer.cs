using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        #region Standard
        IEnumerable<Standard> GetAllStandards();

        Standard GetStandardByID(int id);

        Standard GetStandardByName(string name);

        void AddStandard(Standard standard);

        void UpdateStandard(Standard standard);

        void RemoveStandard(Standard standard);
        #endregion

        #region Teacher
        IEnumerable<Teacher> GetAllTeachers();

        Teacher GetTeacherByID(int id);

        Teacher GetTeacherByName(string teacher);

        void AddTeacher(Teacher teacher);

        void UpdateTeacher(Teacher teacher);

        void RemoveTeacher(Teacher teacher);
        #endregion

        #region Course
        IEnumerable<Course> GetAllCourses();

        Course GetCourseByID(int id);

        Course GetCourseByName(string student);

        void AddCourse(Course student);

        void UpdateCourse(Course student);

        void RemoveCourse(Course student);
        #endregion

    }
}