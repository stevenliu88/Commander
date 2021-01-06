using AutoMapper;
using Commander.Data;
using Commander.Dtos;
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
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllCommands")]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands() 
        {
            var commands = _repo.GetAllCommanders();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet]
        [Route("GetCommandById/{Id}")]
        public ActionResult<CommandReadDto> GetCommandById(int Id) 
        {
            var command = _repo.GetCommandById(Id);
            if (command != null) { 
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateCommand")]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd) 
        {
            var CommandModel = _mapper.Map<Command>(cmd);
             _repo.CreateCommand(CommandModel);
            var result = _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(CommandModel);
            if (result) 
            {
                return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id}, commandReadDto);
            }
            return BadRequest();
        }
    }
}
