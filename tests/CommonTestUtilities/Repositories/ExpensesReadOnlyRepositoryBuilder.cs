using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ExpensesReadOnlyRepositoryBuilder
{
    public static IExpensesReadOnlyRepository Build()
    {
        var mock = new Mock<IExpensesReadOnlyRepository>();

        return mock.Object;
    }
}
