using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EJCRUD4.Models
{
    public class cCuentas
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }

        public int ID_Origen { get; set; }
    }
}
