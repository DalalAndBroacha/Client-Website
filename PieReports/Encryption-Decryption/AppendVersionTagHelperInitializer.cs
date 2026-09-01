using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace PieReports.Encryption_Decryption
{
    //To set Global Asp-append-version to true
    // Source https://mattferderer.com/set-asp-append-version-true-by-default-in-all-script-js-style-css-tags
    public class AppendVersionTagHelperInitializer :
    ITagHelperInitializer<ScriptTagHelper>,
    ITagHelperInitializer<LinkTagHelper>
    {
        private const bool DefaultValue = true;

        public void Initialize(ScriptTagHelper helper, ViewContext context)
        {
            helper.AppendVersion = DefaultValue;
        }

        public void Initialize(LinkTagHelper helper, ViewContext context)
        {
            helper.AppendVersion = DefaultValue;
        }
    
    }
}
