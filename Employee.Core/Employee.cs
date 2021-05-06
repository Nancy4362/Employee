using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Employee.Core
{
    public struct Position
    {
        public const string prjMgr = "Project Manager";
        public const string prdMgr = "Production Manager";
        public const string hrDir = "HR Director";
        public const string senEdr = "Senior Editor";
        public const string edr = "Editor";
        public const string genMgr = "General Manager";
        
    }
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
