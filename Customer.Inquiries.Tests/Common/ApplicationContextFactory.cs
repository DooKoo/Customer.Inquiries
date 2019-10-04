using System;
using Microsoft.EntityFrameworkCore;
using Customer.Inquiries.DataAccess.Base;
using Customer.Inquiries.DataAccess.Models;

namespace Customer.Inquiries.Tests.Common
{
    public class ApplicationContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            context.Customers.AddRange(new[] {
                new DataAccess.Models.Customer
                {
                    CustomerId = 213233,
                    ContactEmail = "customer1@test.com",
                    CustomerName = "Test1 Test1",
                    MobileNumber = 12343123,
                    CreatedDate = DateTimeOffset.Now
                },

                new DataAccess.Models.Customer
                {
                    CustomerId = 213234,
                    ContactEmail = "customer2@test.com",
                    CustomerName = "Test2 Test2",
                    MobileNumber = 12343124,
                    CreatedDate = DateTimeOffset.Now
                },

                new DataAccess.Models.Customer
                {
                    CustomerId = 213235,
                    ContactEmail = "customer2@test.com",
                    CustomerName = "Test2 Test2",
                    MobileNumber = 12343125,
                    CreatedDate = DateTimeOffset.Now
                },
            });

            context.Transactions.AddRange(new[] {
                new Transaction
                {
                    TransactionId = 9876782,
                    CustomerId = 213233,
                    Amount = 100.45M,
                    Currency = "USD",
                    Status = ETransactionStatus.Success,
                    TransactionDateTime = DateTimeOffset.Now,
                    CreatedDate = DateTimeOffset.Now,
                },
                new Transaction
                {
                    TransactionId = 9876781,
                    CustomerId = 213233,
                    Amount = 10.25M,
                    Currency = "EUR",
                    Status = ETransactionStatus.Failed,
                    TransactionDateTime = DateTimeOffset.Now,
                    CreatedDate = DateTimeOffset.Now,
                },
                new Transaction
                {
                    TransactionId = 9876783,
                    CustomerId = 213234,
                    Amount = 0.0M,
                    Currency = "USD",
                    Status = ETransactionStatus.Canceled,
                    TransactionDateTime = DateTimeOffset.Now,
                    CreatedDate = DateTimeOffset.Now,
                }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
