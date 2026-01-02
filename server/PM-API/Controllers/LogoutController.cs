using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PM_API.Controllers;

[ApiController]
[Authorize]
[Route("")]
public class AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly UserManager<IdentityUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Signed out successfully" });
    }
    
    [HttpPost("logout/everywhere")]
    public async Task<IActionResult> LogoutEverywhere()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        // Update security stamp to invalidate all existing tokens/cookies
        await _userManager.UpdateSecurityStampAsync(user);
        await _signInManager.SignOutAsync();

        return Ok(new { message = "Signed out from all devices" });
    }
}