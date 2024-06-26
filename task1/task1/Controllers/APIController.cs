﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using task1.Models;
using task1.Models.Errors;
using task1.Services;
using task1.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace task1.Controllers
{
    [Route("api/v1/task7")]
    [ApiController]
    public class APIController : ControllerBase
    {
        IHomeService homeService = new HomeServiceImpl();
        FormModel model1 = new FormModel();
        // GET: api/<APIController>
        [HttpGet]
        public IActionResult Get(string input, string sortMethod)
        {
            JSONHelper jh = new JSONHelper();
            jh.Deserialize();
            if (jh.Settings.ParallelLimit <= RequestCounter.CurrentRequestCount)
            {
                return new StatusCodeResult(503);
            }
            RequestCounter.CurrentRequestCount++;
            var options1 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            FormModel model = new FormModel(sortMethod, input);
            model1 = model;
            model = homeService.ParseModel(model);
            string json;

            if (!(model.Error is null))
            {
                Error error = new Error(
                    400,
                    "Bad request. Некорректный запрос. " + model.Error
                    );
                json = JsonSerializer.Serialize(error, options1);
                return new BadRequestObjectResult(json);
            }
            
            json = JsonSerializer.Serialize(model, options1);

            return new OkObjectResult(json);
        }
    }
}
