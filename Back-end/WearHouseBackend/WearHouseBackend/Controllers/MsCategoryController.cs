using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WearHouseBackend.Connections;
using WearHouseBackend.Models.Database;
using WearHouseBackend.Models.Response;

namespace WearHouseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsCategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        public MsCategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetCategory")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<MsCategory>>> GetCategory(string UserId)
        {
            try
            {
                var DataCategory = await _dbContext.MsCategory
                    .Where(x => x.MsUserID == UserId)
                    .OrderBy(x => x.CtgIn)
                    .ToListAsync();

                if (DataCategory != null && DataCategory.Any())
                {
                    return Ok(DataCategory);
                }
                else
                {
                    return NotFound(); // Mengembalikan HTTP 404 Not Found jika data tidak ditemukan.
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex); // Mengembalikan HTTP 400 Bad Request dengan pesan kesalahan jika terjadi kesalahan.
            }
        }

        //Menambah Category
        [HttpPost]
        [Route("AddCategory")]
        [Produces("application/json")]
        public async Task<ActionResult<MsCategory>> AddCategory(MsCategory msCategory)
        {
            var NewCat = new MsCategory
            {
                MsCtgID = Guid.NewGuid().ToString("D"),
                MsCtg = msCategory.MsCtg,
                CtgIn = DateTime.Now,
                MsUserID = msCategory.MsUserID
            };

            try
            {
                _dbContext.MsCategory.Add(NewCat);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to add new category: " + ex.Message);
            }
        }

        //Membuat fitur edit kategoried
        [HttpPost]
        [Route("EditCategory")]
        [Produces("application/json")]
        public async Task<ActionResult> EditCategory(MsCategory msCategory)
        {
            var DataCategory = await _dbContext.MsCategory.FirstOrDefaultAsync(x => x.MsUserID == msCategory.MsUserID && x.MsCtgID == msCategory.MsCtgID);

            DataCategory.MsCtg = msCategory.MsCtg;

            _dbContext.MsCategory.Update(DataCategory);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("DeleteCategory")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteCategory(MsCategory msCategory)
        {
            var DeleteCat = await _dbContext.MsCategory.FirstOrDefaultAsync( x => x.MsUserID == msCategory.MsUserID && x.MsCtgID == msCategory.MsCtgID);

            if (DeleteCat != null)
            {
                try
                {
                    _dbContext.MsCategory.Remove(DeleteCat);
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest("Failed to remove category: " + ex.Message);
                }
            }
            else
            {
                return NotFound("Category not found!");
            }
        }
    }
}
