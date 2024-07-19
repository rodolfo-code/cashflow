using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class UsersRepository : IUsersWriteOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public UsersRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}
