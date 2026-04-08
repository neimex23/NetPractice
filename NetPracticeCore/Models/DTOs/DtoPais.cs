using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetPracticeCore.Models.DTOs
{
    public class DtoPais
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaFundacion { get; set; }

        [Required]
        public string DeporteId { get; set; }

        [Required]
        public string ConfederacionId { get; set; }
    }
}
