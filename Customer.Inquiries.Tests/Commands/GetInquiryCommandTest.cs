using System.Threading;
using System.Threading.Tasks;
using Customer.Inquiries.Core.Commands;
using Customer.Inquiries.Core.Models;
using Customer.Inquiries.DataAccess.Base;
using Customer.Inquiries.Tests.Common;
using NUnit.Framework;

namespace Customer.Inquiries.Tests.Commands
{
    public class GetInquiryCommandTest : BaseCommandTest
    {
        [Test]
        public async Task EmptyTest()
        {
            var sut = new GetInquiryCommandHandler(Repository, Mapper);

            var result = await sut.Handle(new GetInquiryCommand { }, CancellationToken.None);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByEmailTest()
        {
            var sut = new GetInquiryCommandHandler(Repository, Mapper);

            var result = await sut.Handle(new GetInquiryCommand { Email = "customer2@test.com" }, CancellationToken.None);

            Assert.IsInstanceOf<CustomerProfileDto>(result);
            Assert.AreEqual("customer2@test.com", result.Email);
        }

        [Test]
        public async Task GetByIdTest()
        {
            var sut = new GetInquiryCommandHandler(Repository, Mapper);

            var result = await sut.Handle(new GetInquiryCommand { CustomerId = 213233 }, CancellationToken.None);

            Assert.IsInstanceOf<CustomerProfileDto>(result);
            Assert.AreEqual(213233, result.CustomerId);
        }
    }
}