namespace User.Api.Controllers.v1
{
	using Common.Models;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="AccountController" />.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		/// <summary>
		/// Defines the _userManager.
		/// </summary>
		private readonly UserManager<IdentityUser> _userManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="userManager">The userManager<see cref="UserManager{IdentityUser}"/>.</param>
		public AccountController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// The Post.
		/// </summary>
		/// <param name="model">The model<see cref="RegisterModel"/>.</param>
		/// <returns>The <see cref="Task{IActionResult}"/>.</returns>
		///       
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RegisterModel model)
		{
			var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };

			var result = await _userManager.CreateAsync(newUser, model.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(x => x.Description);

				return Ok(new RegisterResult { Successful = false, Errors = errors });
			}

			return Ok(new RegisterResult { Successful = true });
		}
	}
}
