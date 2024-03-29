﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<CourseTemplate> _courseTemplates;
        private readonly IRepository<Grade> _grades;
		private readonly IRepository<Person> _persons;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<ProjectGroup> _projectGroups;
        private readonly IRepository<Semester> _semesters;
        private readonly IRepository<TeacherRegistration> _teacherRegistrations;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
            _grades               = _uow.GetRepository<Grade>();
			_persons              = _uow.GetRepository<Person>();
            _projects             = _uow.GetRepository<Project>();
            _projectGroups        = _uow.GetRepository<ProjectGroup>();
            _semesters            = _uow.GetRepository<Semester>();
            _teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
		}

        /// <summary>
        /// Gets a list of all teachers in given course.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of teachers.</returns>
		public List<Person> GetCourseTeachers(string semester)
		{
            var teachers = (from p in _persons.All()
                            join t in _teacherRegistrations.All() on p.SSN equals t.SSN
                            join c in _courseInstances.All() on t.CourseInstanceID equals c.ID
                            where c.SemesterID == semester
                            select p).ToList();

			return teachers;
		}

        /// <summary>
        /// Gets all courses taught in a given semester.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of courses.</returns>
		public List<CourseInstanceDTO> GetSemesterCourses(string semester)
		{
            var courses = from c in _courseInstances.All()
                          where c.SemesterID == semester
                          select new CourseInstanceDTO()
                          {
                              Name = c.Courses.Name,
                              CourseInstanceID = c.ID,
                              CourseID = c.CourseID,
                              Description = c.Courses.Description
                          };

            return courses.ToList();
		}

        /// <summary>
        /// Gets main teacher in a given course and semester,
        /// default empty if there is none.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of main teachers.</returns>
        public List<CourseInstanceDTO> GetTeacherInCourse(string semester)
        {
            var courseWithTeacher = from c in _courseInstances.All()
                                    join MainTeacher in
                                    (
                                        from t in _teacherRegistrations.All()
                                        join p in _persons.All() on t.SSN equals p.SSN
                                        where t.Type == 1
                                        select new { p.Name, t.CourseInstanceID }
                                    )
                                    on c.ID equals MainTeacher.CourseInstanceID into CoursesAndTeacher
                                    from item in CoursesAndTeacher.DefaultIfEmpty()
                                    where c.SemesterID == semester
                                    select new CourseInstanceDTO
                                    {
                                        CourseID = c.Courses.CourseID,
                                        CourseInstanceID = c.ID,
                                        MainTeacher = item == null ? "" : item.Name,
                                        Name = c.Courses.Name
                                    };

            return courseWithTeacher.ToList();
        }
	}
}
