using ProdMonitor.Domain.Models;

namespace ProdMonitor.Test.UnitTests.RepositoryTest.Helpers;

public class DetailMother
{
    public static DetailCreate Default()
    {
        return new DetailCreate(
            "Detail1", 
            "Russia", 
            50,
            30,
            10,
            2,
            2);
    }
    
    public static DetailCreate Broken()
    {
        return new DetailCreate(
            null, 
            "Russia", 
            50,
            30,
            10,
            2,
            2);
    }
}