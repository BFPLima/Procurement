using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Procurement.WebAPP.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public abstract class ControllerBaseAdmin: Controller
    {
        protected IUnitOfWorkAsync unitOfWorkAsync;

        public ControllerBaseAdmin(IUnitOfWorkAsync unitOfWorkAsync)
        {
            this.unitOfWorkAsync = unitOfWorkAsync;
        }

    }
}