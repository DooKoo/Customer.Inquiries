using System;
using AutoMapper;
using Customer.Inquiries.Core;
using Customer.Inquiries.DataAccess.Base;
using Customer.Inquiries.DataAccess.Interfaces;
using NUnit.Framework;

namespace Customer.Inquiries.Tests.Common
{
    [TestFixture]
    public class BaseCommandTest
    {
        private ApplicationDbContext _context;
        public IRepository Repository { get; private set; }
        public IMapper Mapper { get; private set; }

        [SetUp]
        public void SetUp()
        {
            _context = ApplicationContextFactory.Create();
            Repository = new Repository(_context);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            ApplicationContextFactory.Destroy(_context);
        }
    }
}
