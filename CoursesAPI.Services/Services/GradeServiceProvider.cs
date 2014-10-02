﻿using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Services
{
    public class GradeServiceProvider
    {
        private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<CourseTemplate> _courseTemplates;
        private readonly IRepository<Grade> _grades;
		private readonly IRepository<Person> _persons;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<ProjectGroup> _projectGroups;
        private readonly IRepository<Semester> _semesters;

		public GradeServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
            _grades               = _uow.GetRepository<Grade>();
			_persons              = _uow.GetRepository<Person>();
            _projects             = _uow.GetRepository<Project>();
            _projectGroups        = _uow.GetRepository<ProjectGroup>();
            _semesters            = _uow.GetRepository<Semester>();
		}

        public Project AddProject(ProjectCreateViewModel model)
        {
            var p = new Project
            {
                Name = model.Name,
                Weight = model.Weight,
                MinGradeToPassCourse = model.MinGradeToPassCourse,
                CourseInstanceID = model.CourseInstanceID,
                ProjectGroupID = model.ProjectGroupID,
                OnlyHigherThanProjectID = model.OnlyHigherThanProjectID
            };

            _projects.Add(p);
            _uow.Save();
            return p;
        }

        public Grade AddGrade(GradeCreateViewModel model)
        {
            var g = new Grade
            {
                PersonID = model.PersonID,
                ProjectID = model.ProjectID,
                StudentGrade = model.StudentGrade
            };

            _grades.Add(g);
            _uow.Save();

            return g;
        }

        public ProjectGroup AddProjectGroup(ProjectGroupCreateViewModel model)
        {
            var pg = new ProjectGroup
            {
                ID = model.ID,
                Name = model.Name,
                GradedProjectsCount = model.GradedProjectsCount
            };

            _projectGroups.Add(pg);
            _uow.Save();

            return pg;
        }
        
        // Helpfunction to get X of Y best grades in
        // a project group.
        public float GetGroupGrade(int ID)
        {
            var theGroup = (from h in _projectGroups.All()
                           where (h.ID == ID)
                           select h).SingleOrDefault();

            int howMany = theGroup.GradedProjectsCount;

            var allProjects = (from g in _grades.All()
                               join p in _projects.All() on g.ProjectID equals p.ID 
                               orderby g.StudentGrade descending
                               where p.ProjectGroupID == ID
                               select g).Take(howMany).ToList();

            return allProjects.Average(x => x.StudentGrade);
        }

        public float GetFinalGrade(String StudentID)
        {
            float finalGrade = 0;

            var allBasicGrades = (from g in _grades.All()
                             join p in _projects.All() on g.ProjectID equals p.ID 
                             where (g.PersonID == StudentID 
                                    && p.ProjectGroupID == null
                                    && p.OnlyHigherThanProjectID == null)
                             select g).ToList();

            foreach(var grade in allBasicGrades)
            {
                finalGrade =+ (grade.StudentGrade);
            }

            var allGroupGrades = (from g in _grades.All()
                                  join p in _projects.All() on g.ProjectID equals p.ID
                                  where (g.PersonID == StudentID
                                         && p.ProjectGroupID != null
                                         && p.OnlyHigherThanProjectID == null)
                                         orderby (p.ProjectGroupID) descending
                                  select g).ToList();

            float previous = -1;
            foreach (var grade in allGroupGrades)
            {
                if(grade.ProjectID == previous)
                {
                    continue;
                }

                finalGrade =+ GetGroupGrade(grade.ProjectID);
                previous = grade.ProjectID;
            }

            var allHigherThanProjects = (from g in _grades.All()
                                  join p in _projects.All() on g.ProjectID equals p.ID
                                  where (g.PersonID == StudentID
                                         && p.ProjectGroupID == null
                                         && p.OnlyHigherThanProjectID != null)
                                  select p).ToList();

            foreach (var proj in allHigherThanProjects)
            {
                var grade1 = (from g in _grades.All()
                              where g.ProjectID == proj.ID
                              select g.StudentGrade).SingleOrDefault();

                var grade2 = (from g in _grades.All()
                              where g.ProjectID == proj.OnlyHigherThanProjectID
                              select g.StudentGrade).SingleOrDefault();

                if (grade1 > grade2)
                {

                }
            }

            return finalGrade;
        }
    }
}
