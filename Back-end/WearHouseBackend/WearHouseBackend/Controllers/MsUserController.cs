using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WearHouseBackend.Connections;
using WearHouseBackend.Models.Database;
using WearHouseBackend.Models.Response;

namespace WearHouseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsUserController : Controller
    {
        private readonly AppDbContext _dbContext;
        public MsUserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Login
        [HttpPost]
        [Route("LoginUser")]
        [Produces("application/json")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] MsUser user)
        {
            var LoginUser = await _dbContext.MsUser
                .FirstOrDefaultAsync(x => x.MsEmail == user.MsEmail && x.MsPassword == user.MsPassword);

            if (user != null)
            {
                return Ok(LoginUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AddUser")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateUser([FromBody] MsUser msUser)
        {
            Guid userId = Guid.NewGuid();

            var NewUser = new MsUser
            {
                MsUserID = userId.ToString("D"),
                MsName = msUser.MsName,
                MsEmail = msUser.MsEmail,
                MsPassword = msUser.MsPassword
            };

            try
            {
                _dbContext.MsUser.Add(NewUser);
                await _dbContext.SaveChangesAsync();

                //Membuat url url tempat entitas baru yang dapat diakses
                return Ok();
            }
            catch (Exception ex)
            {
                //Menangani kesalahan jika operasi penyimpanan gagal
                return BadRequest("Failed to create user: " + ex.Message);
            }
        }
    }
}
