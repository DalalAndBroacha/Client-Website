using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PieReports.Utility
{
    /// <summary>
    /// WCAG 4.1.1 Parsing — ids must be unique in a page.
    ///
    /// The report-header and toolbar partials hard-code their ids
    /// (lblReport_Name, report_start_date, header_div_…), and several report
    /// views render those partials once per section, so the same id appeared
    /// two to seven times on one page.
    ///
    /// The first element to ask for an id keeps it unchanged; later ones get a
    /// numeric suffix (-2, -3, …). Scripts reach these elements with
    /// $("#id"), which only ever returns the first match in the document, so
    /// the elements they already used keep the ids they already had.
    /// </summary>
    public static class A11yIds
    {
        private const string Key = "A11yIds.Used";

        public static string Unique(HttpContext context, string id)
        {
            if (string.IsNullOrEmpty(id) || context == null) { return id; }
            var used = context.Items[Key] as HashSet<string>;
            if (used == null)
            {
                used = new HashSet<string>();
                context.Items[Key] = used;
            }
            var candidate = id;
            var n = 2;
            while (!used.Add(candidate)) { candidate = id + "-" + n++; }
            return candidate;
        }
    }
}
