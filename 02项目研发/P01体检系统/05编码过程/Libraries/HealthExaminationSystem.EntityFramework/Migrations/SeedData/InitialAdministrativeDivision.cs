using System;
using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData
{
    public class InitialAdministrativeDivision
    {
        private readonly MyProjectDbContext _context;

        public static List<AdministrativeDivision> InitialAdministrativeDivisions { get; }

        public InitialAdministrativeDivision(MyProjectDbContext context)
        {
            _context = context;
        }

        static InitialAdministrativeDivision()
        {
            var administrativeDivisionStrings = Properties.Resources.AdministrativeDivision.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            InitialAdministrativeDivisions = new List<AdministrativeDivision>();
            for (var i = 1; i < administrativeDivisionStrings.Length; i++)
            {
                var administrativeDivisionString = administrativeDivisionStrings[i].Split('|');
                var administrativeDivision = new AdministrativeDivision
                {
                    Id = Guid.NewGuid(),
                    Code = administrativeDivisionString[0],
                    Name = administrativeDivisionString[1]
                };
                InitialAdministrativeDivisions.Add(administrativeDivision);
            }
        }

        public void Create()
        {
            var alls = _context.AdministrativeDivisions.ToList();
            var deletes = alls.Except(InitialAdministrativeDivisions);
            foreach (var administrativeDivision in deletes)
            {
                DeleteEditions(administrativeDivision);
            }
            var creates = InitialAdministrativeDivisions.Except(alls);
            foreach (var module in creates)
            {
                CreateEditions(module);
            }

            _context.SaveChanges();
            GC.Collect();
        }

        private void DeleteEditions(AdministrativeDivision administrativeDivision)
        {
            if (_context.AdministrativeDivisions.Any(r => r.Code == administrativeDivision.Code))
            {
                var entity = _context.AdministrativeDivisions.First(r => r.Code == administrativeDivision.Code);
                _context.AdministrativeDivisions.Remove(entity);
            }
        }

        private void CreateEditions(AdministrativeDivision module)
        {
            if (_context.AdministrativeDivisions.Any(r => r.Code == module.Code))
            {
                return;
            }

            _context.AdministrativeDivisions.Add(module);
        }
    }
}