#pragma checksum "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5bda0e712456b03446957580b32e58ae04a5c8f48ec29846b98f79125becfd00"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Hr_HrProfile), @"mvc.1.0.view", @"/Views/Hr/HrProfile.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Hrms\Hrms\Views\_ViewImports.cshtml"
using Hrms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Hrms\Hrms\Views\_ViewImports.cshtml"
using Hrms.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"5bda0e712456b03446957580b32e58ae04a5c8f48ec29846b98f79125becfd00", @"/Views/Hr/HrProfile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"7dfa210df43d1080c36bc92f21c6c863221c11253f533dc4bdc977df9cda0e37", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Hr_HrProfile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<main id=""main"" class=""main"">

    <div class=""pagetitle"">
        <h1>Profile</h1>
    </div>
    <section class=""section profile"">
        <div class=""row"">

            <div class=""col-xl-4"">
                <div class=""card"">
                    <div class=""card-body profile-card pt-4 d-flex flex-column align-items-center"">
                        <img");
            BeginWriteAttribute("src", " src=\"", 369, "\"", 391, 1);
#nullable restore
#line (12,35)-(12,51) 29 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
WriteAttributeValue("", 375, Model.EmpImages, 375, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Employee Image\" />\r\n                        <p>");
#nullable restore
#line (13,29)-(13,44) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line (13,46)-(13,60) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                    </div>
                </div>

            </div>

            <div class=""col-xl-8"">
                <div class=""card"">
                    <div class=""card-body pt-3"">
                        <ul class=""nav nav-tabs nav-tabs-bordered"">
                            <li class=""nav-item"">
                                <button class=""nav-link active"" data-bs-toggle=""tab"" data-bs-target=""#profile-overview"">Profile Details</button>
                            </li>
                        </ul>
                        <div class=""tab-content pt-2"">
                            <div class=""tab-pane fade show active profile-overview"" id=""profile-overview"">


                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Full Name</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (33,69)-(33,84) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line (33,86)-(33,100) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Employee Code</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (38,69)-(38,87) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.EmployeeCode);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Department</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (43,69)-(43,89) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.DepartmentName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Manager</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (48,69)-(48,86) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.ManagerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                </div>\r\n                                <div class=\"row\">\r\n                                    <div class=\"col-lg-3 col-md-4 label\">Gender</div>\r\n                                    <div class=\"col-lg-9 col-md-8\">");
#nullable restore
#line (52,69)-(52,81) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Date of Birth</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (57,69)-(57,106) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.DateOfBirth.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Designation</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (62,69)-(62,86) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.Designation);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Date of Joining</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (67,69)-(67,108) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.DateOfJoining.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Father's Name</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (72,69)-(72,85) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.FatherName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Marital Status</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (77,69)-(77,88) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.MaritalStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Employee Contact</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (82,69)-(82,90) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.EmployeeContact);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Email (Official)</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (87,69)-(87,88) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.EmailOfficial);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>

                                <div class=""row"">
                                    <div class=""col-lg-3 col-md-4 label"">Present Address</div>
                                    <div class=""col-lg-9 col-md-8"">");
#nullable restore
#line (92,69)-(92,89) 6 "E:\Hrms\Hrms\Views\Hr\HrProfile.cshtml"
Write(Model.PresentAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                                </div>



                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
</main><!-- End #main -->
<a href=""#"" class=""back-to-top d-flex align-items-center justify-content-center""><i class=""bi bi-arrow-up-short""></i></a>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
