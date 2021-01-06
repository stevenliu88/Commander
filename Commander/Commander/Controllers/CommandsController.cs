using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut]
        [Route("UpdateCommand/{Id}")]
        public ActionResult UpdateCommand(int Id, CommandUpdateDto cmd) 
        {
            var command = _repo.GetCommandById(Id);
            if (command != null) 
            {
                 _mapper.Map(cmd, command);
                _repo.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch]
        [Route("PartialUpdateCommand/{Id}")]
        public ActionResult PartialUpdateCommand(int Id, JsonPatchDocument<CommandUpdateDto> patchDoc) 
        {
            var command = _repo.GetCommandById(Id);
            if (command == null) {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(command);

            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(ModelState)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);

            _repo.SaveChanges();

            return NoContent();

        }

        [HttpDelete]
        [Route("DeleteCommand/{Id}")]
        public ActionResult DeleteCommand(int Id) 
        {
            var command = _repo.GetCommandById(Id);
            if (command == null) {
                return BadRequest();
            }

            _repo.DeleteCommand(command);
            _repo.SaveChanges();
            return NoContent();


        }

    }
}
