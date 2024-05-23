using CQRS.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}