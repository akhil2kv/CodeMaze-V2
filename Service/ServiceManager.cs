using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{                                               
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;

        private readonly Lazy<ICompanyService> _companyService;

        private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repository, ILoggerManager logger,IMapper mapper,IDataShaper<EmployeeDto> dataShaper)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repository, logger, mapper,dataShaper));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repository, logger, mapper));
        }

        public  IEmployeeService EmployeeService => _employeeService.Value;

        public  ICompanyService CompanyService => _companyService.Value;
    }
}
