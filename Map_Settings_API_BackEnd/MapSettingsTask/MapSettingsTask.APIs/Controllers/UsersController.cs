using MapSettingsTask.APIs.Data;
using MapSettingsTask.APIs.Data.Model;
using MapSettingsTask.APIs.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MapSettingsTask.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<MapCreator> _userManager;

    public UsersController(IConfiguration configuration,
        UserManager<MapCreator> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    #region Login

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
    {
        MapCreator? user = await _userManager.FindByNameAsync(credentials.UserName);
        if (user is null)
        {
            return NotFound();
        }


        bool isAuthenticated = await _userManager.CheckPasswordAsync(user, credentials.Password);
        if (!isAuthenticated)
        {
            return Unauthorized();
        }

        var claimsList = await _userManager.GetClaimsAsync(user);

        //Getting the key ready
        string keyString = _configuration.GetValue<string>("SecretKey")!;
        byte[] keyInBytes = Encoding.ASCII.GetBytes(keyString);
        var key = new SymmetricSecurityKey(keyInBytes);

        //Combine Secret Key with Hashing Algorithm
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //Putting All together
        var expiry = DateTime.Now.AddMinutes(15);
        var jwt = new JwtSecurityToken(
            expires: expiry,
            claims: claimsList,
            signingCredentials: signingCredentials);

        //Getting Token String
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(jwt);

        return new TokenDto
        {
            Token = tokenString,
            Expiry = expiry
        };
    }

    #endregion

    #region Register

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register(RegisterDto registrDto)
    {
        var newCreator = new MapCreator
        {
            UserName = registrDto.UserName,
            Department = registrDto.Department,
            Email = registrDto.Email,
        };

        var result = await _userManager.CreateAsync(newCreator, registrDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, newCreator.Id),
            new Claim(ClaimTypes.Role, "Student")
        };

        await _userManager.AddClaimsAsync(newCreator, claims);

        return NoContent();
    }

    #endregion
}