using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.DTO;
using MovieStore.Repositories.Abstract;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserAuthenticationService _authService;

        public HomeController(IMovieService movieService, IUserAuthenticationService authService)
        {
            _movieService = movieService;
            _authService = authService;
        }

        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movieListVm = _movieService.List(term, paging: true, currentPage: currentPage);
            return View(movieListVm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index");
            }

            TempData["msg"] = result.Message;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index");
        }
    }
}
