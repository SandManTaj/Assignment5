using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class UnitOfWork: IDisposable
    {
        private SchoolDBEntities context = new SchoolDBEntities();
        private Repository<Standard> standardRepository;
        private Repository<Course> courseRepository;
        private Repository<Teacher> teacherRepository;

        public Repository<Standard> StandardRepository
        {
            get
            {
                if (this.standardRepository == null)
                {
                    this.standardRepository = new Repository<Standard>(context);
                }
                return standardRepository;
            }
        }

        public Repository<Course> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new Repository<Course>(context);
                }
                return courseRepository;
            }
        }

        public Repository<Teacher> TeacherRepository
        {
            get
            {
                if (this.teacherRepository == null)
                {
                    this.teacherRepository = new Repository<Teacher>(context);
                }
                return teacherRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
