using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ForestryAnimals_AtaRK.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LanguageController : Controller
    {
        [HttpGet]
        public IActionResult SetLanguage(string culture)
        {
            try
            {
                Response.Cookies.Append(key: CookieRequestCultureProvider.DefaultCookieName,
                                        value: CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                                        options: new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
