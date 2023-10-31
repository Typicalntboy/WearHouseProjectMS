using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WearHouseAPI.Data;
using WearHouseAPI.Models.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WearHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("AddCtg")]
        [Produces("application/json")]
        public async Task<ActionResult<whCategory>> AddCategory(whCategory category)
        {
            var newCtg = new whCategory
            {
                CatId = Guid.NewGuid().ToString("D"),
                UserID = category.UserID,
                Category = category.Category,
                CtgIn = DateTime.Now
            };

            try
            {
                _dbContext.whCategory.Add(newCtg);
                await _dbContext.SaveChangesAsync();
                return Ok(newCtg);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to make Category: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCtg")]
        public async Task<ActionResult<IEnumerable<whCategory>>> GetCtg()
        {
            var DataCtg = await _dbContext.whCategory.OrderBy(x => x.CtgIn).ToListAsync();
            return Ok(DataCtg);
        }

        [HttpPost]
        [Route("UpdateCtg")]
        [Produces("application/json")]
        public async Task<ActionResult>  EditCategory(whCategory category)
        {
            var OldCtg = await _dbContext.whCategory.FirstOrDefaultAsync(x => x.UserID == category.UserID && x.CatId == category.CatId );

            if (OldCtg != null)
            {
                OldCtg.Category = category.Category;
                OldCtg.CtgIn = DateTime.Now;

                try
                {
                    _dbContext.whCategory.Update(OldCtg);
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest("Failed to edit category: " + ex.Message);
                }
            }
            else
            {
                return NotFound("Category not found");
            }
        }

        [HttpPost]
        [Route("DeleteCtg")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteCategory(whCategory category)
        {
            var DeleteCtg = await _dbContext.whCategory.FirstOrDefaultAsync(x => x.UserID == category.UserID && x.CatId == category.CatId);

            if (DeleteCtg != null)
            {
                try
                {   
                    //Digunakan untuk menghapus data pada tabel category
                    _dbContext.whCategory.Remove(DeleteCtg);
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
                return NotFound("Failed to find category");
            }
        }

    }
};
