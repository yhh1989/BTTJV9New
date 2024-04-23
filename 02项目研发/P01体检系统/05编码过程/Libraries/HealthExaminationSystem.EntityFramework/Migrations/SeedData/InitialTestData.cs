using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Test;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData
{
    public class InitialTestData
    {

        private readonly MyProjectDbContext _context;

        public static List<TestTable1> InitialModules { get; private set; }
        public List<TestTable2> InitialModules2 { get; private set; }

        public InitialTestData(MyProjectDbContext context)
        {
            _context = context;
        }

        static InitialTestData()
        {
            InitialModules = new List<TestTable1>();
            for (int i = 0; i < 10000; i++)
            {
                InitialModules.Add(new TestTable1
                {
                    Id = Guid.NewGuid(),
                    Column1 = i.ToString(),
                    Column2 = "2",
                    Column3 = "3",
                    Column4 = "4",
                    Column5 = "5",
                    Column6 = "6",
                    CreationTime = DateTime.Now,
                    CreatorUserId = 0
                });
            }
        }

        public void Create()
        {
            foreach (var module in InitialModules)
            {
                CreateEditions(module);
            }
        }

        private void CreateEditions(TestTable1 module)
        {
            if (_context.TestTable1s.Any(l => l.Column1 == module.Column1))
            {
                InitialModules2 = new List<TestTable2>();
                for (int i = 0; i < 100; i++)
                {
                    InitialModules2.Add(new TestTable2
                    {
                        Id = Guid.NewGuid(),
                        Column1 = i.ToString(),
                        Column2 = "2",
                        Column3 = "3",
                        Column4 = "4",
                        Column5 = "5",
                        Column6 = "6",
                        CreationTime = DateTime.Now,
                        CreatorUserId = 0,
                        TestTable1 = _context.TestTable1s.First(l => l.Column1 == module.Column1)
                    });
                }
                _context.SaveChanges();
            }
        }
    }
}