using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ViewModel.ClientComms
{
    public class EqSetDataModel
    {
        public string CODE { get; set; }
        public string SENDERACCOUNTNO { get; set; }
        public string AMOUNT { get; set; }
        

        public string BENEFICIARYACCOUNTNO { get; set; }
        public string BENEFICIARYNAME { get; set; }

        public string BENEFICIARYIFSC { get; set; }

        public string Remarks { get; set; }

        public string UTRNO { get; set; }
        //public string uploadFilename { get; set; }
        //public string uploadDate { get; set; }
        //public string uploadGUID { get; set; }

    }
}

