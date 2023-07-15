using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [MetadataType(typeof(LoginMetadata))]
    public class Login
    {
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
    }
}
