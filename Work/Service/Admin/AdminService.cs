using LoginComponent.Interface.IRepositories.Admin;
using LoginComponent.Interface.IServices.Admin;
using LoginComponent.LoginEnums;
using LoginComponent.Models.Department;
using LoginComponent.Models.Request.Admin;
using LoginComponent.Models.Responses.Admin;
using LoginComponent.Models.Transports;

namespace LoginComponent.Service.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<TransportRespons> AddTransportAsync(TransportRequest transportRequest)
        {
            var newTransport = new Transport
            {
                Id = Guid.NewGuid().ToString(),
                Capacity = transportRequest.Capacity,
                TypeOfTransport = transportRequest.TypeOfTransport
            };

            var existingTransport = await _adminRepository.SaveTransportAsync(newTransport);

            if(existingTransport >= 0)
            {
                return new TransportRespons { Success = true };
            }

            return new TransportRespons
            {
                Success = false,
                Error = "Unable to save the transport",
                ErrorCode = "500"
            };
        }

        public async Task<DepartmentResponse> AddTDepartmentAsync(DepartmentReqest departmentReqest)
        {
            var checkType = Enum.TryParse(departmentReqest.TypeDepartment, out DepartmentImportanceEnum typeChildrenDepartment);

            if (!checkType)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong type department",
                    ErrorCode = "500"
                };
            }

            var existingDepartment = await _adminRepository
                .SearchDepartmentsAddressAsync(departmentReqest.Address, typeChildrenDepartment);

            if (existingDepartment != null)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "There is a department with this address!",
                    ErrorCode = "500"
                };
            }

            int? numberNewDepartment = default;
            int saveResponse = default;

            if (typeChildrenDepartment == 0)
            {
                numberNewDepartment = await _adminRepository.SearchDepartmentsNumberAsync();

                saveResponse = await _adminRepository.AddDepartmentsAsync(
                    new RegionalDepartment
                    {
                        Number = (int)numberNewDepartment + 1,
                        Address = departmentReqest.Address
                    });


            }

            if (saveResponse > 0)
            {
                return new DepartmentResponse { Success = true };
            }

            bool checkId = Guid.TryParse(departmentReqest.PerentDepartmentId, out Guid reqestId);

            if (!checkId)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong Id",
                    ErrorCode = "500"
                };
            }

            var typePerentDepartment = typeChildrenDepartment - 1;

            existingDepartment = await _adminRepository.SearchDepartmentsIdAsync(reqestId, typePerentDepartment);

            if (existingDepartment == null)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "There is a department with this Id!",
                    ErrorCode = "500"
                };
            }

            numberNewDepartment = await _adminRepository.SearchDepartmentsNumberAsync(reqestId, typeChildrenDepartment);

            if(numberNewDepartment == null)
            {
                numberNewDepartment = 0;
            }

            switch (typeChildrenDepartment)
            {
                case DepartmentImportanceEnum.District:
                    var newDictDepartment = new DistrictDepartment
                    {
                        RegionalID = reqestId,
                        Number = (int)numberNewDepartment + 1,
                        Address = departmentReqest.Address
                    };

                    saveResponse = await _adminRepository.AddDepartmentsAsync(existingDepartment, newDictDepartment);
                    break;

                case DepartmentImportanceEnum.Local:
                    saveResponse = await _adminRepository.AddDepartmentsAsync(existingDepartment,
                       new LocalDepartment
                       {
                           DistrictId = reqestId,
                           Number = (int)numberNewDepartment + 1,
                           Address = departmentReqest.Address
                       });
                    break;
                default:
                    return new DepartmentResponse
                    {
                        Success = false,
                        Error = "Unable to save the departmnt",
                        ErrorCode = "500"
                    };
            }

            if(saveResponse > 0)
            {
                return new DepartmentResponse { Success = true };
            }

            return new DepartmentResponse
            {
                Success = false,
                Error = "Unable to save the departmnt",
                ErrorCode = "500"
            };
        }
    }
}
