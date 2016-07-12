using System.Collections.Generic;
using System.Linq;
using Api.Api.Models;
using Api.Shared.Options;
using Digipolis.Eventhandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : Controller
    {
        // Normally this data would come from an injected business or repository class
        private readonly List<Example> _examples = new List<Example>()
        {
            new Example() { Id = 1, Name = "Peter Parker" },
            new Example() { Id = 2, Name = "Clark Kent" },
            new Example() { Id = 3, Name = "Bruce Wayne" }
        };

        public IEventHandler EventHandler { get; private set; }

        public AppSettings AppSettings { get; private set; }

        public ExampleController(IEventHandler eventHandler, IOptions<AppSettings> appSettings)
        {
            EventHandler = eventHandler;

            // This is an example of how you inject configuration options that are read from config files and registered in Startup.cs.
            AppSettings = appSettings.Value;
        }
        
        // GET /api/example
        [HttpGet]
        public IActionResult GetAll()
        {
            //Publish an Event passing an entire object
            EventHandler.Publish<List<Example>>("CUD", "examples:requested", _examples , "666", nameof(ExampleController));

            // this will return a HTTP Status Code 200 (OK) along with the data
            return Ok(_examples);
        }

        // GET /api/example/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var example = _examples.FirstOrDefault(e => e.Id == id);
            if ( example == null )
            {
                //Publish an Event passing a string message
                EventHandler.PublishString("CUD","examples:notfound", "Example {id} not found", "666", nameof(ExampleController));
                               
                // this will return a HTTP Status Code 404 (Not Found) along with the message
                return NotFound($"No example found with id = {id}");
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(example);
                //Publish an Event passing a json string
                EventHandler.PublishJson("CUD", "examples:found", json, "666", nameof(ExampleController));
            }

            // this will return a HTTP Status Code 200 (OK) along with the data
            return Ok(example);
        }
    }
}