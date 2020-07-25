using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CodingCraftEx04.Domain.Infra.Identity;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Enum;
using CodingCraftEx04.MVC.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CodingCraftEx04.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await UserManager.FindByIdAsync(id);
            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);
            ViewBag.Permissoes = await UserManager.GetClaimsAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, string[] selectedRoles, params string[] selectedPermissoes)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    Id = Guid.NewGuid(),
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email
                };
                var adminResult = await UserManager.CreateAsync(user, userViewModel.Password);

                if (adminResult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }

                    // Permissões
                    if (selectedPermissoes != null)
                        foreach (var permissao in selectedPermissoes)
                            await UserManager.AddClaimAsync(user.Id, new Claim(permissao, "True"));
                }
                else
                {
                    ModelState.AddModelError("", adminResult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            var userPermissoes = await UserManager.GetClaimsAsync(user.Id);


            return View(new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                Permissoes = userPermissoes.ToList().Select(x => x.Type)
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser, string[] selectedPermissoes,
            params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                    return HttpNotFound();

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                // Roles
                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                // permissões
                var permissoesAntigas = await UserManager.GetClaimsAsync(user.Id);

                //Adiciona se não houver
                foreach (var permissao in selectedPermissoes)
                    if (permissoesAntigas.Count(x => x.Type == permissao) == 0)
                        await UserManager.AddClaimAsync(user.Id, new Claim(permissao, "True"));

                // Remove se houver algo pra remover
                foreach (var permissao in permissoesAntigas)
                    if (!selectedPermissoes.Contains(permissao.Type))
                        await UserManager.RemoveClaimAsync(user.Id, permissao);

                // Verifica se é o mesmo usuário, se for, loga novamente para atualizar o cookie
                if (user.Id.ToString() == User.Identity.GetUserId())
                    await SignInManager.SignInAsync(user, true, false);


                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            if (ModelState.IsValid)
            {
                if (id == Guid.Empty)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                    return HttpNotFound();

                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}