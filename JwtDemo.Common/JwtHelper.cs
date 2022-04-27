using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.Common
{
    public class JwtHelper
    {

        public static string IssueJwt(JwtUserInfo jwtUserInfo)
        {

 
            string iss = DemoAppsettings.GetVal(new string[] { "Audience", "Issuer" });
            string aud = DemoAppsettings.GetVal(new string[] { "Audience", "Audience" });
            string secret = DemoAppsettings.GetVal(new string[] { "Audience", "Secret" }); 


            var claimsIdentity = new List<Claim>
                {
                 new Claim(JwtRegisteredClaimNames.Jti, jwtUserInfo.Uid.ToString()), 
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,iss), 
                new Claim(JwtRegisteredClaimNames.Aud,aud),
                new Claim(JwtRegisteredClaimNames.Email,jwtUserInfo.Email!)
               };

            //添加用户的角色信息（非必须，可添加多个）
            var claimRoleList = jwtUserInfo.Role!.Split(',').Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claimsIdentity.AddRange(claimRoleList);


            #region 【Step3-签名对象】

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)); //创建密钥对象
            var sigCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //创建密钥签名对象

            #endregion

            #region 【Step5-将JWT相关信息封装成对象】
            var jwt = new JwtSecurityToken(
              issuer: iss,
              claims: claimsIdentity,
              signingCredentials: sigCreds);
            #endregion

            #region 【Step6-将JWT信息对象生成字符串形式】
            var jwtHandler = new JwtSecurityTokenHandler();
            string token = jwtHandler.WriteToken(jwt);
            #endregion

            return token;
        } // END IssueJwt()


        public static JwtUserInfo SerializeJwtStr(string jwtStr)
        {
            JwtUserInfo jwtUserInfo = new JwtUserInfo();
            var jwtHandler = new JwtSecurityTokenHandler();

            if (!string.IsNullOrEmpty(jwtStr) && jwtHandler.CanReadToken(jwtStr))
            {
   
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

  
                jwtUserInfo.Uid = Convert.ToInt64(jwtToken.Id);
                object role;
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtUserInfo.Role = role == null ? "" : role.ToString();
            }

            return jwtUserInfo;
        } //END SerializeJwt()



    }
}