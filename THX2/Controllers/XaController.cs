using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using THX2.Entities;

namespace THX2.Controllers
{
    public class XaController : Controller
    {
        private readonly NewthxContext _context;

        public XaController(NewthxContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Xas.ToList();
            ViewData["Tinh"] = _context.Tinhs.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult GetHuyens(int tinhId)
        {
            var huyens = _context.Huyens.Where(h => h.MaT == tinhId).ToList();
            return Json(huyens);
        }

        [HttpPost]
        public IActionResult GetHuyensByTinhId(int tinhId)
        {
            var huyens = _context.Huyens.Where(h => h.MaT == tinhId).ToList();
            return Json(huyens);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["TinhList"] = _context.Tinhs.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Xa model)
        {
            if (ModelState.IsValid)
            {
                Xa newItem = new Xa()
                {
                    MaT =  model.MaT,
                    MaH = model.MaH,
                    MaX = model.MaX,
                    Ten = model.Ten,
                    Cap = model.Cap
                };
                _context.Xas.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "THX");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _context.Xas.FirstOrDefault(x => x.MaX == id);
            ViewData["TinhList"] = _context.Tinhs.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edited(Xa model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Xas.FirstOrDefault(x => x.MaX == model.MaX);
                data.MaT = model.MaT;
                data.MaH = model.MaH;
                data.MaX = model.MaX;
                data.Ten = model.Ten;
                data.Cap = model.Cap;
                _context.Xas.Update(data);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "THX");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = _context.Xas.FirstOrDefault(x => x.MaX == id);
            if (data != null)
            {
                _context.Xas.Remove(data);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
