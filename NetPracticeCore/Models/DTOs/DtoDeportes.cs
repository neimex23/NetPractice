using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetPracticeCore.Models.DTOs
{
    public class DtoDeportes
    {
        [Required]
        public string Nombre { get; set; }
    }
}
