using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseUseCase : IGetExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IMapper _mapper;
    public GetExpenseUseCase(IExpensesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(int id)
    {
        var response = await _repository.GetById(id);

        var mapped = _mapper.Map<ResponseExpenseJson>(response);

        return mapped;
    }
}
