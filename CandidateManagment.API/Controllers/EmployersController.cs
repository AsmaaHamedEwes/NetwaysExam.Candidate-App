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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public EmployersController(IEmployerService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.service = service;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET: api/<EmployersController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetEmployers();
            return Ok(new SuccessResponse<List<ResEmployerVM>>
            {
                Code = 200,
                Data = result
            });

        }


        // GET api/<EmployersController>/5
        [HttpGet("{EmployerId}")]
        public IActionResult Get(string EmployerId)
        {
            var result = service.GetEmployer(EmployerId);
            return Ok(new SuccessResponse<ResEmployerVM>
            {
                Code = 200,
                Data = result
            });

        }

        // POST api/<EmployersController>
        [HttpPost]
        public IActionResult Post([FromBody] ReqEmployerVM req)
        {
            var result = service.CreateEmployer(req);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // PUT api/<EmployersController>/5
        [HttpPut("{EmployerId}")]
        public IActionResult Put(string EmployerId, [FromBody] ReqEmployerVM req)
        {
            var result = service.UpdateEmployerProfile(req , EmployerId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // DELETE api/<EmployersController>/5
        [HttpDelete("{EmployerId}")]
        public IActionResult Delete(string EmployerId)
        {
            var result = service.DeleteEmployer( EmployerId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }
    }
}
