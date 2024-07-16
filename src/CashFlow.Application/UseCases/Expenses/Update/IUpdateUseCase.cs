using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateUseCase
{
    Task Execute(int id, RequestExpenseJson request);
}
