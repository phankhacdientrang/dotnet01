using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using THX2.Entities;

namespace THX2.Controllers
{
    public class TinhController : Controller
    {
        private readonly NewthxContext _context;

        public TinhController(NewthxContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Tinhs.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tinh model)
        {
            if (ModelState.IsValid)
            {
                Tinh newItem = new Tinh()
                {
                    MaT = model.MaT,
                    Ten = model.Ten,
                    Cap = model.Cap
                };
                _context.Tinhs.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "THX");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var data = _context.Tinhs.FirstOrDefault(x => x.MaT == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edited(Tinh model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Tinhs.FirstOrDefault(x => x.MaT == model.MaT);
                if (data != null)
                {
                    data.Ten = model.Ten;
                    data.Cap = model.Cap;
                    _context.Tinhs.Update(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "THX");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tinh = _context.Tinhs.Include(t => t.Huyens).ThenInclude(h => h.Xas).FirstOrDefault(x => x.MaT == id);
            if (tinh != null)
            {
                // Xóa các xã thuộc huyện
                foreach (var huyen in tinh.Huyens)
                {
                    _context.Xas.RemoveRange(huyen.Xas);
                }

                // Xóa các huyện thuộc tỉnh
                _context.Huyens.RemoveRange(tinh.Huyens);

                // Xóa tỉnh
                _context.Tinhs.Remove(tinh);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
