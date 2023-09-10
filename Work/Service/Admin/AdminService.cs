using LoginComponent.Interface.IRepositories.Admin;
using LoginComponent.Interface.IServices.Admin;
using LoginComponent.LoginEnums;
using LoginComponent.Models.Department;
using LoginComponent.Models.Request.Admin;
using LoginComponent.Models.Request.Admin.Department;
using LoginComponent.Models.Responses.Admin;
using LoginComponent.Models.Transports;
using System.Net;

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
                //Id = Guid.NewGuid().ToString(),
                Capacity = transportRequest.Capacity,
                Name = transportRequest.TypeOfTransport
            };

            var existingTransport = await _adminRepository.SaveTransportAsync(newTransport);

            if (existingTransport >= 0)
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

        public async Task<DepartmentResponse> AddTDepartmentAsync(DepartmentAddReqest departmentReqest)
        {
            var checkType = Enum.TryParse(departmentReqest.TypeDepartment, out DepartmentImportanceEnum typeDepartment);

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
                .SearchDepartmentsAddressAsync(departmentReqest.Address, typeDepartment);

            if (existingDepartment != null)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "There is a department with this address!",
                    ErrorCode = "500"
                };
            }

            bool checkId = Guid.TryParse(departmentReqest.PerentDepartmentId, out Guid reqestId);

            if (!checkId && typeDepartment != 0)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong Id",
                    ErrorCode = "500"
                };
            }

            var typePerentDepartment = typeDepartment - 1;

            if(!checkId)
            {
                existingDepartment = await _adminRepository.SearchParentDepartmentsIdAsync(reqestId, typePerentDepartment);

                if (existingDepartment == null)
                {
                    return new DepartmentResponse
                    {
                        Success = false,
                        Error = "There isn't a department with this Id!",
                        ErrorCode = "500"
                    };
                }
            }
           

            int? numberNewDepartment = await _adminRepository.SearchDepartmentsNumberAsync(reqestId, typeDepartment);

            if (numberNewDepartment == null)
            {
                numberNewDepartment = 0;
            }

            int saveResponse = default;

            switch (typeDepartment)
            {
                case DepartmentImportanceEnum.Regional:
                    var newRegDepartment = new RegionalDepartment
                    {
                        Number = (int)numberNewDepartment + 1,
                        Address = departmentReqest.Address
                    };

                    saveResponse = await _adminRepository.AddDepartmentsAsync(newRegDepartment, Guid.Empty);
                    break;

                case DepartmentImportanceEnum.District:
                    var newDictDepartment = new DistrictDepartment
                    {
                        RegionalID = reqestId,
                        Number = (int)numberNewDepartment + 1,
                        Address = departmentReqest.Address
                    };

                    saveResponse = await _adminRepository.AddDepartmentsAsync(newDictDepartment, reqestId);
                    break;

                case DepartmentImportanceEnum.Local:
                    var newLocDepartment = new LocalDepartment
                    {
                        DistrictId = reqestId,
                        Number = (int)numberNewDepartment + 1,
                        Address = departmentReqest.Address
                    };
                    saveResponse = await _adminRepository.AddDepartmentsAsync(newLocDepartment, reqestId);
                    break;

                default:
                    return new DepartmentResponse
                    {
                        Success = false,
                        Error = "Unable to save the departmnt",
                        ErrorCode = "500"
                    };
            }

            if (saveResponse > 0)
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

        public async Task<DepartmentResponse> RemoveDepartmentAsync(DepartmentRemoveRequest departmentRemoveRequest)
        {
            bool checkId = Guid.TryParse(departmentRemoveRequest.DepartmentId, out Guid reqestId);

            if (!checkId)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong Id",
                    ErrorCode = "500"
                };
            }

            var checkType = Enum.TryParse(departmentRemoveRequest.TypeDepartment, out DepartmentImportanceEnum typeDepartment);

            if (!checkType)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong type department",
                    ErrorCode = "500"
                };
            }

            var existingDepartment = await _adminRepository.SearchDepartmentAsync(reqestId, typeDepartment);

            if (existingDepartment == null)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "There isn't a department with this Id!",
                    ErrorCode = "500"
                };
            }

            var removeResponse = await _adminRepository.RemoveDepartmentAsync(existingDepartment);

            if (removeResponse >= 0)
            {
                return new DepartmentResponse { Success = true };
            }

            return new DepartmentResponse
            {
                Success = false,
                Error = "Unable to delete the transport",
                ErrorCode = "500"
            };


        }

        public async Task<DepartmentResponse> UpdateDepartmentAsync(DepartmentUpdataRequest departmentUpdataRequest)
        {
            bool checkId = Guid.TryParse(departmentUpdataRequest.DepartmentId, out Guid reqestId);

            if (!checkId)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong Id",
                    ErrorCode = "500"
                };
            }

            var checkType = Enum.TryParse(departmentUpdataRequest.TypeDepartment, out DepartmentImportanceEnum typeDepartment);

            if (!checkType)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "Wrong type department",
                    ErrorCode = "500"
                };
            }

            var existingDepartment = await _adminRepository.SearchDepartmentAsync(reqestId, typeDepartment);

            if (existingDepartment == null)
            {
                return new DepartmentResponse
                {
                    Success = false,
                    Error = "There isn't a department with this Id!",
                    ErrorCode = "500"
                };
            }

            int saveResponse = default;


            switch (existingDepartment)
            {
                case RegionalDepartment regionalDepartment:
                    var newRegDepartment = new RegionalDepartment
                    {
                        Number = regionalDepartment.Number,
                        Address = departmentUpdataRequest.NewAddress
                    };

                    saveResponse = await _adminRepository.UpdateDepartmentAsync(newRegDepartment);
                    break;

                case DistrictDepartment districtDepartment:
                    var newDictDepartment = new DistrictDepartment
                    {
                        RegionalID = districtDepartment.RegionalID,
                        Number = districtDepartment.Number,
                        Address = departmentUpdataRequest.NewAddress
                    };

                    saveResponse = await _adminRepository.UpdateDepartmentAsync(newDictDepartment);
                    break;

                case LocalDepartment localDepartment:
                    var newLocDepartment = new LocalDepartment
                    {
                        DistrictId = localDepartment.DistrictId,
                        Number = localDepartment.Number,
                        Address = departmentUpdataRequest.NewAddress
                    };
                    saveResponse = await _adminRepository.UpdateDepartmentAsync(newLocDepartment);
                    break;

                default:
                    return new DepartmentResponse
                    {
                        Success = false,
                        Error = "Unable to updata the departmnt",
                        ErrorCode = "500"
                    };
            }

            if (saveResponse > 0)
            {
                return new DepartmentResponse { Success = true };
            }

            return new DepartmentResponse
            {
                Success = false,
                Error = "Unable to updata the departmnt",
                ErrorCode = "500"
            };

        }
    }
}
