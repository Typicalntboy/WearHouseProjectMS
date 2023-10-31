using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using WearHouseAPI.Models.Database;
using WearHouseAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WearHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhUserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public WhUserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("doLogin")]
        [Produces("application/json")]
        public async Task<ActionResult<WhUser>> Get([FromBody] WhUser User)
        {
            var userData = await _dbContext.WhUser
                .FirstOrDefaultAsync(x => x.Email == User.Email && x.Password == User.Password);

            if (userData != null)
            {
                return Ok(userData);
            }else
            {
                return BadRequest();
            }
                
        }

        //Fungsi Register

        [HttpPost]
        [Route("AddUser")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateUser([FromBody] WhUser whUser)
        {
            Guid UserId = Guid.NewGuid();

            var NewUser = new WhUser
            {
                UserID = UserId.ToString("D"),
                Name = whUser.Name,
                Email = whUser.Email,
                Password = whUser.Password,
            };

            try
            {
                _dbContext.WhUser.Add(NewUser);
                await _dbContext.SaveChangesAsync();

                //Membuat URL untuk tempat entitas baru yang dapat diakses
                return Ok();
            }
            catch (Exception ex)
            {
                //Membuat pesan peringatan jika operasi penyimpanan gagal
                return BadRequest("Failed create user: " + ex.Message);
            }
        }
    }
}
