using System;
using AutoMapper;
using Customer.Inquiries.Core;
using Customer.Inquiries.Core.Models;
using Customer.Inquiries.DataAccess.Models;
using NUnit.Framework;

namespace Customer.Inquiries.Tests
{
    [TestFixture]
    public class MappingTest
    {
        private IConfigurationProvider _configuration;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void ShouldMapCustomerToCustomerProfileDto()
        {
            var entity = new DataAccess.Models.Customer();

            var result = _mapper.Map<CustomerProfileDto>(entity);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CustomerProfileDto>(result);
        }

        [Test]
        public void ShouldMapTrasactionToTransactionDto()
        {
            var entity = new Transaction();

            var result = _mapper.Map<TransactionDto>(entity);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<TransactionDto>(result);
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
