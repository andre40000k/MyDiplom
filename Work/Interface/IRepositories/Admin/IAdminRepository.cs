using LoginComponent.LoginEnums;
using LoginComponent.Models;
using LoginComponent.Models.Department;
using LoginComponent.Models.Transports;

namespace LoginComponent.Interface.IRepositories.Admin
{
    public interface IAdminRepository
    {
        Task<int> SaveTransportAsync(Transport transport);
        Task<Transport?> GetTransportsAsync(string Id);

        Task<IBaseDepartment?> SearchDepartmentsAddressAsync(string adress, DepartmentImportanceEnum typeDepartment);
        Task<int?> SearchDepartmentsNumberAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment);
        Task<IBaseDepartment?> SearchParentDepartmentsIdAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment);
        Task<IBaseDepartment?> SearchDepartmentAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment);

        Task<int> AddDepartmentsAsync<T>(T department, Guid idPerentDepartment);
        Task<int> UpdateDepartmentAsync<T>(T department);
        Task<int?> RemoveDepartmentAsync<T>(T department);
    }
}
