using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Communication.Responses;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Expenses.GetAll;
public class GetAllExpensesUseCaseTest
{
    [Fact(DisplayName = nameof(GetAll_Expenses_Success))]
    [Trait("UseCases", "GetAll Expenses UseCase")]
    public async Task GetAll_Expenses_Success()
    {
        var useCase = CreateUseCase();

        ResponseExpensesJson response = await useCase.Execute();


        response.Should().NotBeNull();
        response.Should().BeAssignableTo<ResponseExpensesJson>();
    }

    private GetAllExpensesUseCase CreateUseCase()
    {
        var repository = ExpensesReadOnlyRepositoryBuilder.Build();

        var mapper = MapperBuilder.Build();

        var useCase = new GetAllExpensesUseCase(repository, mapper);

        return useCase;
    }
}
