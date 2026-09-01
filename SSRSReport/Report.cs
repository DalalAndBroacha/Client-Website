using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PTUtility.Interfaces.Data;
using System.Data;
using System.Web.Script.Serialization;


namespace SSRSReport
{
    public class Report 
    {
        public IQueryProcess ssrsReport(string navId)
        {
            return (new DataReport()).ssrsReport(navId);
        }
    }
}