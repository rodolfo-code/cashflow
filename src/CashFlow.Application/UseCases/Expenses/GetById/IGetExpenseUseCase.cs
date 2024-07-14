using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseUseCase
{
    Task<ResponseExpenseJson> Execute(int id);
}
