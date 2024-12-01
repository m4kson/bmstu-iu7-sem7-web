using ProdMonitor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdMonitor.DataAccess.Models.Converters
{
    public static class TractorConverter
    {
        public static Tractor? ToDomain(TractorDb? tractorDb)
        {
            if (tractorDb == null)
            {
                return null;
            }

            return new Tractor(id: tractorDb.Id,
                model: tractorDb.Model,
                releaseYear: tractorDb.ReleaseYear,
                engineType: tractorDb.EngineType,
                enginePower: tractorDb.EnginePower,
                frontTireSize: tractorDb.FrontTireSize,
                backTireSize: tractorDb.BackTireSize,
                wheelsAmount: tractorDb.WheelsAmount,
                tankCapacity: tractorDb.TankCapacity,
                ecologicalStandart: tractorDb.EcologicalStandard,
                length: tractorDb.Length,
                width: tractorDb.Width,
                cabinHeight: tractorDb.CabinHeight,
                assemblyLines: tractorDb.AssemblyLines?.Select(AssemblyLineConverter.ToDomain).ToList() ?? new List<AssemblyLine>());
        }

        public static TractorDb? ToDb(Tractor? tractorDomain)
        {
            if (tractorDomain == null)
            {
                return null;
            }

            return new TractorDb(
                id: tractorDomain.Id,
                model: tractorDomain.Model,
                releaseYear: tractorDomain.ReleaseYear,
                engineType: tractorDomain.EngineType,
                enginePower: tractorDomain.EnginePower,
                frontTireSize: tractorDomain.FrontTireSize,
                backTireSize: tractorDomain.BackTireSize,
                wheelsAmount: tractorDomain.WheelsAmount,
                tankCapacity: tractorDomain.TankCapacity,
                ecologicalStandard: tractorDomain.EcologicalStandart,
                length: tractorDomain.Length,
                width: tractorDomain.Width,
                cabinHeight: tractorDomain.CabinHeight
            );
        }
    }
}
