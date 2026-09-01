using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Shared
{
    public class DisplayEqScripHolCliList
    {
        public List<psp_dsp_BH_RM_list> BH_RM_list { get; set; }
        public List<psp_rpt_scripwiseholding_scrip> script_list { get; set; }

        public string rm_id { get; set; }

        public string scrip_code { get; set; }

    }
}
