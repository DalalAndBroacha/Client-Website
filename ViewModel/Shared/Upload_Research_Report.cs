using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace ViewModel.Shared
{
    public class Upload_Research_Report
    {
        public List<psp_rpt_scripwiseholding_scrip> script_data { get; set; }

        public List<psp_dsp_research_category> cat_data { get; set; }

        public List<psp_dsp_research_recommendation> reco_data { get; set; }

        public List<psp_dsp_equity_research_reports> reports_data { get; set; }


        [Required(ErrorMessage = "Please enter Title")]
        public string RepoTitle { get; set; }

        [Required(ErrorMessage = "Please choose a file to upload.")]
        public IFormFile reportFile { get; set; }
        public IFormFile updateReportFile { get; set; }

        [DataMember]
        public string category { get; set; }

        [DataMember]
        public string scrip_code { get; set; }

        [DataMember]
        public string Recomandation { get; set; }
        [DataMember]
        public int? rec_id { get; set; }

        [DataMember]
        public float targetPrice { get; set; }

        [DataMember]
        public string fileURL { get; set; }
        [DataMember]
        public string flag { get; set; }
        [DataMember]
        
        public bool chkDspWeb { get; set; }
    }

    //public class EmptyScripCodeAttribute : ValidationAttribute
    //{
    //    public string GetErrorMessage() =>
    //    $"Please choose a Script.";
    //    public override bool IsValid(object value)
    //    {
    //        string strValue = value as string;
    //        if (!string.IsNullOrEmpty(strValue))
    //        {
    //            if (strValue == "0")
    //            {
    //                return false;
    //            }
    //        }
    //        return true;
    //    } 
    //} (ErrorMessage = "Please choose a Script.")


}
