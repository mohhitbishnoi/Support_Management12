using Application.Features.Companies.CompanyCommands;
using Application.Features.Companies.CompanyQueries;
using Application.Features.Companies.CompanyQuries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Management_WebApi.Responces;

namespace Support_Management_WebApi.Controllers.CompanyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create-Company")]
        public async Task<IActionResult> Create(CreateCompanyCommand command)
        {
            var response = await _mediator.Send(command);
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("All User")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCompaniesQuery());
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CompanyGetById(int id)
        {
            var response = await _mediator.Send(new GetCompanyByIdQuries(id));
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpPut("Update-Company {id}")]

        public async Task<IActionResult> UpdateCompany(CreateCompanyCommand command, int id)
        {
            var response = await _mediator.Send(new UpdateCompanyCommand(id,command));
            return ResponseHelper.GenerateResponse(response);
        }

        [HttpDelete("Delete-Company{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var response = await _mediator.Send(new DeleteCompanyCommand(id));
            return ResponseHelper.GenerateResponse(response);
        }

    }
}
