#pragma checksum "D:\Flutter Udemy Course\node-tutorial-practice\PRD High School\PRD High School\Views\Account\NotFound.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2b0b2a3629630f5278785d186bc1b3dc7794871c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_NotFound), @"mvc.1.0.view", @"/Views/Account/NotFound.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/NotFound.cshtml", typeof(AspNetCore.Views_Account_NotFound))]
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
#line 1 "D:\Flutter Udemy Course\node-tutorial-practice\PRD High School\PRD High School\Views\_ViewImports.cshtml"
using PRD_High_School;

#line default
#line hidden
#line 2 "D:\Flutter Udemy Course\node-tutorial-practice\PRD High School\PRD High School\Views\_ViewImports.cshtml"
using PRD_High_School.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2b0b2a3629630f5278785d186bc1b3dc7794871c", @"/Views/Account/NotFound.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e9f673b3b46390512c185aa46f2b288729e3f13", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_NotFound : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\Flutter Udemy Course\node-tutorial-practice\PRD High School\PRD High School\Views\Account\NotFound.cshtml"
  
    ViewData["Title"] = "NotFound";
    Layout = "~/Views/Shared/_MainPage.cshtml";

#line default
#line hidden
            BeginContext(95, 29, true);
            WriteLiteral("\r\n<div class=\"container\"><h2>");
            EndContext();
            BeginContext(125, 20, false);
#line 7 "D:\Flutter Udemy Course\node-tutorial-practice\PRD High School\PRD High School\Views\Account\NotFound.cshtml"
                      Write(ViewBag.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(145, 15, true);
            WriteLiteral("</h2></div>\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
