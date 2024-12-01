using ProdMonitor.DataAccess.Models;
using ProdMonitor.DataAccess.Models.Enums;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;

namespace ProdMonitor.E2E.Helpers;

public class AssemblyLineBuilder
{
    private Guid _id = Guid.Parse("ec539f4e-0811-40bd-b077-8b9e604f0345");
    private string _name = "Line1";
    private int _length = 100;
    private int _height = 50;
    private int _width = 30;
    private LineStatusTypeDb _status = LineStatusTypeDb.Working;
    private int _downTime = 10;
    private int _inspectionsPerYear = 2;
    private DateOnly _lastInspection = new DateOnly(2023, 8, 1);
    private DateOnly _nextInspection = new DateOnly(2024, 8, 1);
    private int _defectRate = 2;
    
    public AssemblyLineBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public AssemblyLineBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public AssemblyLineBuilder WithLength(int length)
    {
        _length = length;
        return this;
    }
    
    public AssemblyLineBuilder WithHeight(int height)
    {
        _height = height;
        return this;
    }
    
    public AssemblyLineBuilder WithWidth(int width)
    {
        _width = width;
        return this;
    }
    
    public AssemblyLineBuilder WithStatus(LineStatusTypeDb status)
    {
        _status = status;
        return this;
    }
    
    public AssemblyLineBuilder WithDownTime(int downTime)
    {
        _downTime = downTime;
        return this;
    }
    
    public AssemblyLineBuilder WithInspectionsPerYear(int inspectionsPerYear)
    {
        _inspectionsPerYear = inspectionsPerYear;
        return this;
    }
    
    public AssemblyLineBuilder WithLastInspection(DateOnly lastInspection)
    {
        _lastInspection = lastInspection;
        return this;
    }
    
    public AssemblyLineBuilder WithNextInspection(DateOnly nextInspection)
    {
        _nextInspection = nextInspection;
        return this;
    }
    
    public AssemblyLineBuilder WithDefectRate(int defectRate)
    {
        _defectRate = defectRate;
        return this;
    }
    
    public AssemblyLineDb Build()
    {
        return new AssemblyLineDb(
            _id,
            _name,
            _length,
            _height,
            _width,
            _status,
            _downTime,
            _inspectionsPerYear,
            _lastInspection,
            _nextInspection,
            _defectRate);
    }
}