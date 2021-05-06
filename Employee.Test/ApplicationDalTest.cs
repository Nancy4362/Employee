using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Core;
using Employee.Core.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Employee.Test
{
    public class ApplicationDalTest : BaseIntegrationTest
    {
        private ApplicationDAL subject;
        private ApplicationContext testApplicationContext;
        private Mock<IApplicationLogger> mockAppLogger = new Mock<IApplicationLogger>();
        public ApplicationDalTest() : base()
        {
            testApplicationContext = _appContext;
            subject = new ApplicationDAL(testApplicationContext, mockAppLogger.Object);
        }

        [Fact]
        public void GetCompany_ReturnNull_IfNotFound()
        {
            var result = subject.GetData();
            result.Should().BeNull();
        }
        [Fact]
        public void GetCompany_ReturnObject_IfFound()
        {
            var clientCode = "test Employee";
            var emp = new Core.Employee
            {
                EmployeeFullName = clientCode,
                EmployeeId = 123,
                Address = "test address"
            };
            testApplicationContext.Add(emp);
            var result = subject.GetData().FirstOrDefault(x=> x.EmployeeId == emp.EmployeeId);
            result.EmployeeId.Should().Equals(emp.EmployeeId);
        }
    }
}
