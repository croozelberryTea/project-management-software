using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM_API.Infrastructure.Model;

namespace PM_API.Controllers;

[ApiController]
[Authorize]
[Route("logout/")]
public class LogoutController(SignInManager<User> signInManager, UserManager<User> userManager) : ControllerBase
{
    private readonly SignInManager<User> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

    [HttpPost("")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Signed out successfully" });
    }
    
    [HttpPost("everywhere")]
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