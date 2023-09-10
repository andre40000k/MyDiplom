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
            await _aplicationContext.Transports.AddAsync(transport);

            return await _aplicationContext.SaveChangesAsync();
        }
        public async Task<Transport?> GetTransportsAsync(string Id)
        {
            //return await _aplicationContext.Transports.FirstOrDefaultAsync(t => t.Id == Id);

            return null;
        }



        public async Task<IBaseDepartment?> SearchDepartmentsAddressAsync(string adress, DepartmentImportanceEnum typeDepartment)
        {
            switch(typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments
                        .FirstOrDefaultAsync(a => a.Address == adress);
               
                case DepartmentImportanceEnum.District:
                    return await _aplicationContext.DistrictDepartments
                        .FirstOrDefaultAsync(a => a.Address == adress);

                case DepartmentImportanceEnum.Local:
                    return await _aplicationContext.LocalDepartments
                        .FirstOrDefaultAsync(a => a.Address == adress);
                default:
                    return null;
            }
        }
        public async Task<IBaseDepartment?> SearchParentDepartmentsIdAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment)
        {
            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments
                        .Include(d => d.DistrictDepartments)
                        .FirstOrDefaultAsync(id => id.Id == nodalId);

                case DepartmentImportanceEnum.District:

                    return await _aplicationContext.DistrictDepartments
                        .Include(d => d.LocalDepatments)
                        .FirstOrDefaultAsync(id => id.Id == nodalId);

                default:
                    return null;
            }
        }
        public async Task<int?> SearchDepartmentsNumberAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment)
        {
            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments
                        .Select(n => (int?)n.Number)
                        .DefaultIfEmpty()
                        .MaxAsync();

                case DepartmentImportanceEnum.District:
                    return await _aplicationContext.DistrictDepartments
                        .Where(id => id.RegionalID == nodalId)
                        .Select(n => (int?)n.Number)
                        .DefaultIfEmpty()
                        .MaxAsync();

                case DepartmentImportanceEnum.Local:
                    return await _aplicationContext.LocalDepartments
                        .Where(id => id.DistrictId == nodalId)
                        .Select(n => (int?)n.Number)
                        .DefaultIfEmpty()
                        .MaxAsync();

                default:
                    return null;
            }

            //var existingDepartment = await SearchDepartment(nodalId, typeDepartment-1);

            //var type = existingDepartment.GetType();

            //switch (type)
            //{
            //    //case var t when t == typeof(RegionalDepartment):
            //    //    //_aplicationContext.RegionalDepartments.Remove((RegionalDepartment)(object)existingDepartment);
            //    //    return await _aplicationContext.SaveChangesAsync();

            //    case var t when t == typeof(DistrictDepartment):

            //        return await _aplicationContext.DistrictDepartments
            //            .Where(id => id.RegionalID == nodalId)
            //            .Select(n => (int?)n.Number)
            //            .DefaultIfEmpty()
            //            .MaxAsync();

            //    //_aplicationContext.DistrictDepartments.Remove((DistrictDepartment)(object)existingDepartment);
            //    //return await _aplicationContext.SaveChangesAsync();

            //    case var t when t == typeof(LocalDepartment):
            //        _aplicationContext.LocalDepartments.Remove((LocalDepartment)(object)existingDepartment);
            //        return await _aplicationContext.SaveChangesAsync();

            //    default:
            //        return null;
            //}
            //switch (typeDepartment)
            //{
            //    case DepartmentImportanceEnum.District:

            //        return await _aplicationContext.RegionalDepartments
            //            .Where(id => id.Id == nodalId)
            //            .SelectMany(d => d.DistrictDepartments)
            //            .Select(n => (int?)n.Number)
            //            .DefaultIfEmpty()
            //            .MaxAsync();

            //    case DepartmentImportanceEnum.Local:

            //        return await _aplicationContext.RegionalDepartments
            //            .SelectMany(d => d.DistrictDepartments)
            //            .Where(id => id.Id == nodalId)
            //            .SelectMany(l => l.LocalDepatments)
            //            .Select(n => (int?)n.Number)
            //            .DefaultIfEmpty()
            //            .MaxAsync();

            //    default:
            //        return default;
            //}
        }
        public async Task<IBaseDepartment?> SearchDepartmentAsync(Guid nodalId, DepartmentImportanceEnum typeDepartment)
        {
            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    return await _aplicationContext.RegionalDepartments.FirstOrDefaultAsync(id => id.Id == nodalId);

                case DepartmentImportanceEnum.District:
                    return await _aplicationContext.DistrictDepartments.FirstOrDefaultAsync(id => id.Id == nodalId);

                case DepartmentImportanceEnum.Local:
                    return await _aplicationContext.LocalDepartments.FirstOrDefaultAsync(id => id.Id == nodalId);

                default:
                    return null;
            }
        }


        public async Task<int> AddDepartmentsAsync<T>(T department, Guid idPerentDepartment)
            where T : notnull
        {
            var type = department.GetType();
            switch (type)
            {
                case var t when t == typeof(RegionalDepartment):
                    _aplicationContext.RegionalDepartments.Add((RegionalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(DistrictDepartment):
                    _aplicationContext.RegionalDepartments.Include(d => d.DistrictDepartments)
                        .First(id => id.Id == idPerentDepartment)
                        .DistrictDepartments.Add((DistrictDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(LocalDepartment):
                    _aplicationContext.DistrictDepartments.Include(d => d.LocalDepatments)
                       .First(id => id.Id == idPerentDepartment)
                       .LocalDepatments.Add((LocalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                default:
                    return default;
            }

        }

        public async Task<int> UpdateDepartmentAsync<T>(T department) where T : notnull
        {
            var type = department.GetType();

            switch (type)
            {
                case var t when t == typeof(RegionalDepartment):
                    _aplicationContext.RegionalDepartments.Update((RegionalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(DistrictDepartment):
                    _aplicationContext.DistrictDepartments.Update((DistrictDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(LocalDepartment):
                    _aplicationContext.LocalDepartments.Update((LocalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                default:
                    return default;
            }
        }

        public async Task<int?> RemoveDepartmentAsync<T>(T department) 
            where T : notnull
        {
            var type = department.GetType();

            switch (type)
            {
                case var t when t == typeof(RegionalDepartment):
                    _aplicationContext.RegionalDepartments.Remove((RegionalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(DistrictDepartment):
                    _aplicationContext.DistrictDepartments.Remove((DistrictDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                case var t when t == typeof(LocalDepartment):
                    _aplicationContext.LocalDepartments.Remove((LocalDepartment)(object)department);
                    return await _aplicationContext.SaveChangesAsync();

                default:
                    return null;
            }

        }
    }

}
