using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Procurement.Model.Users;
using Procurement.Repository.EF;
using Procurement.Repository.Interface.Users;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Procurement.Service.Interface.Users;

namespace Procurement.WebAPP.Controllers
{


    public class UserController : Controller
    {
        //protected IRepositoryUser iRepositoryUser;
        protected IUnitOfWorkAsync iUnitOfWorkAsync;
        protected IServiceUser iServiceUser;

        public UserController(IUnitOfWorkAsync iUnitOfWorkAsync,
                              IServiceUser iServiceUser)
        {
            this.iUnitOfWorkAsync = iUnitOfWorkAsync;
            this.iServiceUser = iServiceUser;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            //return View(await db.Custumers.ToListAsync());
            return View();
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await this.iServiceUser.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await this.iServiceUser.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,LastName,Login,Password,Email,UserType,LegalPersonality,CreatedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ObjectState = ObjectState.Modified;
                this.iServiceUser.Update(user);
                await this.iUnitOfWorkAsync.SaveChangesAsync();
                return RedirectToAction("Details", new { id = user.ID });
            }
            return View(user);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.iUnitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
