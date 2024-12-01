using Moq;
using ProdMonitor.Application.Services;
using ProdMonitor.Domain.Exceptions;
using ProdMonitor.Domain.Interfaces.Repositories;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using Xunit;

namespace ProdMonitor.Tests.UnitTests
{
    public class AssemblyLineServiceTests
    {
        private readonly Mock<IAssemblyLineRepository> _mockAssemblyLineRepository;
        private readonly AssemblyLineService _assemblyLineService;

        public AssemblyLineServiceTests()
        {
            _mockAssemblyLineRepository = new Mock<IAssemblyLineRepository>();
            _assemblyLineService = new AssemblyLineService(_mockAssemblyLineRepository.Object);
        }

        [Fact]
        public async Task CreateAssemblyLineAsync_Success_ReturnsAssemblyLine()
        {
            // Arrange
            var assemblyLineCreate = new AssemblyLineCreate(name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusType.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            var expectedAssemblyLine = new AssemblyLine(Guid.NewGuid(), 
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusType.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            _mockAssemblyLineRepository
                .Setup(repo => repo.CreateAssemblyLineAsync(assemblyLineCreate))
                .ReturnsAsync(expectedAssemblyLine);

            // Act
            var result = await _assemblyLineService.CreateAssemblyLineAsync(assemblyLineCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssemblyLine.Id, result.Id);
            _mockAssemblyLineRepository.Verify(repo => repo.CreateAssemblyLineAsync(assemblyLineCreate), Times.Once);
        }

        [Fact]
        public async Task CreateAssemblyLineAsync_Failure_ThrowsAssemblyLineServiceException()
        {
            // Arrange
            var assemblyLineCreate = new AssemblyLineCreate(name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusType.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            _mockAssemblyLineRepository
                .Setup(repo => repo.CreateAssemblyLineAsync(assemblyLineCreate))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AssemblyLineServiceException>(() => _assemblyLineService.CreateAssemblyLineAsync(assemblyLineCreate));
            Assert.Equal("Failed to create line", exception.Message);
            _mockAssemblyLineRepository.Verify(repo => repo.CreateAssemblyLineAsync(assemblyLineCreate), Times.Once);
        }

        [Fact]
        public async Task GetAssemblyLineByIdAsync_Success_ReturnsAssemblyLineWithTractorsAndDetails()
        {
            // Arrange
            var lineId = Guid.NewGuid();
            var expectedTractors = new List<Tractor> { 
                new Tractor(id: Guid.NewGuid(),
                                model: "Tractor1",
                                releaseYear: 2023,
                                engineType: "Diesel",
                                enginePower: "250 HP",
                                frontTireSize: 24,
                                backTireSize: 28,
                                wheelsAmount: 4,
                                tankCapacity: 200,
                                ecologicalStandart: "Euro 6",
                                length: 5.5f,
                                width: 2.5f,
                                cabinHeight: 3.2f), 
                new Tractor(id: Guid.NewGuid(),
                                model: "Tractor1",
                                releaseYear: 2023,
                                engineType: "Diesel",
                                enginePower: "250 HP",
                                frontTireSize: 24,
                                backTireSize: 28,
                                wheelsAmount: 4,
                                tankCapacity: 200,
                                ecologicalStandart: "Euro 6",
                                length: 5.5f,
                                width: 2.5f,
                                cabinHeight: 3.2f) 
            };

            var expectedDetails = new List<Detail> { new Detail(Guid.NewGuid(), "Detail1", "USA", 10, 200, 100, 50, 30) };

            var expectedAssemblyLine = new AssemblyLine(
                id: lineId,
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusType.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2,
                tractors: expectedTractors,
                details: expectedDetails
            );

            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ReturnsAsync(expectedAssemblyLine);

            // Act
            var result = await _assemblyLineService.GetAssemblyLineByIdAsync(lineId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAssemblyLine.Id, result.Id);
            Assert.Equal(expectedAssemblyLine.Tractors.Count, result.Tractors.Count);
            Assert.Equal(expectedAssemblyLine.Details.Count, result.Details.Count);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
        }

        [Fact]
        public async Task GetAllAssemblyLinesAsync_Success_ReturnsListOfAssemblyLines()
        {
            // Arrange
            var filter = new AssemblyLineFilter();
            var expectedLines = new List<AssemblyLine>
            {
                new AssemblyLine(Guid.NewGuid(),
                    name: "Line1",
                    length: 100,
                    height: 50,
                    width: 30,
                    status: LineStatusType.Working,
                    production: 1000,
                    downTime: 10,
                    inspectionsPerYear: 2,
                    lastInspection: new DateOnly(2023, 8, 1),
                    nextInspection: new DateOnly(2024, 8, 1),
                    defectRate: 2),

                new AssemblyLine(Guid.NewGuid(),
                    name: "Line2",
                    length: 100,
                    height: 50,
                    width: 30,
                    status: LineStatusType.Working,
                    production: 1000,
                    downTime: 10,
                    inspectionsPerYear: 2,
                    lastInspection: new DateOnly(2023, 8, 1),
                    nextInspection: new DateOnly(2024, 8, 1),
                    defectRate: 2)
            };

            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAllAssemblyLinesAsync(filter))
                .ReturnsAsync(expectedLines);

            // Act
            var result = await _assemblyLineService.GetAllAssemblyLinesAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLines.Count, result.Count);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAllAssemblyLinesAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAllAssemblyLinesAsync_Failure_ThrowsAssemblyLineServiceException()
        {
            // Arrange
            var filter = new AssemblyLineFilter();
            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAllAssemblyLinesAsync(filter))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AssemblyLineServiceException>(() => _assemblyLineService.GetAllAssemblyLinesAsync(filter));
            Assert.Equal("Failed to get lines", exception.Message);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAllAssemblyLinesAsync(filter), Times.Once);
        }

        [Fact]
        public async Task GetAssemblyLineByIdAsync_Success_ReturnsAssemblyLine()
        {
            // Arrange
            var lineId = Guid.NewGuid();
            var expectedLine = new AssemblyLine(lineId, 
                name: "Line1",
                length: 100,
                height: 50,
                width: 30,
                status: LineStatusType.Working,
                production: 1000,
                downTime: 10,
                inspectionsPerYear: 2,
                lastInspection: new DateOnly(2023, 8, 1),
                nextInspection: new DateOnly(2024, 8, 1),
                defectRate: 2);

            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ReturnsAsync(expectedLine);

            // Act
            var result = await _assemblyLineService.GetAssemblyLineByIdAsync(lineId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedLine.Id, result.Id);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
        }

        [Fact]
        public async Task GetAssemblyLineByIdAsync_LineNotFound_ThrowsLineNotFoundException()
        {
            // Arrange
            var lineId = Guid.NewGuid();

            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ReturnsAsync((AssemblyLine)null); // Возвращаем null, чтобы имитировать отсутствие линии

            // Act & Assert
            var exception = await Assert.ThrowsAsync<LineNotFoundException>(() => _assemblyLineService.GetAssemblyLineByIdAsync(lineId));
            Assert.Equal($"Line with id {lineId} not found", exception.Message);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
        }

        [Fact]
        public async Task GetAssemblyLineByIdAsync_Failure_ThrowsAssemblyLineServiceException()
        {
            // Arrange
            var lineId = Guid.NewGuid();
            _mockAssemblyLineRepository
                .Setup(repo => repo.GetAssemblyLineByIdAsync(lineId))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AssemblyLineServiceException>(() => _assemblyLineService.GetAssemblyLineByIdAsync(lineId));
            Assert.Equal("Failed to get line", exception.Message);
            _mockAssemblyLineRepository.Verify(repo => repo.GetAssemblyLineByIdAsync(lineId), Times.Once);
        }
    }
}
