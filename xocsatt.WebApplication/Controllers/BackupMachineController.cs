using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xocsatt.WebApplication.Models;
using XOcsatt.Entities;

namespace xocsatt.WebApplication.Controllers
{
    public class BackupMachineController : Controller
    {
        //
        // GET: /BackupMachine/
        public ActionResult Index()
        {
            var backupMachine = BackupMachine.GetAll();

            return View(backupMachine);
        }
	}
}