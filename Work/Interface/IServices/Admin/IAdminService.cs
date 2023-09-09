using LoginComponent.Models.Request.Admin;
using LoginComponent.Models.Responses.Admin;

namespace LoginComponent.Interface.IServices.Admin
{
    public interface IAdminService
    {
        Task<TransportRespons> AddTransportAsync(TransportRequest transportRequest);
        Task<DepartmentResponse> AddTDepartmentAsync(DepartmentReqest departmentReqest);
    }
}
