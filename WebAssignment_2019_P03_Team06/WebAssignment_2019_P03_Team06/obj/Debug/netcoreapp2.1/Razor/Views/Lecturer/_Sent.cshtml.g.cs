#pragma checksum "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2f9bef2c51d27b8825227ed380b02ada785518aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Lecturer__Sent), @"mvc.1.0.view", @"/Views/Lecturer/_Sent.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Lecturer/_Sent.cshtml", typeof(AspNetCore.Views_Lecturer__Sent))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 3 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f9bef2c51d27b8825227ed380b02ada785518aa", @"/Views/Lecturer/_Sent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccc84e73d4e21640c8f3fc9d573333fbf920e45a", @"/Views/_ViewImports.cshtml")]
    public class Views_Lecturer__Sent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebAssignment_2019_P03_Team06.Models.SentSuggestion>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(72, 159, true);
            WriteLiteral("\n<style>\n    table {\n        font-family: arial, sans-serif;\n        width: 100%;\n    }\n\n    td, th {\n        border: 1px solid #dddddd;\n    }\n    }\n</style>\n\n");
            EndContext();
#line 15 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
 if (Model.ToList().Count > 0)
{

#line default
#line hidden
            BeginContext(264, 475, true);
            WriteLiteral(@"    <div class=""table-responsive"">
        <table id=""viewStaff"" class=""table table-striped table-bordered"">
            <thead class=""thead-dark"">
                <tr>
                    <th>Suggestion ID</th>
                    <th>To Student ID</th>
                    <th>Description</th>
                    <th>Status</th>
                    <th>Date Created</th>
                    
                </tr>
            </thead>
            <tbody>
                
");
            EndContext();
#line 31 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                 foreach (var item in Model)
                {
                   

                    
                    

#line default
#line hidden
#line 36 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                     if (item.LecturerId == Context.Session.GetInt32("LecturerId"))
                    {

#line default
#line hidden
            BeginContext(950, 53, true);
            WriteLiteral("                    <tr>\n                        <td>");
            EndContext();
            BeginContext(1004, 28, false);
#line 39 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                       Write(item.SuggestionId.ToString());

#line default
#line hidden
            EndContext();
            BeginContext(1032, 34, true);
            WriteLiteral("</td>\n                        <td>");
            EndContext();
            BeginContext(1067, 14, false);
#line 40 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                       Write(item.StudentId);

#line default
#line hidden
            EndContext();
            BeginContext(1081, 34, true);
            WriteLiteral("</td>\n                        <td>");
            EndContext();
            BeginContext(1116, 16, false);
#line 41 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                       Write(item.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1132, 34, true);
            WriteLiteral("</td>\n                        <td>");
            EndContext();
            BeginContext(1167, 11, false);
#line 42 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                       Write(item.Status);

#line default
#line hidden
            EndContext();
            BeginContext(1178, 34, true);
            WriteLiteral("</td>\n                        <td>");
            EndContext();
            BeginContext(1213, 16, false);
#line 43 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                       Write(item.DateCreated);

#line default
#line hidden
            EndContext();
            BeginContext(1229, 58, true);
            WriteLiteral(" </td>\n\n                       \n                    </tr>\n");
            EndContext();
#line 47 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                    }

#line default
#line hidden
#line 47 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
                     
                    

                    
                }

#line default
#line hidden
            BeginContext(1370, 49, true);
            WriteLiteral("            </tbody>\n        </table>\n    </div>\n");
            EndContext();
#line 55 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(1428, 55, true);
            WriteLiteral("    <span style=\"color:red\">No Sent Suggestion!</span>\n");
            EndContext();
#line 59 "C:\Users\victo\Downloads\px29-web2019asn_p03_team6-8ca8f72aee1d\px29-web2019asn_p03_team6-8ca8f72aee1d\WebAssignment_2019_P03_Team06\WebAssignment_2019_P03_Team06\Views\Lecturer\_Sent.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebAssignment_2019_P03_Team06.Models.SentSuggestion>> Html { get; private set; }
    }
}
#pragma warning restore 1591
