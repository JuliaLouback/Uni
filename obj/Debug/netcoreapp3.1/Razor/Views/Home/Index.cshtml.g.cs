#pragma checksum "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d70dd7881d33e6150a9be0ce577da90668eda1e9"
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
#line 1 "C:\Users\user\Documents\trabalho\Uni\Views\_ViewImports.cshtml"
using Uni;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\Documents\trabalho\Uni\Views\_ViewImports.cshtml"
using Uni.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d70dd7881d33e6150a9be0ce577da90668eda1e9", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Uni Construções";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4 mb-5 mt-4 text-primary\">Uni Construções</h1>\r\n</div>\r\n<div class=\"row\">\r\n");
#nullable restore
#line 11 "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml"
     if (User.Identity.IsAuthenticated)
    {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
            <div class=""card border border-primary"" style=""width: 19rem;"">
                <div class=""card-body"">
                    <h4 class=""card-title text-primary"">Fornecedores</h4>
                    <img src=""https://image.flaticon.com/icons/svg/411/411760.svg"" />
                    <a href=""/Fornecedores"" class=""btn btn-outline-primary"">Saiba mais</a>
                </div>
            </div>
        </div>
");
            WriteLiteral(@"        <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
            <div class=""card border border-primary"" style=""width: 19rem;"">
                <div class=""card-body"">
                    <h4 class=""card-title text-primary"">Clientes</h4>
                    <img src=""https://image.flaticon.com/icons/svg/912/912318.svg"" />
                    <a href=""/Clientes"" class=""btn btn-outline-primary"">Saiba mais</a>
                </div>
            </div>
        </div>
");
            WriteLiteral(@"        <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
            <div class=""card border border-primary"" style=""width: 19rem;"">
                <div class=""card-body"">
                    <h4 class=""card-title text-primary"">Funcionários</h4>
                    <img src=""https://image.flaticon.com/icons/svg/1670/1670353.svg"" width=""262"" />
                    <a href=""/Funcionarios"" class=""btn btn-outline-primary"">Saiba mais</a>
                </div>
            </div>
        </div>
");
            WriteLiteral(@"        <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
            <div class=""card border border-primary"" style=""width: 19rem;"">
                <div class=""card-body"">
                    <h4 class=""card-title text-primary"">Produtos</h4>
                    <img src=""https://image.flaticon.com/icons/svg/1524/1524539.svg"" width=""262"" />
                    <a href=""/Produtoes"" class=""btn btn-outline-primary"">Saiba mais</a>
                </div>
            </div>
        </div>
");
            WriteLiteral(@"        <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
            <div class=""card border border-primary"" style=""width: 19rem;"">
                <div class=""card-body"">
                    <h4 class=""card-title text-primary"">Vendas</h4>
                    <img src=""https://image.flaticon.com/icons/svg/454/454637.svg"" width=""262"" />
                    <a href=""/VendaProdutoes"" class=""btn btn-outline-primary"">Saiba mais</a>
                </div>
            </div>
        </div>
");
#nullable restore
#line 63 "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml"

    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""col-sm-12"">
            <div class=""jumbotron jumbotron-fluid"">
                <div class=""container"">
                    <h1 class=""display-4"">A melhor loja de Materias de Construção!</h1>
                    <p class=""lead"">A mais de 20 anos no mercado.</p>
                </div>
            </div>
        </div>
");
#nullable restore
#line 75 "C:\Users\user\Documents\trabalho\Uni\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IAuthorizationService AuthorizationService { get; private set; }
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
