using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.ClientComms
{
    public class EquitySettlement
    {
        [Required(ErrorMessage = "Please choose a file to upload.")]
        public IFormFile reportFile { get; set; }
        public DateTime uploadDate { get; set; }
    }
    
}
