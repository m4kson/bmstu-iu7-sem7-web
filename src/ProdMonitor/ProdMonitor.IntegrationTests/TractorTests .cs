using Xunit;
using ProdMonitor.Domain.Models;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.DataAccess.Models.Enums;
using System.Collections.Generic;
using ProdMonitor.DataAccess.Models.Converters;

namespace ProdMonitor.IntegrationTests
{
    public class TractorTests : IClassFixture<ServiceTestSetup>
    {
        private readonly ServiceTestSetup _setup;

        public TractorTests(ServiceTestSetup setup)
        {
            _setup = setup;
        }

        [Fact]
        public async Task Should_Create_Tractor_Through_Service_Successfully()
        {
            // Arrange
            var tractorCreate = new TractorCreate(
                model: "TestModel",
                releaseYear: 2023,
                engineType: "Diesel",
                enginePower: "150HP",
                frontTireSize: 20,
                backTireSize: 25,
                wheelsAmount: 4,
                tankCapacity: 100,
                ecologicalStandart: "Euro5",
                length: 3.5f,
                width: 2.5f,
                cabinHeight: 3.0f,
                assemblyLines: null);

            // Act
            var createdTractor = await _setup.TractorService.CreateTractorAsync(tractorCreate);

            // Assert
            createdTractor.Should().NotBeNull();
            createdTractor.Model.Should().Be("TestModel");
        }

        [Fact]
        public async Task GetAllTractorsAsync_ShouldReturnAllTractors()
        {
            // Arrange
            var tractor1 = new TractorDb
            {
                Id = Guid.NewGuid(),
                Model = "TestModel1",
                ReleaseYear = 2022,
                EngineType = "Diesel",
                EnginePower = "150HP",
                FrontTireSize = 20,
                BackTireSize = 25,
                WheelsAmount = 4,
                TankCapacity = 100,
                EcologicalStandard = "Euro5",
                Length = 3.5f,
                Width = 2.5f,
                CabinHeight = 3.0f
            };

            var tractor2 = new TractorDb
            {
                Id = Guid.NewGuid(),
                Model = "TestModel2",
                ReleaseYear = 2023,
                EngineType = "Electric",
                EnginePower = "200HP",
                FrontTireSize = 22,
                BackTireSize = 28,
                WheelsAmount = 4,
                TankCapacity = 80,
                EcologicalStandard = "Euro6",
                Length = 3.8f,
                Width = 2.8f,
                CabinHeight = 3.5f
            };

            _setup.DbContext.Tractors.AddRange(tractor1, tractor2);
            await _setup.DbContext.SaveChangesAsync();

            var filter = new TractorFilter();

            // Act
            var result = await _setup.TractorService.GetAllTractorsAsync(filter);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetTractorByIdAsync_ShouldReturnTractor_WhenTractorExists()
        {
            // Arrange
            var tractor = new TractorDb
            {
                Id = Guid.NewGuid(),
                Model = "TestModel2",
                ReleaseYear = 2023,
                EngineType = "Diesel",
                EnginePower = "150HP",
                FrontTireSize = 20,
                BackTireSize = 25,
                WheelsAmount = 4,
                TankCapacity = 100,
                EcologicalStandard = "Euro5",
                Length = 3.5f,
                Width = 2.5f,
                CabinHeight = 3.0f
            };

            _setup.DbContext.Tractors.Add(tractor);
            await _setup.DbContext.SaveChangesAsync();

            // Act
            var result = await _setup.TractorService.GetTractorByIdAsync(tractor.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tractor.Id, result.Id);
            Assert.Equal("TestModel2", result.Model);
        }

    }
}
