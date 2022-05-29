using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UC001_Beispielprojekt_Firmenpflege.Models;

namespace UC001_Beispielprojekt_Firmenpflege.Controllers
{
    public class CompanyModelsController : Controller
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: CompanyModels
        public async Task<ActionResult> Index()
        {
            return View(await db.Company_db.ToListAsync());
        }

        // GET: CompanyModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = await db.Company_db.FindAsync(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // GET: CompanyModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyModels/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyNo,CompanyName,Industry,AmountEmployees,City,Holding,Level")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                db.Company_db.Add(companyModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyModel);
        }

        // GET: CompanyModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = await db.Company_db.FindAsync(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // POST: CompanyModels/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyNo,CompanyName,Industry,AmountEmployees,City,Holding,Level")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyModel);
        }

        // GET: CompanyModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyModel companyModel = await db.Company_db.FindAsync(id);
            if (companyModel == null)
            {
                return HttpNotFound();
            }
            return View(companyModel);
        }

        // POST: CompanyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyModel companyModel = await db.Company_db.FindAsync(id);
            db.Company_db.Remove(companyModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
