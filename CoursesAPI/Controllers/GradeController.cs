﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoursesAPI.Services;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Controllers
{
    /// <summary>
    /// GradeController represents resources belonging to grades.
    /// </summary>
    [RoutePrefix("api/courses/{courseInstanceID}")]
    public class GradeController : ApiController
    {
        private readonly GradeServiceProvider _service;

        public GradeController()
        {
            _service = new GradeServiceProvider(new UnitOfWork<AppDataContext>());
        }

        [HttpPost]
        [Route("")]
        public Project PostProject(ProjectCreateViewModel model)
        {
            _service.AddProject(model);
        }
    }
}
