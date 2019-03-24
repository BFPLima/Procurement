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

namespace Procurement.WebAPP.Controllers
{
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

            if (this.Request.RawUrl.Contains("Admin") && this.Request.RawUrl.Contains("Logoff"))
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

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






            if (!iServiceUser.Validade(login.Email, login.Password))
            {
                ModelState.AddModelError(string.Empty, "Not found user!");
                return View();
            }

            User user = iServiceUser.GetByEmail(login.Email);

            if (user.IsInRole("Admin") && !ReturnUrl.StartsWith("/Admin"))
            {
                ModelState.AddModelError(string.Empty, "Admin can not log in this module!" + Environment.NewLine + "Please go to Admin module and try to log in.");
                return View();
            }
            FormsAuthentication.SetAuthCookie(login.Email, false);

            ModelState.Remove("Password");


            this.ViewBag.BGColorMenu = true;

            if (Url.IsLocalUrl(ReturnUrl))
            {

                if (ReturnUrl.Equals("/Admin/Login/Login"))
                {
                    return Redirect("/Admin/Admin/Dashboard");
                }
                return Redirect(ReturnUrl);
            }



            if (user.IsInRole("Supplier"))
            {
                return RedirectToAction("Dashboard", "Supplier", new { id = user.ID });
            }
            else if (user.IsInRole("Custumer"))
            {
                return RedirectToAction("Dashboard", "Custumer", new { id = user.ID });
            }

            return RedirectToAction("Index", "Home", new { id = user.ID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registre(Models.UserRegistreViewModel userRegistreViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            #region Server Side Validation
            bool modelStateFail = false;

            if (string.IsNullOrEmpty(userRegistreViewModel.Name))
            {
                this.ModelState.AddModelError("Name", "Name field is required!");
                modelStateFail = true;
            }

            if (string.IsNullOrEmpty(userRegistreViewModel.Login))
            {
                this.ModelState.AddModelError("Login", "Login field is required!");
                modelStateFail = true;
            }

            if (string.IsNullOrEmpty(userRegistreViewModel.Password))
            {
                this.ModelState.AddModelError("Password", "Password field is required!");
                modelStateFail = true;
            }

            if (string.IsNullOrEmpty(userRegistreViewModel.PasswordConfirm))
            {
                this.ModelState.AddModelError("PasswordConfirm", "Confirm password field is required!");
                modelStateFail = true;
            }

            if (string.IsNullOrEmpty(userRegistreViewModel.Email))
            {
                this.ModelState.AddModelError("Email", "Emailfield is required!");
                modelStateFail = true;
            }

            if (string.IsNullOrEmpty(userRegistreViewModel.EmailConfirm))
            {
                this.ModelState.AddModelError("EmailConfirm", "Confirm email field is required!");
                modelStateFail = true;
            }

            if (!userRegistreViewModel.Email.Equals(userRegistreViewModel.EmailConfirm))
            {
                this.ModelState.AddModelError("EmailConfirm", "Confirm email is different!");
                modelStateFail = true;
            }

            if (!userRegistreViewModel.Password.Equals(userRegistreViewModel.PasswordConfirm))
            {
                this.ModelState.AddModelError("PasswordConfirm", "Confirm password is different!");
                modelStateFail = true;
            }


            if (iServiceUser.AlreadyRegistredEmail(userRegistreViewModel.Email))
            {
                this.ModelState.AddModelError("Email", "Email alread registred!");
                modelStateFail = true;
            }

            if (modelStateFail)
            {
                return View();
            }
            #endregion




            User user = new Model.Users.User()
            {
                Name = userRegistreViewModel.Name,
                Login = userRegistreViewModel.Login,
                Email = userRegistreViewModel.Email,
                Password = userRegistreViewModel.Password,
                UserType = userRegistreViewModel.UserType
            };


            this.iServiceUser.Insert(user);
            this.iUnitOfWorkAsync.SaveChanges();


            IServiceRole serviceRole = DependencyResolver.Current.GetService<ServiceRole>();
            string roleName = "";

            if (user.UserType == Model.Enums.UserType.Supplier)
            {
                roleName = "Supplier";
                IServiceSupplierInfo serviceSupplierInfo = DependencyResolver.Current.GetService<ServiceSupplierInfo>();
                SupplierInfo SupplierInfo = new SupplierInfo();
                SupplierInfo.User = user;
                serviceSupplierInfo.Insert(SupplierInfo);
            }
            else if (user.UserType == Model.Enums.UserType.Custumer)
            {
                roleName = "Custumer";
                IServiceCustumerInfo seviceCustumerInfo = DependencyResolver.Current.GetService<ServiceCustumerInfo>();
                CustumerInfo custumerInfo = new CustumerInfo();
                custumerInfo.User = user;
                seviceCustumerInfo.Insert(custumerInfo);
            }

            serviceRole.AddUserOnRole(roleName, user);

            this.iUnitOfWorkAsync.SaveChanges();


            FormsAuthentication.SetAuthCookie(user.Email, false);

            ModelState.Remove("Password");



            return RedirectToAction("Edit", "User", new { id = user.ID });
            //return View();
        }

        public ActionResult Registre()
        {
            this.ViewBag.SideCollapse = true;
            return View();
        }


        public ActionResult Logoff()
        {
            this.ViewBag.SideCollapse = true;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}