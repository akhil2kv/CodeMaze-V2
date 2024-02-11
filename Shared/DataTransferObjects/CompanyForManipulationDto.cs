using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Address is 100 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Company country is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 characters.")]
        public string? Country { get; set; }

        public IEnumerable<EmployeeForCreationDto>? Employees { get; set; }
    }
}
