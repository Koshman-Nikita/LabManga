using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LabManga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBLibraryContext _contect;

        public ChartsController(DBLibraryContext context)
        {
            _contect = context;
        }

        [HttpGet("categoriesJsonData")]
        public JsonResult JsonData()
        {
            var categories = _contect.Categories.Include(b => b.Mangas).ToList();
            List<object> catManga = new List<object>();
            catManga.Add(new[] { "Категория", "Количество манг" });
            foreach (var c in categories)
            {
                catManga.Add(new object[] { c.Name, c.Mangas.Count() });
            }
            return new JsonResult(catManga);
        }
        [HttpGet("AthoursJsonData")]
        public JsonResult JsonData1()
        {
            var authors = _contect.Authors.Include(a => a.AuthorsMangas).ToList();
            List<object> catManga = new List<object>();
            catManga.Add(new[] { "Автор", "Количество манг" });
            foreach (var a in authors)
            {
                catManga.Add(new object[] { a.Name, a.AuthorsMangas.Count() });
            }
            return new JsonResult(catManga);
        }

    }
}
