#pragma checksum "D:\PROJETOS\Uni\Views\Produtoes\ErroProduto.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04f5ea890b4759e8c3ff296f533d2503c75c3468"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Produtoes_ErroProduto), @"mvc.1.0.view", @"/Views/Produtoes/ErroProduto.cshtml")]
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
#line 1 "D:\PROJETOS\Uni\Views\_ViewImports.cshtml"
using Uni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJETOS\Uni\Views\_ViewImports.cshtml"
using Uni.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04f5ea890b4759e8c3ff296f533d2503c75c3468", @"/Views/Produtoes/ErroProduto.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Produtoes_ErroProduto : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\PROJETOS\Uni\Views\Produtoes\ErroProduto.cshtml"
  
    ViewData["Title"] = "Erro Produto";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-primary mt-2 mb-5\">Erro ao excluir produto!</h1>\r\n\r\n<h4>Produto <span class=\"text-danger\">");
#nullable restore
#line 9 "D:\PROJETOS\Uni\Views\Produtoes\ErroProduto.cshtml"
                                 Write(ViewBag.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> não pode ser excluído pois está vinculado a uma ou mais vendas e/ou cotações!</h4>\r\n\r\n\r\n");
#nullable restore
#line 12 "D:\PROJETOS\Uni\Views\Produtoes\ErroProduto.cshtml"
Write(Html.ActionLink("Voltar", "Delete", new { id = ViewBag.Id }, new { @class = "btn btn-primary mt-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
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
