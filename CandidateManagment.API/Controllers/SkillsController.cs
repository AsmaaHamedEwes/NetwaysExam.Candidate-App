using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.IService;
using Service.ViewModel.VM;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;

namespace CandidateManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SkillsController(ISkillService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET: api/<SkillsController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetSkills();
            return Ok(new SuccessResponse<List<ResSkillVM>>
            {
                Code = 200,
                Data = result
            });
        }

        // GET api/<SkillsController>/5
        [HttpGet("{SkillId}")]
        public IActionResult Get(string SkillId)
        {
            var result = service.GetSkill(SkillId);
            return Ok(new SuccessResponse<ResSkillVM>
            {
                Code = 200,
                Data = result
            });
        }

        // POST api/<SkillsController>
        [HttpPost]
        public IActionResult Post([FromBody] ReqSkillVM req)
        {
            var result = service.CreateSkill(req);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // PUT api/<SkillsController>/5
        [HttpPut("{SkillId}")]
        public IActionResult Put(string SkillId, [FromBody] ReqSkillVM req)
        {
            var result = service.UpdateSkillRecord(req , SkillId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // DELETE api/<SkillsController>/5
        [HttpDelete("{SkillId}")]
        public IActionResult Delete(string SkillId)
        {
            var result = service.DeleteSkill(SkillId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }
    }
}
