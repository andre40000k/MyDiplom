using LoginComponent.Interface.IServices.Admin;
using LoginComponent.Models.Request.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LoginComponent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("addTramsport")]
        public async Task<IActionResult> AddTransport(TransportRequest transportRequest)
        {
            var addTransportResponse = await _adminService.AddTransportAsync(transportRequest);

            if(!addTransportResponse.Success)
            {
                return UnprocessableEntity(addTransportResponse);
            }

            return Ok("Transport added successfully!");
        }

        [HttpPost]
        [Route("addDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentReqest departmentReqest)
        {
            var addDepartment = await _adminService.AddTDepartmentAsync(departmentReqest);

            if(!addDepartment.Success)
            {
                return UnprocessableEntity(addDepartment);
            }

            return Ok("Department added successfully!");
        }
    }
}
