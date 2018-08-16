using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FootballData.Database;
using FootballData.Domain;
using FootballData.Repositories;
using FootballData.Services.General;

namespace FootballData.Services.Test
{
    [TestClass]
    public class LeagueTest
    {
        private Mock<ModelContainer> _mockContext;
        private Mock<DbSet<League>> _mockLeagueSet;

        private LeagueService _leagueService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockContext = new Mock<ModelContainer>();
            _mockLeagueSet = new Mock<DbSet<League>>();
        }

        [TestMethod]
        public void CreateNewLeague()
        {
            // Create new League
            var league = new League
            {
                Id = 1,
                Caption = "European Championships France 2016",
                Code = "EC",
                Year = "2016",
                FootballDataLeagueId = 424,
                Teams = new List<Team>
                {
                    new Team
                    {
                        Name = "France",
                        Code = "FRA"
                    },
                    new Team
                    {
                        Name = "Argentina",
                        Code = "ARG"
                    }
                }
            };

            try
            {
                // Setup Context
                _mockContext.Setup(m => m.Set<League>()).Returns(_mockLeagueSet.Object);

                _leagueService = new LeagueService(new GenericRepository<League>(_mockContext.Object));

                // Insert League
                _leagueService.Insert(league);

                // Check if the League was added
                _mockLeagueSet.Verify(m => m.Add(It.IsAny<League>()), Times.Once());
                _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CheckIfLeagueAlreadyExists()
        {
            var europeanLeague = new League
            {
                Id = 1,
                Caption = "European Championships France 2016",
                Code = "EC",
                Year = "2016",
                FootballDataLeagueId = 424
            };

            var leagueOne = new League
            {
                Id = 2,
                Caption = "League One 2016/17",
                Code = "EL1",
                Year = "2016",
                FootballDataLeagueId = 428
            };

            // Create new league
            var leagues = new List<League>
            {
                europeanLeague,
                leagueOne
            }.AsQueryable();

            try
            {
                // Setup League DbSet
                _mockLeagueSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(id => leagues.FirstOrDefault(d => d.Id == (long)id[0]));

                // Setup Context
                _mockContext.Setup(m => m.Set<League>()).Returns(_mockLeagueSet.Object);

                _leagueService = new LeagueService(new GenericRepository<League>(_mockContext.Object));

                // Get league from mock
                var mockLeague = _leagueService.Get(leagueOne.Id);

                // Check that league already exists
                Assert.IsNotNull(mockLeague);
                Assert.AreEqual(leagueOne.Id, mockLeague.Id);
                Assert.AreEqual(leagueOne.Caption, mockLeague.Caption);
                Assert.AreEqual(leagueOne.Code, mockLeague.Code);
                Assert.AreEqual(leagueOne.Year, mockLeague.Year);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
