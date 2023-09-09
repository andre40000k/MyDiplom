using LoginComponent.LoginEnums;
using LoginComponent.Models.Department;
using LoginComponent.Models.Transports;

namespace LoginComponent.Interface.IRepositories.Admin
{
    public interface IAdminRepository
    {
        Task<int> SaveTransportAsync(Transport transport);
        //Task<int> SaveDepartmentAsync(Department department);
        Task<Transport?> GetTransportsAsync(string Id);
        //Task<Department?> GetDepartmentAsync(Guid Id);
        //Task<int> SaveDepartmentAsync(RegionalDepartment regionalDepartment);
        Task<RegionalDepartment?> SearchDepartmentsAddressAsync(string adress, 
            DepartmentImportanceEnum typeDepartment);

        Task<int> SearchDepartmentsNumberAsync();
        Task<int?> SearchDepartmentsNumberAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment);


        Task<RegionalDepartment?> SearchDepartmentsIdAsync(Guid nodalId, 
            DepartmentImportanceEnum typeDepartment);
        Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment);
        Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment,
            DistrictDepartment districtDepartment);
        Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment,
            LocalDepartment localDepartment);
    }
}
