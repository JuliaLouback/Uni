#pragma checksum "C:\Users\julia\3D Objects\Uni\Views\Fornecedores\ErroFornecedor.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "52f3c3fde51ceb8e38a5578f93e23d2a249ddb5e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fornecedores_ErroFornecedor), @"mvc.1.0.view", @"/Views/Fornecedores/ErroFornecedor.cshtml")]
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
#line 1 "C:\Users\julia\3D Objects\Uni\Views\_ViewImports.cshtml"
using Uni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\julia\3D Objects\Uni\Views\_ViewImports.cshtml"
using Uni.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52f3c3fde51ceb8e38a5578f93e23d2a249ddb5e", @"/Views/Fornecedores/ErroFornecedor.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Fornecedores_ErroFornecedor : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\julia\3D Objects\Uni\Views\Fornecedores\ErroFornecedor.cshtml"
  
    ViewData["Title"] = "Erro Fornecedor";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 class=\"text-primary mt-2 mb-5\">Erro ao excluir fornecedor!</h1>\r\n\r\n<h4>O fornecedor <span class=\"text-danger\">");
#nullable restore
#line 9 "C:\Users\julia\3D Objects\Uni\Views\Fornecedores\ErroFornecedor.cshtml"
                                      Write(ViewBag.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> - <span class=\"text-danger\">");
#nullable restore
#line 9 "C:\Users\julia\3D Objects\Uni\Views\Fornecedores\ErroFornecedor.cshtml"
                                                                                       Write(ViewBag.Cnpj);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> não pode ser excluído pois está vinculado a um ou mais produtos, exclua os produtos primeiro!</h4>\r\n\r\n\r\n");
#nullable restore
#line 12 "C:\Users\julia\3D Objects\Uni\Views\Fornecedores\ErroFornecedor.cshtml"
Write(Html.ActionLink("Voltar", "Delete", new { id = ViewBag.Id}, new { @class = "btn btn-primary mt-2" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
