using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlatform.Domain.Domain;
using GamePlatform.Repository;
using GamePlatform.Service.Interface;
using System.Security.Claims;

namespace GamePlatform.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IDeveloperService _devService;
        private readonly IShoppingCartService _shoppingCartService;

        public GamesController(IGameService gameService, IDeveloperService devService, IShoppingCartService shoppingCartService)
        {
            _gameService = gameService;
            _devService = devService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Games
        public IActionResult Index()
        {
            return View(_gameService.GetAllGames());
        }

        // GET: Games/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }

            ViewData["DeveloperId"] = new SelectList(_devService.GetAllDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_devService.GetAllDevelopers(), "Id", "DevName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GameTitle,GameDesc,DateReleased,GameImage,Rating,Price,DeveloperId,Id")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.Id = Guid.NewGuid();
                _gameService.CreateGame(game);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_devService.GetAllDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // GET: Games/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_devService.GetAllDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("GameTitle,GameDesc,DateReleased,GameImage,Rating,Price,DeveloperId,Id")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _gameService.UpdateGame(game);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_devService.GetAllDevelopers(), "Id", "DevName", game.DeveloperId);
            return View(game);
        }

        // GET: Games/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _gameService.DeleteGame(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToCart(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGame(id);

            GameInCart gameInCart = new GameInCart();

            if(game != null)
            {
                gameInCart.GameId = game.Id;
            }

            return View(gameInCart);
        }

        public IActionResult AddToCartConfirmed(GameInCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.AddToShoppingCartConfirmed(model, userId);

            return Redirect(nameof(Index));
        }
    }
}
