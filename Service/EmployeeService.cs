﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger,IMapper mapper)
        {
            _repository= repository;
            _logger = logger;
            _mapper = mapper;
        }

        IEnumerable<EmployeeDto> IEmployeeService.GetAllEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employees = _repository.Employee.GetAllEmployees(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        EmployeeDto IEmployeeService.GetEmployee(Guid companyid,Guid id,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyid,trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyid);

            var employee = _repository.Employee.GetEmployee(companyid, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        EmployeeDto IEmployeeService.CreateEmployeeForCompany(Guid companyid,EmployeeForCreationDto employeeForCreationDto,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyid, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyid);

            var employeeEntity = _mapper.Map<Employee> (employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyid, employeeEntity);

            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        void IEmployeeService.DeleteEmployeeForCompany(Guid companyId,Guid id,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges);

            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(employee);

            _repository.Save();
        }

        void IEmployeeService.UpdateEmployeeForCompany
            (Guid companyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto,bool compTrackChanges,bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdateDto, employee);

            _repository.Save();
        }

        public (EmployeeForUpdateDto employeeTopatch,Employee employeeEntity) GetEmployeeForPatch(Guid companyId,Guid id,bool compChanges,bool empChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, empChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch,Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);

            _repository.Save();
        }
    }
}
