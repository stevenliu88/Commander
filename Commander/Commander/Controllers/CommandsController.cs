using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly ICommanderRepo _repo;

        public CommandsController(ICommanderRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("GetAllCommands")]
        public ActionResult<IEnumerable<Command>> GetAllCommands() 
        {
            var result = _repo.GetAllCommanders();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCommandById/{Id}")]
        public ActionResult<Command> GetCommandById(int Id) 
        {
            var result = _repo.GetCommandById(Id);
            return Ok(result);
        }
    }
}
