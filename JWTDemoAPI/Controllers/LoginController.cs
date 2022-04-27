using JwtDemo.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public async Task<object> GetJwtStr(string userName, string pwd)
        {

            JwtUserInfo jwtUserInfo = new JwtUserInfo { Uid = 1, Role = "Admin,Leader" };
            string jwtStr = JwtHelper.IssueJwt(jwtUserInfo);

            return Ok(new { success = true, token = jwtStr });
        }
    }
}
