using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;

namespace Sw.Hospital.HealthExaminationSystem.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class MyProjectNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    L("HomePage"),
                    url: "",
                    icon: "home",
                    requiresAuthentication: true
                //isVisible: false
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Tenants,
                    L("Tenants"),
                    url: "Tenants",
                    icon: "business",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)//,
                     //isVisible: false

                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Users,
                    L("Users"),
                    url: "Users",
                    icon: "people",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)//,
                     //isVisible: false
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Roles,
                    L("Roles"),
                    url: "Roles",
                    icon: "local_offer",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)//,
                     //isVisible: false
                )
            )
            //.AddItem(
            //        new MenuItemDefinition(
            //            "ResultStatic",
            //            L("体检统计"),
            //            url: "ResultStatic",
            //            icon: "local_offer",
            //            //requiredPermissionName: PermissionNames.Pages_Roles
            //            // 此处缺少合适的替代，需要从新下载一份项目源码进行参考
            //            // 升级 Abp 6.4.0 问题
            //            requiresAuthentication: true
            //        )
            //    )
                //.AddItem(
                //    new MenuItemDefinition(
                //        PageNames.About,
                //        L("About"),
                //        url: "About",
                //        icon: "info",
                //         isVisible: false
                //    )
                //)
                //.AddItem(
                //    new MenuItemDefinition(
                //        "SwaggerUI Api",
                //        new FixedLocalizableString("SwaggerUI Api"),
                //        url: "swagger/ui/index",
                //        icon: "info",
                //        target: "_blank",
                //         isVisible: false
                //    )
                //)
                //.AddItem( //Menu items below is just for demonstration!
                //    new MenuItemDefinition(
                //        "MultiLevelMenu",
                //        L("MultiLevelMenu"),
                //        icon: "menu",
                //         isVisible:false
                //    ).AddItem(
                //        new MenuItemDefinition(
                //            "AspNetBoilerplate",
                //            new FixedLocalizableString("ASP.NET Boilerplate")
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetBoilerplateHome",
                //                new FixedLocalizableString("Home"),
                //                url: "https://aspnetboilerplate.com?ref=abptmpl"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetBoilerplateTemplates",
                //                new FixedLocalizableString("Templates"),
                //                url: "https://aspnetboilerplate.com/Templates?ref=abptmpl"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetBoilerplateSamples",
                //                new FixedLocalizableString("Samples"),
                //                url: "https://aspnetboilerplate.com/Samples?ref=abptmpl"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetBoilerplateDocuments",
                //                new FixedLocalizableString("Documents"),
                //                url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl"
                //            )
                //        )
                //    ).AddItem(
                //        new MenuItemDefinition(
                //            "AspNetZero",
                //            new FixedLocalizableString("ASP.NET Zero")
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroHome",
                //                new FixedLocalizableString("Home"),
                //                url: "https://aspnetzero.com?ref=abptmpl"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroDescription",
                //                new FixedLocalizableString("Description"),
                //                url: "https://aspnetzero.com/?ref=abptmpl#description"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroFeatures",
                //                new FixedLocalizableString("Features"),
                //                url: "https://aspnetzero.com/?ref=abptmpl#features"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroPricing",
                //                new FixedLocalizableString("Pricing"),
                //                url: "https://aspnetzero.com/?ref=abptmpl#pricing"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroFaq",
                //                new FixedLocalizableString("Faq"),
                //                url: "https://aspnetzero.com/Faq?ref=abptmpl"
                //            )
                //        ).AddItem(
                //            new MenuItemDefinition(
                //                "AspNetZeroDocuments",
                //                new FixedLocalizableString("Documents"),
                //                url: "https://aspnetzero.com/Documents?ref=abptmpl"
                //            )
                //        )
                //    )
                //)
                ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
        }
    }
}
