using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Services.DTOS.RoleManagementDto
{
    public class RoleDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
