using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace ViewModel.ClientComms
{
    [DataContract]
    public class psp_dsp_client_coms_outstanding_debit_send_mail
    {
        [DataMember]
        public string login_id { get; set; }
        [DataMember]
        public string trdate { get; set; }
        [DataMember]
        public string content_id { get; set; }
        [DataMember]
        public string client_name { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public string account_code { get; set; }
        [DataMember]
        public string dsp_amount
        {
            get
            {
                decimal num = Convert.ToDecimal(this.amount);
               
                return num.ToString("#,##0.00");
            }
        }
        [DataMember]
        public string str_acc_code { get; set; }
        [DataMember]
        public string str_amt { get; set; }
        [DataMember]
        public string as_on_date { get; set; }
        [DataMember]
        public string sql_msg { get; set; }
    }
}
