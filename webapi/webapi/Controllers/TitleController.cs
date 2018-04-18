using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [RoutePrefix("titles")]
    public class TitleController : ApiController
    {
        private readonly TitleService titleService = new TitleService(); 

        [HttpPost]
        [Route("Add")]
        public string AddTitle([FromBody] Title title)
        {
            titleService.AddTitle(title);

            return "title added...";
        }

        [Route("Get/All")]
        [HttpGet]
        public List<Title> GetAllTitles()
        {
            return titleService.GetAllTitles();
        }
        
        [Route("Remove")]
        [HttpPost]
        public string RemoveTitle([FromBody]Title title)
        {
            titleService.RemoveTitle(title);
            return "title removed..";
        }
    }
}
