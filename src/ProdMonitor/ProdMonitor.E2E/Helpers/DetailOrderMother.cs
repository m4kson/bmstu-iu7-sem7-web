using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Web.Dto.Orders;

namespace ProdMonitor.E2E.Helpers;

public class DetailOrderMother
{
    public static DetailOrderDb DetailOrderInDelivery()
    {
        return new DetailOrderDb(
            id: Guid.Parse("5215c9e2-c214-4335-b8d3-7bc9c22d2541"),
            userId: Guid.Parse("a991df27-a0ff-4a4e-8173-fcf3a40befb0"),
            status: DetailOrderStatusDb.InDelivery,
            totalPrice: 100,
            orderDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc));
    }

    public static DetailOrderDb DetailOrderInWork()
    {
        return new DetailOrderDb(
            id: Guid.NewGuid(),
            userId: Guid.Parse("a991df27-a0ff-4a4e-8173-fcf3a40befb0"),
            status: DetailOrderStatusDb.InWork,
            totalPrice: 150,
            orderDate: DateTime.SpecifyKind(new DateTime(2022, 1, 1, 12, 0, 0), DateTimeKind.Utc));
    }

}