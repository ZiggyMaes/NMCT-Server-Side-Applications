using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Presentation.Models;
using Presentation.Repositories;

namespace Presentation.Controllers
{
    public class ForumAPIController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetMessage(int MessageId)
        {
            Message FetchedMessaged = ForumRepository.GetMessage(MessageId);

            if (FetchedMessaged == null)  return NotFound();
            else return Ok(FetchedMessaged);
        }

        [HttpGet]
        public IEnumerable<Message> GetThreadContent(int AreaId)
        {
            return ForumRepository.GetThreads(AreaId);
        }
    }
}