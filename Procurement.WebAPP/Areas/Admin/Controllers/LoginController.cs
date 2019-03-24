using Procurement.Model.Users;
using Procurement.Repository.EF;
using Procurement.Repository.Interface.Users;
using Procurement.Service.Interface.Users;
using Procurement.Service.Users;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{

    [Authorize(Roles = "Supplier,Custumer")]
    public class LoginController : Controller
    {

        protected IServiceUser iServiceUser;
        protected IUnitOfWorkAsync iUnitOfWorkAsync;

        public LoginController(IUnitOfWorkAsync iUnitOfWorkAsync,
                               IServiceUser iServiceUser)
        {
            this.iUnitOfWorkAsync = iUnitOfWorkAsync;
            this.iServiceUser = iServiceUser;
        }


        public ActionResult Login()
        {
            this.ViewBag.SideCollapse = true;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.LoginViewModel login, string ReturnUrl = "")
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            //  Membership.ValidateUser(login.Email, login.Password);

            //UserBusinessLogic bl = new UserBusinessLogic();
            //User userTemp = bl.Validade(user.Email, user.Password);



            //if (userTemp == null)
            //{
            //    this.ModelState.AddModelError("", "Dados inválidos!!!!!");
            //    return View(user);
            //}

            if (!iServiceUser.Validade(login.Email, login.Password))
            {
                ModelState.AddModelError(string.Empty, "Not found user!");
                return View();
            }

            FormsAuthentication.SetAuthCookie(login.Email, false);

            ModelState.Remove("Password");

            //User user = this.iServiceUser.GetByEmail(login.Email);

            //this.ViewBag.User = user;


            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logoff()
        {
            this.ViewBag.SideCollapse = true;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Item");
        }


    }
}