using System;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{

    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {   
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository,IMapper mapper)        
         {
             _repository = repository;
             _mapper = mapper;
         }

        //GET api/commands as the rounte in the beginning
        [HttpGet]
         public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
         {
             var commandsItems = _repository.GetAllCommands();
             
             return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandsItems));

         } 

         [HttpGet("{id}",Name="GetCommandById")]
         public ActionResult <CommandReadDto> GetCommandById(int id)
         {
             var commandItem = _repository.GetCommandById(id);
             if(commandItem != null)
             {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
             }
            return NotFound();
         }

         [HttpPost]
         public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto createDto)
         {
             //var commandItem = _repository.CreateCommand();
             //from createDto to a real Command model
             var commandItem =_mapper.Map<Command>(createDto);
             //add model to db by calling the real creat method from interface or repo?
             _repository.CreateCommand(commandItem);
             _repository.SaveChanges();

             //show readDto to users
             var readDto=_mapper.Map<CommandReadDto>(commandItem);
             //create the URI 
             return CreatedAtRoute(nameof(GetCommandById),new {Id = readDto.Id},readDto);

         }
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,CommandCreateDto creatDto)
        {
            //find the target comand model by id
            // var commandModel = _repository.GetCommandById(id);
            // Console.WriteLine(commandModel.ToString());
            // if (commandModel ==null)
            // {
            //     return NotFound();
            // }
            //_mapper.Map(creatDto,commandModel);
            var commandModel = _mapper.Map<Command>(creatDto);
             _repository.CreateCommand(commandModel);

            //_repository.UpdateCommand(commandModel);
            _repository.SaveChanges();

            return NoContent();

        } 

    }



}