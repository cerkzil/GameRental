#pragma checksum "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a43bebd2c58c5a03e6be482c9135c6b86575d6fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\_ViewImports.cshtml"
using GR.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\_ViewImports.cshtml"
using GR.MVC.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\_ViewImports.cshtml"
using System.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
using GR.Domains.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a43bebd2c58c5a03e6be482c9135c6b86575d6fa", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad3234fe8f8b4f7188c074cdf7c3d25d29d4c9aa", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GR.Domains.Game>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome To GameRentals!</h1>\r\n    <p>Rent your favorite games for cheap!</p>\r\n");
#nullable restore
#line 10 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
     using (Html.BeginForm("Index", "Home", FormMethod.Post))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"form-row\">\r\n            <div class=\"form-group col-md-3\">\r\n                ");
#nullable restore
#line 14 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
           Write(Html.DropDownList("filter", new SelectList(Enum.GetValues(typeof(Genre))),
                "Select Genre...", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"form-group col-md-3\">\r\n                ");
#nullable restore
#line 18 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
           Write(Html.DropDownList("filter2", new SelectList(Enum.GetValues(typeof(Platform))),
                "Select Platform...", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"form-group\">\r\n                <input type=\"submit\" value=\"Filter\" class=\"btn btn-primary\" />\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 25 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\r\n");
#nullable restore
#line 27 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card\" style=\"width: 16rem;\">\r\n                <img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 1134, "\"", 1153, 1);
#nullable restore
#line 30 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
WriteAttributeValue("", 1140, item.ImgLink, 1140, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1154, "\"", 1171, 1);
#nullable restore
#line 30 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
WriteAttributeValue("", 1160, item.Title, 1160, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"125\" height=\"325\">\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">");
#nullable restore
#line 32 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
                                      Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 33 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
                        var x = "";
                        foreach (var p in item.PlatformList)
                        {
                            var platform = p.Platform + " ";
                            x = x + platform;
                        }
                        var y = "";
                        foreach (var g in item.GenreList)
                        {
                            var genre = g.Genre + " ";
                            y = y + genre;
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p class=\"card-text\">Platforms: ");
#nullable restore
#line 45 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
                                                   Write(x);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p class=\"card-text\">Genres: ");
#nullable restore
#line 46 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
                                                Write(y);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
            WriteLiteral("                    <a href=\"#\" class=\"btn btn-primary\">Order</a>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 51 "C:\Users\milka\Documents\GitHub\GameRental\GameRental\GR.MVC\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n<style>\r\n    .card {\r\n        padding: 5px;\r\n    }\r\n</style>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GR.Domains.Game>> Html { get; private set; }
    }
}
#pragma warning restore 1591
