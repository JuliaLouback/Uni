#pragma checksum "D:\PROJETOS\Uni\Views\CotacaoProdutoes\ErroListaEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c5fc862123d37f0dd47bd12ec25809c2f26613b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CotacaoProdutoes_ErroListaEdit), @"mvc.1.0.view", @"/Views/CotacaoProdutoes/ErroListaEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c5fc862123d37f0dd47bd12ec25809c2f26613b", @"/Views/CotacaoProdutoes/ErroListaEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_CotacaoProdutoes_ErroListaEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\PROJETOS\Uni\Views\CotacaoProdutoes\ErroListaEdit.cshtml"
  
    ViewData["Title"] = "Erro Cotação";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-primary mt-2 mb-5\">Erro ao finalizar cotação!</h1>\r\n\r\n<h4>Lista de produtos vazia, insira um ou mais produtos para finalizar a cotação!</h4>\r\n\r\n");
#nullable restore
#line 11 "D:\PROJETOS\Uni\Views\CotacaoProdutoes\ErroListaEdit.cshtml"
Write(Html.ActionLink("Voltar", "Edit", new { id = ViewBag.Editar }, new { @class = "btn btn-primary mt-2" }));

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
