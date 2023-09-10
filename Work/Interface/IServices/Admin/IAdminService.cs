using LoginComponent.Models.Request.Admin;
using LoginComponent.Models.Request.Admin.Department;
using LoginComponent.Models.Responses.Admin;

namespace LoginComponent.Interface.IServices.Admin
{
    public interface IAdminService
    {
        Task<TransportRespons> AddTransportAsync(TransportRequest transportRequest);
        Task<DepartmentResponse> AddTDepartmentAsync(DepartmentAddReqest departmentReqest);
        Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdataRequest departmentUpdataRequest);
        Task<DepartmentResponse> RemoveDepartmentAsync(DepartmentRemoveRequest departmentRemoveRequest);
    }
}
