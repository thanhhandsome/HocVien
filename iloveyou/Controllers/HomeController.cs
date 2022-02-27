using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iloveyou.Controllers
{
    public class HomeController : Controller
    {
        private Models.ModelHocvien dc = new Models.ModelHocvien();
        // GET: Home
        public ActionResult Index()
        {
            return View(dc.lyliches);
        }
        public ActionResult xemChiTietHocvien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);
            return View(hv);
        }
        public ActionResult FormThemHocvien()
        {
            ViewBag.DSLop = dc.lops.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ThemHocvien(Models.lylich hv)
        {
            if (ModelState.IsValid)
            {
                var kq = dc.lyliches.Where(x => x.mshv == hv.mshv).ToList();
                if (kq.Count > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    dc.lyliches.Add(hv);
                    dc.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
        public ActionResult FormSuaHocvien(string id)
        {
            ViewBag.DSLop = dc.lops.ToList();
            Models.lylich hv = dc.lyliches.Find(id);
            return View(hv);
        }
        public ActionResult SuaHocvien(Models.lylich hv)
        {
            if (ModelState.IsValid)
            {
                Models.lylich x = dc.lyliches.Find(hv.mshv);
                x.tenhv = hv.tenhv;
                x.ngaysinh = hv.ngaysinh;
                x.phai = hv.phai;
                x.malop = hv.malop;

                dc.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult FormXoaHocvien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);
            ViewBag.Hocvien = hv;
            ViewBag.CoXoa = true;
            foreach (var a in dc.diemthis.Where(x => x.mshv == id))
            {
                ViewBag.CoXoa = false;
                break;
            }
            return View();
        }
        public ActionResult XoaHocvien(string id)
        {
            Models.lylich hv = dc.lyliches.Find(id);
            dc.lyliches.Remove(hv);
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}