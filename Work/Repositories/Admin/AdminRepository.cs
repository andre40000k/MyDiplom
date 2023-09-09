using LoginComponent.DataBase;
using LoginComponent.Interface.IRepositories.Admin;
using LoginComponent.LoginEnums;
using LoginComponent.Models;
using LoginComponent.Models.Department;
using LoginComponent.Models.Transports;
using Microsoft.EntityFrameworkCore;

namespace LoginComponent.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AplicationContext _aplicationContext;

        public AdminRepository(AplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }

        //public async Task<Transport> GetTransportAsync()

        public async Task<int> SaveTransportAsync(Transport transport)
        {
            //await _aplicationContext.Transports.AddAsync(transport);

            return await _aplicationContext.SaveChangesAsync();
        }

        //public async Task<int> SaveDepartmentAsync(Department department)
        //{
        //    await _aplicationContext.Departments.AddAsync(department);

        //    return await _aplicationContext.SaveChangesAsync();
        //}

        public async Task<Transport?> GetTransportsAsync(string Id)
        {
            //return await _aplicationContext.Transports.FirstOrDefaultAsync(t => t.Id == Id);

            return null;
        }

        //public async Task<Department?> GetDepartmentAsync(Guid Id)
        //{
        //    return await _aplicationContext.Departments.FirstOrDefaultAsync(t => t.Id == Id);
        //}

        //public async Task<int> SaveDepartmentAsync(RegionalDepartment regionalDepartment)
        //{
        //    await _aplicationContext.RegionalDepartments.AddAsync(regionalDepartment);

        //    return await _aplicationContext.SaveChangesAsync();
        //}

        public async Task<RegionalDepartment?> SearchDepartmentsAddressAsync(string adress, DepartmentImportanceEnum typeDepartment)
        {
            switch(typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments
                        .FirstOrDefaultAsync(a => a.Address == adress);
               
                case DepartmentImportanceEnum.District:
                    return await _aplicationContext.RegionalDepartments
                        .Where(d => d.DistrictDepartments
                               .Any(a => a.Address == adress))
                        .FirstOrDefaultAsync();

                case DepartmentImportanceEnum.Local:
                    return await _aplicationContext.RegionalDepartments
                        .Where(d => d.DistrictDepartments
                               .Any(l => l.LocalDepatments
                                    .Any(a => a.Address == adress)))
                        .FirstOrDefaultAsync();

                default:
                    return null;
            }
        }

        public async Task<RegionalDepartment?> SearchDepartmentsIdAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment)
        {
            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments
                        .Include(d => d.DistrictDepartments)
                        .FirstOrDefaultAsync(id => id.Id == nodalId);

                case DepartmentImportanceEnum.District:

                    return await _aplicationContext.RegionalDepartments
                        .Include(d => d.DistrictDepartments)
                        .ThenInclude(l => l.LocalDepatments)
                        .FirstOrDefaultAsync(d => d.DistrictDepartments
                        .Any(id =>id.Id == nodalId));

                default:
                    return null;
            }
        }
        public async Task<int> SearchDepartmentsNumberAsync()
        {
            return await _aplicationContext.RegionalDepartments
                        .MaxAsync(n => n.Number);
        }

        public async Task<int?> SearchDepartmentsNumberAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment)
        {
            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.District:

                    return await _aplicationContext.RegionalDepartments
                        .Where(id => id.Id == nodalId)
                        .SelectMany(d => d.DistrictDepartments)
                        .Select(n => (int?)n.Number)
                        .DefaultIfEmpty()
                        .MaxAsync();

                case DepartmentImportanceEnum.Local:

                    return await _aplicationContext.RegionalDepartments
                        .SelectMany(d => d.DistrictDepartments)
                        .Where(id => id.Id == nodalId)
                        .SelectMany(l => l.LocalDepatments)
                        .Select(n => (int?)n.Number)
                        .DefaultIfEmpty()
                        .MaxAsync();

                default:
                    return default;
            }
        }



        public async Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment) 
        {
            _aplicationContext.RegionalDepartments.Add(regionalDepartment);
            return await _aplicationContext.SaveChangesAsync();
        }

        public async Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment,
            DistrictDepartment districtDepartment)
        {
            regionalDepartment.DistrictDepartments.Add(districtDepartment);
            return await _aplicationContext.SaveChangesAsync();
        }

        public async Task<int> AddDepartmentsAsync(RegionalDepartment regionalDepartment, 
            LocalDepartment localDepartment)
        {
            regionalDepartment.DistrictDepartments.First()
                .LocalDepatments.Add(localDepartment);

            return await _aplicationContext.SaveChangesAsync();
        }
    }
}
