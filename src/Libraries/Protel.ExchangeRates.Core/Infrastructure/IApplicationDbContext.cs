using Microsoft.EntityFrameworkCore;
using Protel.ExchangeRates.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protel.ExchangeRates.Core.Infrastructure
{
    public interface IApplicationDbContext
    {
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
