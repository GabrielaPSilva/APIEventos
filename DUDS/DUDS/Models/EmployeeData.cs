using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("employeeData")]
    public partial class EmployeeData
    {
        [Key]
        [Column("emp_id")]
        public int EmpId { get; set; }
        [Required]
        [Column("emp_bankAccountNumber")]
        [StringLength(10)]
        public string EmpBankAccountNumber { get; set; }
        [Column("emp_salary")]
        public int EmpSalary { get; set; }
        [Required]
        [Column("emp_SSN")]
        [StringLength(11)]
        public string EmpSsn { get; set; }
        [Required]
        [Column("emp_lname")]
        [StringLength(32)]
        public string EmpLname { get; set; }
        [Required]
        [Column("emp_fname")]
        [StringLength(32)]
        public string EmpFname { get; set; }
        [Required]
        [Column("emp_fname2")]
        [StringLength(32)]
        public string EmpFname2 { get; set; }
        [Required]
        [Column("emp_fname3")]
        [StringLength(32)]
        public string EmpFname3 { get; set; }
        [Required]
        [Column("emp_fname4")]
        [StringLength(32)]
        public string EmpFname4 { get; set; }
        [Required]
        [Column("emp_fname5")]
        [StringLength(32)]
        public string EmpFname5 { get; set; }
        [Required]
        [Column("emp_fname6")]
        [StringLength(32)]
        public string EmpFname6 { get; set; }
        [Column("emp_manager")]
        public int EmpManager { get; set; }
    }
}
