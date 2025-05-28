using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.SharedKernel.Models.Request
{
    public class GuidIdRequestDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
