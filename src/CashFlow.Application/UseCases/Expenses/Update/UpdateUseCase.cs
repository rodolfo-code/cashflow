using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateUseCase : IUpdateUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUseCase(IExpensesWriteOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public Task<ResponseRegisteredExpenseJson> Execute(int id, RequestRegisterExpenseJson request)
    {
        throw new NotImplementedException();
    }
}
