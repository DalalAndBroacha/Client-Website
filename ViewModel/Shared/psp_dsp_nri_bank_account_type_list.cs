using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.Shared
{
    [DataContract]
    public class psp_dsp_nri_bank_account_type_list
    {
        [DataMember]
        public string bank_acc_type { get; set; }
        [DataMember]
        public string rec_id { get; set; }
    }
}
