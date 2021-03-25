using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.IService;
using Service.ViewModel.VM;
using Service.ViewModel.VM.Request;
using Service.ViewModel.VM.Response;

namespace CandidateManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICandidateService service;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CandidatesController(ICandidateService service, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.service = service;
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET: api/<Candidates>
        [HttpGet]
        public IActionResult Get()
        {
           // var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;
            var result = service.GetCandidates();
            return Ok(new SuccessResponse<List<ResCandidateVM>>
            {
                Code = 200,
                Data = result
            });

        }

        // GET api/<Candidates>/CandidateId
        [HttpGet("{CandidateId}")]
        public IActionResult Get(string CandidateId)
        {
            var result = service.GetCandidate(CandidateId);
            return Ok(new SuccessResponse<ResCandidateVM>
            {
                Code = 200,
                Data = result
            });
        }

        // POST api/<Candidates>
        [HttpPost]
        public IActionResult Post([FromBody] ReqCandidateVM req)
        {
            var result = service.CreateCandidate(req);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // PUT api/<Candidates>/5
        [HttpPut("{CandidateId}")]
        public IActionResult Put(string CandidateId, [FromBody] ReqCandidateVM req)
        {
            var result = service.UpdateCandidateProfile(req , CandidateId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }

        // DELETE api/<Candidates>/5
        [HttpDelete("{CandidateId}")]
        public IActionResult Delete(string CandidateId)
        {
            var result = service.DeleteCandidate( CandidateId);
            return Ok(new SuccessResponse<string>
            {
                Code = 200,
                Data = result
            });
        }
    }
}
