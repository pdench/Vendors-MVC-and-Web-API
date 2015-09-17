using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VendorTest2.Models;
using VendorsTestUI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PagedList;

namespace VendorsTestUI.Controllers
{
    public class VendorsController : Controller
    {

        protected string serviceUrlBase = "http://localhost:8200/";
        WebClient webClient = new WebClient();

        // GET: Vendors
        public ActionResult Index(string sortOrder, string currentSort, string sortDir, int? page, string mode)
        {

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            string updateMode = String.IsNullOrEmpty(mode) ? "SORT" : mode;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "VendorCode" : sortOrder;
            sortDir = String.IsNullOrEmpty(sortDir) ? "A" : sortDir;
            ViewBag.currentSort = sortOrder;
 
            if (sortOrder == currentSort && updateMode.ToUpper()=="SORT")
            {
                switch (sortDir)
                {
                    case "A":
                        sortDir = "D";
                        break;
                    case "D":
                        sortDir = "A";
                        break;
                    default:
                        sortDir = "A";
                        break;
                }
            }
            else if (updateMode.ToUpper()=="SORT") { sortDir = "A"; }
            ViewBag.sortDir = sortDir;

            IPagedList<Vendor> vendorsPaged = null;
            IEnumerable<Vendor> vendList = null;

            HttpClient client = GetNewHttpClient();

            // List all Vendors.  
            HttpResponseMessage response = client.GetAsync("api/vendors").Result;    
            if (response.IsSuccessStatusCode)
            {
                var vendors = response.Content.ReadAsStringAsync().Result;
                vendList = JsonConvert.DeserializeObject<IEnumerable<Vendor>>(vendors);
            }

            switch (sortOrder)
            {
                case "VendorCode":
                    if (sortDir == "A") { vendorsPaged = vendList.OrderBy(v => v.VendorCode).ToPagedList(pageIndex,pageSize); }
                    else { vendorsPaged = vendList.OrderByDescending(v => v.VendorCode).ToPagedList(pageIndex, pageSize); }
                    break;
                case "VendorName":
                    if (sortDir == "A") { vendorsPaged = vendList.OrderBy(v => v.VendorName).ToPagedList(pageIndex, pageSize); }
                    else { vendorsPaged = vendList.OrderByDescending(v => v.VendorName).ToPagedList(pageIndex, pageSize); }
                    break;
                case "ValidFrom":
                    if (sortDir == "A") { vendorsPaged = vendList.OrderBy(v => v.ValidFrom).ToPagedList(pageIndex, pageSize); }
                    else { vendorsPaged = vendList.OrderByDescending(v => v.ValidFrom).ToPagedList(pageIndex, pageSize); }
                    break;
                case "ValidThru":
                    if (sortDir == "A") { vendorsPaged = vendList.OrderBy(v => v.ValidThru).ToPagedList(pageIndex, pageSize); }
                    else { vendorsPaged = vendList.OrderByDescending(v => v.ValidThru).ToPagedList(pageIndex, pageSize); }
                    break;
                default:
                    vendorsPaged = vendList.OrderBy(v => v.VendorCode).ToPagedList(pageIndex, pageSize); 
                    break;
            }

            return View("Index", vendorsPaged);
        }

        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = GetVendor(id);

            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: Vendors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VendorCode,VendorName,ValidFrom,ValidThru")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                //db.Vendors.Add(vendor);
                //db.SaveChanges();
                HttpClient client = GetNewHttpClient();
                string thisUrl = "api/vendors/";
                var response = client.PostAsJsonAsync(thisUrl, vendor).Result;
                
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = GetVendor(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VendorCode,VendorName,ValidFrom,ValidThru")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(vendor).State = EntityState.Modified;
                //db.SaveChanges();
                HttpClient client = GetNewHttpClient();
                string thisUrl = "api/vendors/" + vendor.Id.ToString();
                var response = client.PostAsJsonAsync(thisUrl, vendor).Result;
                
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = GetVendor(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpClient client = GetNewHttpClient();
            string thisUrl = "api/vendors/" +id.ToString();
            var response = client.DeleteAsync(thisUrl).Result;
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                webClient.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Creates a new Http Client object and sets basic properties used by all methods
        /// </summary>
        /// <returns></returns>
        private HttpClient GetNewHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serviceUrlBase);
            // Add an Accept header for JSON format.  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private Vendor GetVendor(int? Id)
        {

            HttpClient client = GetNewHttpClient();
            string thisUrl = "api/vendors/" + Id.ToString();
            HttpResponseMessage response = client.GetAsync(thisUrl).Result;
            Vendor vendor = null;

            if (response.IsSuccessStatusCode)
            {
                var thisVendor = response.Content.ReadAsStringAsync().Result;
                vendor = JsonConvert.DeserializeObject<Vendor>(thisVendor);
            }

            return vendor;
            
        }
    }
}
