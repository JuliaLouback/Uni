#pragma checksum "D:\PROJETOS\Uni\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f318525cdadd994d741e69815168113eadf54b8c"
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
#nullable restore
#line 1 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f318525cdadd994d741e69815168113eadf54b8c", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef49804c5c32e2a2a3e62431713bb4856e3bf6f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Uni Construções";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4 mb-5 mt-4 text-primary\">Uni Construções</h1>\r\n</div>\r\n<div class=\"row\">\r\n");
#nullable restore
#line 11 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
     if (User.Identity.IsAuthenticated)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Fornecedores</h4>
                        <img src=""https://image.flaticon.com/icons/svg/411/411760.svg"" />
                        <a href=""/Fornecedores"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 24 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 25 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente") || User.IsInRole("Vendedor"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Clientes</h4>
                        <img src=""https://image.flaticon.com/icons/svg/912/912318.svg"" />
                        <a href=""/Clientes"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 36 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente") || User.IsInRole("RH"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Funcionários</h4>
                        <img src=""https://image.flaticon.com/icons/svg/1670/1670353.svg"" width=""262"" />
                        <a href=""/Funcionarios"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 48 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Produtos</h4>
                        <img src=""https://image.flaticon.com/icons/svg/1524/1524539.svg"" width=""262"" />
                        <a href=""/Produtoes"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 60 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente") || User.IsInRole("Vendedor"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
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
#line 72 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente") || User.IsInRole("Vendedor"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Cotações</h4>
                        <img src=""https://image.flaticon.com/icons/svg/1250/1250673.svg"" width=""261"" />
                        <a href=""/CotacaoProdutoes"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 84 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 85 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">CFOP</h4>
                        <img src=""https://image.flaticon.com/icons/svg/2170/2170919.svg"" width=""261"" class=""p-2""/>
                        <a href=""/CFOPs"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 96 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">CST</h4>
                        <img src=""https://image.flaticon.com/icons/svg/522/522575.svg"" width=""261"" class=""p-2"" />
                        <a href=""/CSTs"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 108 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 109 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">NCM</h4>
                        <img src=""https://image.flaticon.com/icons/svg/808/808730.svg"" width=""261"" class=""p-2""/>
                        <a href=""/NCMs"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 120 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 121 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         if (User.IsInRole("Admin"))

        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Endereços</h4>
                        <img src=""https://image.flaticon.com/icons/svg/967/967848.svg"" width=""262"" />
                        <a href=""/Enderecos"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
            WriteLiteral(@"            <div class=""col-lg-4 col-md-6 col-sm-12 mt-3"">
                <div class=""card border border-primary"" style=""width: 19rem;"">
                    <div class=""card-body"">
                        <h4 class=""card-title text-primary"">Telefones</h4>
                        <img src=""https://image.flaticon.com/icons/svg/1250/1250592.svg"" width=""262"" />
                        <a href=""/Telefones"" class=""btn btn-outline-primary"">Saiba mais</a>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 143 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 143 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
         
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
#line 155 "D:\PROJETOS\Uni\Views\Home\Index.cshtml"
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
