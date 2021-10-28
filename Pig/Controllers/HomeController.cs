using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pig.Models;
using Microsoft.AspNetCore.Http;


namespace Pig.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public ViewResult Index()
        {
            MySession session = new MySession(HttpContext.Session);

            Player player1 = session.GetPlayer("Player1");
            Player player2 = session.GetPlayer("Player2");

            if (player1 == null && player2 == null)
            {
                player1 = new Player(true);
                player2 = new Player(false);

                session.SetPlayer(player1, "Player1");
                session.SetPlayer(player2, "Player2");
            }
            else
            {
                Random rand = new Random();

                int roll = rand.Next(1, 7);

                if (player1.turn == true)
                {
                    if (roll == 1)
                    {
                        player1.turn = false;
                        player2.turn = true;


                        player1.score = player1.score - player1.turnScore;
                        player1.turnScore = 0;

                    }
                    else
                    {
                        player1.score = player1.score + roll;
                        player1.turnScore = roll;
                    }


                    session.SetPlayer(player1, "Player1");
                    session.SetPlayer(player2, "Player2");

                    ViewBag.Roll = roll;
                    ViewBag.Turn = "1";


                }
                else
                {
                    if (roll == 1)
                    {
                        player2.turn = false;
                        player1.turn = true;


                        player2.score = player2.score - player2.turnScore;
                        player2.turnScore = 0;

                    }
                    else
                    {
                        player2.score = player2.score + roll;
                        player2.turnScore = roll;
                    }

                    session.SetPlayer(player2, "Player2");
                    session.SetPlayer(player1, "Player1");

                    ViewBag.Roll = roll;
                    ViewBag.Turn = "2";

                }
            }


            ViewBag.Player1Score = player1.score;
            ViewBag.Player2Score = player2.score;



            if (player1.score >=20 || player2.score >= 20)
            {

                session.RemovePlayer("Player1");
                session.RemovePlayer("Player2");

                if(player1.score >= 20)
                {
                    TempData["Winner"] = 1;
                }
                else
                {
                    TempData["Winner"] = 2;
                }


                ViewBag.Player1Score = 0;
                ViewBag.Player2Score = 0;
            }

            return View();
        }



        public RedirectToActionResult Delete()
        {

            MySession session = new MySession(HttpContext.Session);

            session.RemovePlayer("Player1");
            session.RemovePlayer("Player2");

            return RedirectToAction("Index");
        }

        public ViewResult Hold()
        {

            MySession session = new MySession(HttpContext.Session);

            Player player1 = session.GetPlayer("Player1");
            Player player2 = session.GetPlayer("Player2");

            if (player1.turn == true)
            {
                player1.turn = false;
                player1.turnScore = 0;
                player2.turn = true;

                ViewBag.Turn = "2";
            }
            else
            {
                player2.turn = false;
                player2.turnScore = 0;
                player1.turn = true;

                ViewBag.Turn = "1";
            }

            ViewBag.Player1Score = player1.score;
            ViewBag.Player2Score = player2.score;


            session.SetPlayer(player1, "Player1");
            session.SetPlayer(player2, "Player2");

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
