using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ViewModel.ClientComms
{
    [XmlRoot("RESULT"), XmlType("RESULT")]
    public class smsXmlResponse
    {
        [XmlAttribute("REQID")]
        public string REQID { get; set; }

        [XmlElement(ElementName = "MID")]
        public Mid MID { get; set; }
    }
    public class Mid
    {
        [XmlAttribute]
        public string TID { get; set; }

        [XmlAttribute]
        public string SUBMITDATE { get; set; }
    }
}

//< !DOCTYPE RESULT SYSTEM 'http://bulkpush.mytoday.com/BulkSms/BulkSmsRespV1.01.dtd'>
//<RESULT REQID = '62455323444'>
//    <MID SUBMITDATE = '2025-01-21 12:22:01' ID = '1' TAG = 'null' TID = '19499557637'>
//    </MID>
//</RESULT>
