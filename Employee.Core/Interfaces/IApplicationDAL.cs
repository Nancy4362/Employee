using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Core.Interfaces
{
    public interface IApplicationDAL
    {
        List<Employee> GetData();
        bool SaveEmployee(Employee Employee);
        bool DeleteEmployee(int EmployeeId);

    }
}
