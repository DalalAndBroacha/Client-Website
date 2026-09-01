using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PieReports.Models;
using System.Collections.Generic;
using System.Security.Policy;
using ViewModel.ClientComms;
using ViewModel.Login;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace PieReports.Filters
{
    public class SessionTimeoutFilter : ActionFilterAttribute
    {

        private readonly IConfiguration _config;

        public SessionTimeoutFilter(IConfiguration configuration)
        {
            _config = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tempData = context.HttpContext.RequestServices.GetRequiredService<ITempDataDictionaryFactory>()
                            .GetTempData(context.HttpContext);

            string url = _config.GetValue<string>("APIKey") + "Services/psp_dsp_user_access_token";

            if (!tempData.ContainsKey("myFinyearsdata"))
            {
                context.Result = new RedirectToActionResult("Index", "Login", new { msg = "Session Expired."});
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Logic to execute after the action method
        }

        /*
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Example condition: Redirect if a specific query parameter is missing
            if (!context.HttpContext.Request.Query.ContainsKey("someParam"))
            {
                // Set the Result property to a RedirectToActionResult
                context.Result = new RedirectToActionResult("TargetAction", "TargetController", null);

                // Important: Do NOT call base.OnActionExecuting(context) or await next()
                // after setting context.Result, as it will short-circuit the pipeline.
                return;
            }

            base.OnActionExecuting(context); // Call the base implementation if no redirection is needed
        }
        */
    }
}
