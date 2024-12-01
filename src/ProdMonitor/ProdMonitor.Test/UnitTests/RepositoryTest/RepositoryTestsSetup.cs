using System;
using Microsoft.EntityFrameworkCore;
using ProdMonitor.DataAccess.Context;
using ProdMonitor.DataAccess.Repositories;
using ProdMonitor.Domain.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using ProdMonitor.Domain.Interfaces.Repositories;

namespace ProdMonitor.Test.UnitTests.RepositoryTest;

public class RepositoryTestsSetup : IDisposable
{
    public ProdMonitorContext Context { get; private set; }
    public AssemblyLineRepository AssemblyLineRepository { get; private set; }
    public TractorRepository TractorRepository { get; private set; }
    public DetailRepository DetailRepository { get; private set; }
    public DetailOrderRepository DetailOrderRepository { get; private set; }
    public OrderDetailRepository OrderDetailRepository { get; private set; }
    public ServiceReportRepository ServiceReportRepository { get; private set; }
    public ServiceRequestRepository ServiceRequestRepository { get; private set; }
    public UserRepository UserRepository { get; private set; }

    public RepositoryTestsSetup()
    {
        InitializeContext();
    }

    private void InitializeContext()
    {
        var options = new DbContextOptionsBuilder<ProdMonitorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        Context = new ProdMonitorContext(options);
        AssemblyLineRepository = new AssemblyLineRepository(Context);
        TractorRepository = new TractorRepository(Context);
        DetailRepository = new DetailRepository(Context);
        DetailOrderRepository = new DetailOrderRepository(Context);
        OrderDetailRepository = new OrderDetailRepository(Context);
        ServiceReportRepository = new ServiceReportRepository(Context);
        ServiceRequestRepository = new ServiceRequestRepository(Context);
        UserRepository = new UserRepository(Context);
    }

    public void ResetContext()
    {
        Dispose();
        InitializeContext();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}