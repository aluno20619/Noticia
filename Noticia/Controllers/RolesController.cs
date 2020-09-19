using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Noticia.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    namespace TestAppAuthAndAuthorize.Controllers
    {

        /// <summary>
        /// Este código foi tirado de https://www.c-sharpcorner.com/article/adding-role-authorization-to-a-asp-net-mvc-core-application/
        /// </summary>
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Identity;
        using Microsoft.AspNetCore.Mvc;
        using System.Linq;
        using System.Threading.Tasks;

        public class RolesController : Controller
        {
            RoleManager<IdentityRole> roleManager;

            public RolesController(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }

            [AllowAnonymous]
            public IActionResult Index()
            {
                var roles = roleManager.Roles.ToList();
                return View(roles);
            }

            [Authorize(Policy = "writepolicy")]
            public IActionResult Create()
            {
                return View(new IdentityRole());
            }

            [HttpPost]
            public async Task<IActionResult> Create(IdentityRole role)
            {
                await roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
        }
    }
}