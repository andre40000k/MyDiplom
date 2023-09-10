using LoginComponent.Interface.IServices.Admin;
using LoginComponent.Models.Request.Admin;
using LoginComponent.Models.Request.Admin.Department;
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
        public async Task<IActionResult> AddDepartment(DepartmentAddReqest departmentReqest)
        {
            var addDepartment = await _adminService.AddTDepartmentAsync(departmentReqest);

            if(!addDepartment.Success)
            {
                return UnprocessableEntity(addDepartment);
            }

            return Ok("Department added successfully!");
        }

        [HttpDelete]
        [Route("deleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(DepartmentRemoveRequest departmentRemoveRequest)
        {
            var addDepartment = await _adminService.RemoveDepartmentAsync(departmentRemoveRequest);

            if (!addDepartment.Success)
            {
                return UnprocessableEntity(addDepartment);
            }

            return Ok("Department deleted successfully!");
        }

        [HttpPut]
        [Route("updataDepartment")]
        public async Task<IActionResult> UpdataDepartment(DepartmentUpdataRequest departmentUpdataRequest)
        {
            var updataDepartment = await _adminService.UpdateDepartmentAsync(departmentUpdataRequest);
            
            if (!updataDepartment.Success)
            {
                return UnprocessableEntity(updataDepartment);
            }

            return Ok("Department updata successfully!");
        }
    }
}
