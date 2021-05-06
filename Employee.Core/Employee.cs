using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Employee.Core
{
    public enum Position { Project_Manager, Production_Mgr, HR_Dir, Senior_Editor, Editor, Gen_Mgr, Prod_Mgr }
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeFullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
