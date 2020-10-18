using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ModernArchitectureShop.Store.Domain;

namespace ModernArchitectureShop.Store.Application.Persistence
{
    public interface IStoreRepository
    {
        void CreateDatabase();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        ValueTask<Domain.Store?> GetAsync(Guid id, CancellationToken cancellationToken);

        ValueTask AddAsync(Domain.Store store, CancellationToken cancellationToken);

        ValueTask RemoveAsync(Guid id, CancellationToken cancellationToken);

        void Update(Domain.Store store);

        ValueTask<int> CountAsync(CancellationToken cancellationToken);

        IQueryable<Domain.Store> FindStoresQuery(int pageIndex, int pageSize);
    }
}
