using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("auditEmployeeData")]
    public partial class AuditEmployeeData
    {
        [Key]
        [Column("audit_log_id")]
        public Guid AuditLogId { get; set; }
        [Required]
        [Column("audit_log_type")]
        [StringLength(3)]
        public string AuditLogType { get; set; }
        [Column("audit_emp_id")]
        public int AuditEmpId { get; set; }
        [Column("audit_emp_bankAccountNumber")]
        [StringLength(10)]
        public string AuditEmpBankAccountNumber { get; set; }
        [Column("audit_emp_salary")]
        public int? AuditEmpSalary { get; set; }
        [Column("audit_emp_SSN")]
        [StringLength(11)]
        public string AuditEmpSsn { get; set; }
        [Required]
        [Column("audit_user")]
        [StringLength(128)]
        public string AuditUser { get; set; }
        [Column("audit_changed", TypeName = "datetime")]
        public DateTime? AuditChanged { get; set; }
    }
}
