using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Core;
using Employee.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext _appContext;
        private IApplicationDAL _appDal;

        public EmployeeController(ApplicationContext appContext, IApplicationDAL appDal)
        {
            _appContext = appContext;
            _appDal = appDal;
        }
        // GET: EmployeeController
        [HttpGet("employee")]
        public ActionResult Index()
        {
            return View("~/Views/Employee/Index.cshtml");
        }
        [HttpGet("employee/table-data-view")]
        public  IActionResult GetAllEmployees()
        {
            try
            {
                var data = _appDal.GetData();
                ViewData["EmployeesList"] = data;
                return PartialView("~/Views/Employee/_tableData.cshtml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        [HttpGet("employee/add")]
        public IActionResult AddEmployeeForm()
        {
            try
            {
                ViewData["Title"] = "Personal Information";
                ViewData["Employee"] = new Core.Employee();
                return PartialView("~/Views/Employee/_AddEmp.cshtml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        [HttpPost("employee/save")]
        public  IActionResult SaveEmployee(Core.Employee employee)
        {
            //validate EMployee
            if(_appDal.SaveEmployee(employee))
                return Json(new { success = true, message = "Created Successfully" });
            return Json(new { success = false, message = "Error while saving" });

            
        }
        [HttpPost("employee/delete")]
        public  IActionResult DeleteEmployee(int id)
        {
            if (_appDal.DeleteEmployee(id))
                return Json(new { success = true, message = "Delete Successfull!" });
            
            
            return Json(new { success = false, message = "Error while deleting." });
            
        }

       

        
    }
}
