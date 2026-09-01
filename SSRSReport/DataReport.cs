using PTUtility.Interfaces.Data;
using System.Data.SqlClient;
using System.Data;
using PTData.Classes;

namespace SSRSReport
{
    public class DataReport
    {
        public IQueryProcess ssrsReport(string navId)
        {
            return _ssrsReport(navId);
        }

        private IQueryProcess _ssrsReport(string navId)
        {
            try
            {
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "dw_mst_rpt_ssrs_report";
                objCommand.Parameters.AddWithValue("@module_id", navId);
                objCommand.Parameters.AddWithValue("@report_id", navId);
                return DataProcess.Execute(objCommand);
            }
            catch
            {
                throw;
            }
        }
    }
}