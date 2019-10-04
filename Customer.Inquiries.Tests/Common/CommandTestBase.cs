using System;
using Customer.Inquiries.DataAccess.Base;

namespace Customer.Inquiries.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        public CommandTestBase()
        {
            _context = ApplicationContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(_context);
        }
    }
}
