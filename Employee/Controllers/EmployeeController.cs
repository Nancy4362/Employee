using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Employee.Core;
using Employee.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext _appContext;
        private IApplicationDAL _appDal;
        private IApplicationLogger _logger;
        public EmployeeController(ApplicationContext appContext, IApplicationDAL appDal, IApplicationLogger logger)
        {
            _appContext = appContext;
            _appDal = appDal;
            _logger = logger;
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
                _logger.Error($"error in getting data {ex}");
                return null;
            }
        }
        [HttpGet("employee/add")]
        public IActionResult AddEmployee()
        {
            try
            {
                ViewData["Title"] = "Personal Information";
                ViewData["Employee"] = new Core.Employee();
                List < SelectListItem > pos = new List<SelectListItem>
                {
                    new SelectListItem {Text = Position.genMgr, Value = Position.genMgr},
                    new SelectListItem {Text = Position.hrDir, Value = Position.hrDir},
                    new SelectListItem {Text = Position.prdMgr, Value = Position.prdMgr},
                    new SelectListItem {Text = Position.prjMgr, Value = Position.prjMgr},
                    new SelectListItem {Text = Position.senEdr, Value = Position.senEdr},
                    new SelectListItem {Text = Position.edr, Value = Position.edr},
                };
                ViewBag.Positions = pos;
                return PartialView("~/Views/Employee/_AddEmp.cshtml");
            }
            catch (Exception ex)
            {
                _logger.Error($"error in adding data {ex}");
                return null;
            }
        }

        [HttpPost("employee/save")]
        public IActionResult SaveEmployee(Core.Employee employee)
        {
            //validate EMployee data
            if (!string.IsNullOrEmpty(employee.PhoneNumber))
                Regex.Replace(employee.PhoneNumber.Trim(), @"\+\(|\)|\-", "");
            if (ValidateEmployee(employee)) {
                
                if (_appDal.SaveEmployee(employee))
                    return Json(new {success = true, message = "Created Successfully"});
            }
            else
            {
                return Json(new { success = false, message = "Fields are not correct" });
            }
            return Json(new { success = false, message = "Error while saving" });

            
        }
        [HttpPost("employee/delete")]
        public  IActionResult DeleteEmployee(int id)
        {
            if (_appDal.DeleteEmployee(id))
                return Json(new { success = true, message = "Delete Successfull!" });
            
            
            return Json(new { success = false, message = "Error while deleting." });
            
        }
        [HttpGet("employee/edit")]
        public IActionResult EditEmployee(int id)
        {
            try
            {
                Core.Employee employee =  _appDal.GetData().FirstOrDefault(e => e.EmployeeId == id);
                if (employee == null)
                {
                    return NotFound();
                }

                ViewData["Title"] = "Personal Information";
                ViewData["Employee"] = employee;
                return PartialView("~/Views/Employee/_AddForm.cshtml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Error while deleting." });
            }
        }

        private bool ValidateEmployee(Core.Employee employee)
        {
            bool empNameValid = true;
            bool empAddValid = true;
            bool empPhoneValid = true;
            if (string.IsNullOrEmpty(employee.EmployeeFullName))
                empNameValid = false;
            if (string.IsNullOrEmpty(employee.Address))
                empAddValid = false;
            if (string.IsNullOrEmpty(employee.PhoneNumber) && employee.PhoneNumber.Length > 10)
                empPhoneValid = false;
                
                
            return (empNameValid && empAddValid && empPhoneValid);
        }

    }
}
