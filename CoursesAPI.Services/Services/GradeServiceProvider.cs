using CoursesAPI.Services.DataAccess;
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
                ID = model.Id,
                Name = model.Name,
                GradeProjectsCount = model.GradeprojectsCount
            };

            return pg;
        }
        //TODO
        public void GetGroupGrade(int ID)
        {
            var theGroup = (from h in _projectGroups.All()
                           where (h.ID == ID)
                           select h).Single();

            int howMany = theGroup.GradeProjectsCount;

            var allProjects = (from r in _projects.All()
                          where (r.ProjectGroupID == ID)
                          select r).ToList();

            
            

        }
    }
}
