using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.Messages;
using Mindscape.Raygun4Net.WebApi.Messages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace CoursesAPI.Loggers
{
    public class RaygunExceptionLogger : ExceptionLogger
    {
        private readonly string _apiKey;

        public RaygunExceptionLogger()
            : this(ConfigurationManager.AppSettings["RaygunAppKey"])
        { }

        public RaygunExceptionLogger(string apiKey)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");
            _apiKey = apiKey;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            //Create instance of Logging API and log the exception with API
            RaygunClient _client = new RaygunClient("AWfIiFRrK4uswSJQUw5++g==");
            _client.Send(context.Exception);
        }
    }
}