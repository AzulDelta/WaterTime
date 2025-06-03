using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterTimeServer.Shared.DTOs
{
    public sealed class DTOConfirmarEmail_req
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
    }
}
