﻿using MextFullstackSaaS.Domain;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace MextFullstackSaaS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }

        DbSet<UserBalance> UserBalances { get; set; }

        DbSet<UserBalanceHistory> UserBalanceHistories { get; set; }
        DbSet<UserPayment> UserPayments { get; set; }
        DbSet<UserPaymentHistory> UserPaymentHistories { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Role> Roles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}
