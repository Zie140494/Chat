using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.HtmlHelpers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
             {
                 new Game { GameId = 1, Name = "Игра1"},
                 new Game { GameId = 2, Name = "Игра2"},
                 new Game { GameId = 3, Name = "Игра3"},
                 new Game { GameId = 4, Name = "Игра4"},
                 new Game { GameId = 5, Name = "Игра5"}
             });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;

            // Утверждение
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Игра4");
            Assert.AreEqual(games[1].Name, "Игра5");
        }
        [TestMethod]
        public void Can_Generate_page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage=2,
                Totalitems=28,
                ItemsPerPage=10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId=1,Name="Игра1"},
                new Game {GameId=2,Name="Игра2"},
                new Game {GameId=3,Name="Игра3"},
                new Game {GameId=4,Name="Игра4"},
                new Game {GameId=5,Name="Игра5"}
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;

            PagingInfo pageInfo = result.GetPagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.Totalitems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_Games()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameId = 1, Name = "Игра1", Category="Cat1"},
        new Game { GameId = 2, Name = "Игра2", Category="Cat2"},
        new Game { GameId = 3, Name = "Игра3", Category="Cat1"},
        new Game { GameId = 4, Name = "Игра4", Category="Cat2"},
        new Game { GameId = 5, Name = "Игра5", Category="Cat3"}
    });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Game> result = ((GamesListViewModel)controller.List("Cat2", 1).Model)
                .Games.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Игра2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Игра4" && result[1].Category == "Cat2");
        }
    }
}

