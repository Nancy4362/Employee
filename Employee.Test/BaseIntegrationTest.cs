using System;
using System.Collections.Generic;
using System.Text;
using Employee.Core;
using Microsoft.EntityFrameworkCore;

namespace Employee.Test
{
    public class BaseIntegrationTest: IDisposable
    {
        public ApplicationContext _appContext;

        public BaseIntegrationTest()
        {
            _appContext = ConstructNewContext();

        }
        public ApplicationContext ConstructNewContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite("DataSource=:memory:", x => { })
                .Options;

            var context = new ApplicationContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }

        public void Dispose() => _appContext.Database.CloseConnection();
    }
}
