#pragma checksum "C:\Users\julia\Music\Uni\Views\CotacaoProdutoes\ErroEditProd.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c65a812008a35cbf6c1cbe5c63d55eb7b6f83b74"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CotacaoProdutoes_ErroEditProd), @"mvc.1.0.view", @"/Views/CotacaoProdutoes/ErroEditProd.cshtml")]
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
#line 1 "C:\Users\julia\Music\Uni\Views\_ViewImports.cshtml"
using Uni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\julia\Music\Uni\Views\_ViewImports.cshtml"
using Uni.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c65a812008a35cbf6c1cbe5c63d55eb7b6f83b74", @"/Views/CotacaoProdutoes/ErroEditProd.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_CotacaoProdutoes_ErroEditProd : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\julia\Music\Uni\Views\CotacaoProdutoes\ErroEditProd.cshtml"
  
    ViewData["Title"] = "Erro";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-primary mt-2 mb-5\">Quantidade máxima ultrapassada!</h1>\r\n\r\n<h4>O produto ");
#nullable restore
#line 8 "C:\Users\julia\Music\Uni\Views\CotacaoProdutoes\ErroEditProd.cshtml"
         Write(ViewBag.Produto);

#line default
#line hidden
#nullable disable
            WriteLiteral(" possui apenas <span class=\"text-danger\">");
#nullable restore
#line 8 "C:\Users\julia\Music\Uni\Views\CotacaoProdutoes\ErroEditProd.cshtml"
                                                                  Write(ViewBag.Erro);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> peças no estoque!</h4>\r\n\r\n\r\n");
#nullable restore
#line 11 "C:\Users\julia\Music\Uni\Views\CotacaoProdutoes\ErroEditProd.cshtml"
Write(Html.ActionLink("Voltar", "EditProd", new { @class = "btn btn-primary mt-5" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
