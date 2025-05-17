using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using TTM.Core.Infrastructure.Persistence.Context;

namespace TTM.Core.Infrastructure.Repositories;

public class MainDbRepository<T>(MainDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        ApplySpecification(specification, false)
            .ProjectToType<TResult>();
}
