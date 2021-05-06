﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Employee.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee.Core
{
    public class ApplicationDAL: IApplicationDAL
    {
        private ApplicationContext _appContext;
        private IApplicationLogger _logger;
        public ApplicationDAL(ApplicationContext context, IApplicationLogger logger)
        {
            _appContext = context;
            _logger = logger;
        }

        public List<Employee> GetData()
        {
            try
            {
                return _appContext.Employees.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GetDate {ex}");
                return null;
            }
        }

        public bool SaveEmployee(Employee employee)
        {
            try
            {
                var employeeFromDb = _appContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

                if (employeeFromDb == null)
                {
                    _appContext.Employees.Add(employee);
                    _appContext.SaveChanges();
                    
                }
                else
                {
                    employeeFromDb.EmployeeFullName = employee.EmployeeFullName;
                    employeeFromDb.Address = employee.Address;
                    employeeFromDb.Position = employee.Position;
                    employeeFromDb.PhoneNumber = employee.PhoneNumber;
                    employeeFromDb.CreatedDate = DateTime.Now;
                    employeeFromDb.UpdatedDate = DateTime.Now;
                    _appContext.Employees.Update(employeeFromDb);
                    _appContext.SaveChanges();
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in SaveEMployee {ex}");
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employeeFromDb = _appContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

                if (employeeFromDb == null)
                {
                    return false; //log no employee
                }

                _appContext.Employees.Remove(employeeFromDb);
                 _appContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in DeleteEMployee {ex}");
                return false;
            }
        }
    }
}