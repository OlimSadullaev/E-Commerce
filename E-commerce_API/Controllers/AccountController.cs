﻿using E_commerce_API.DTO;
using E_commerce_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser appuser = new ApplicationUser()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.UserName,
                    PasswordHash = userDTO.Password
                
                };

                //create account in db
                IdentityResult result = await userManager.CreateAsync(appuser, userDTO.Password);
                if(result.Succeeded)
                {
                    return Ok("Account Created");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);

        }

        [HttpPost("login")] //api/account/login
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? userFromDB = await userManager.FindByNameAsync(userDTO.UserName);

                if(userFromDB != null)
                {
                    //found in db
                    //check pass

                    bool found = await userManager.CheckPasswordAsync(userFromDB, userDTO.Password);

                    if(found)
                    {
                        // create token
                        List<Claim> myclaim = new List<Claim>();
                        myclaim.Add(new Claim(ClaimTypes.Name, userFromDB.UserName));
                        myclaim.Add(new Claim(ClaimTypes.NameIdentifier, userFromDB.Id));
                        myclaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jti ==> Token id

                        var roles = await userManager.GetRolesAsync(userFromDB);
                        foreach (var role in roles)
                        {
                            myclaim.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));  //"ssssssssssssssssssssssssssssssssssssssssssssssssssssss"

                        SigningCredentials signingCredentials =
                            new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIss"],
                            audience: config["JWT:ValidAud"],
                            expires: DateTime.Now.AddHours(1),
                            claims: myclaim,
                            signingCredentials: signingCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expired = myToken.ValidTo
                        });
                    }
                }

                return Unauthorized("Invalid Account");
            }

            return BadRequest(ModelState);
        }
    }
}
