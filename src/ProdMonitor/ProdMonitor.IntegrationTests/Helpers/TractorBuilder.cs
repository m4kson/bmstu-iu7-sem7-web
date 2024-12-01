using ProdMonitor.DataAccess.Models;

namespace ProdMonitor.IntegrationTests.Helpers;

public class TractorBuilder
{
    private Guid _id = Guid.Parse("39675975-2a1e-4554-9aff-de8cb30b6c80");
    private string _name = "Tractor1";
    private int _releaseYear = 2021;
    private string _engineType = "Diesel";
    private string _enginePower = "100";
    private int _frontTireSize = 20;
    private int _backTireSize = 20;
    private int _wheelsAmount = 4;
    private int _tankCapacity = 100;
    private string _ecologicalStandart = "Euro 5";
    private float _length = 5;
    private float _width = 2;
    private float _cabinHeight = 2;
    
    public TractorBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public TractorBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public TractorBuilder WithReleaseYear(int releaseYear)
    {
        _releaseYear = releaseYear;
        return this;
    }
    
    public TractorBuilder WithEngineType(string engineType)
    {
        _engineType = engineType;
        return this;
    }  
    
    public TractorBuilder WithEnginePower(string enginePower)
    {
        _enginePower = enginePower;
        return this;
    }
    
    public TractorBuilder WithFrontTireSize(int frontTireSize)
    {
        _frontTireSize = frontTireSize;
        return this;
    }
    
    public TractorBuilder WithBackTireSize(int backTireSize)
    {
        _backTireSize = backTireSize;
        return this;
    }
    
    public TractorBuilder WithWheelsAmount(int wheelsAmount)
    {
        _wheelsAmount = wheelsAmount;
        return this;
    }
    
    public TractorBuilder WithTankCapacity(int tankCapacity)
    {
        _tankCapacity = tankCapacity;
        return this;
    }
    
    public TractorBuilder WithEcologicalStandart(string ecologicalStandart)
    {
        _ecologicalStandart = ecologicalStandart;
        return this;
    }
    
    public TractorBuilder WithLength(float length)
    {
        _length = length;
        return this;
    }
    
    public TractorBuilder WithWidth(float width)
    {
        _width = width;
        return this;
    }
    
    public TractorBuilder WithCabinHeight(float cabinHeight)
    {
        _cabinHeight = cabinHeight;
        return this;
    }
    
    public TractorDb Build()
    {
        return new TractorDb(_id, 
            _name,
            _releaseYear,
            _engineType,
            _enginePower,
            _frontTireSize,
            _backTireSize,
            _wheelsAmount,
            _tankCapacity, 
            _ecologicalStandart,
            _length,
            _width, 
            _cabinHeight);
    }
}