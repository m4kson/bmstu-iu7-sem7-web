using ProdMonitor.DataAccess.Models;
using ProdMonitor.Domain.Models;

namespace ProdMonitor.E2E.Helpers;

public class DetailBuilder
{
    private Guid _id = Guid.Parse("0b145e17-dccc-42a8-80e5-3e2906ef08a3");
    private string _name = "Detail1";
    private string _country = "Russia";
    private int _amount = 50;
    private float _price = 30;
    private int _length = 50;
    private int _height = 30;
    private int _width = 10;
    
    public DetailBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public DetailBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public DetailBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }
    
    public DetailBuilder WithAmount(int amount)
    {
        _amount = amount;
        return this;
    }
    
    public DetailBuilder WithPrice(float price)
    {
        _price = price;
        return this;
    }
    
    public DetailBuilder WithLength(int length)
    {
        _length = length;
        return this;
    }
    
    public DetailBuilder WithHeight(int height)
    {
        _height = height;
        return this;
    }
    
    public DetailBuilder WithWidth(int width)
    {
        _width = width;
        return this;
    }
    
    public DetailDb Build()
    {
        return new DetailDb(_id, 
            _name,
            _country, 
            _amount, 
            _price, 
            _length,
            _height,
            _width);
    }
}