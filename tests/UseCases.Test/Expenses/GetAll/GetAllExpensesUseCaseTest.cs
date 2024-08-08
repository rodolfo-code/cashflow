using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CommonTestUtilities;
using CommonTestUtilities.Entities;
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
        var loggedUser = UserBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        ResponseExpensesJson response = await useCase.Execute();


        response.Should().NotBeNull();
        response.Should().BeAssignableTo<ResponseExpensesJson>();
    }

    private GetAllExpensesUseCase CreateUseCase(User user)
    {
        var repository = ExpensesReadOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user); ;

        var useCase = new GetAllExpensesUseCase(repository, mapper, loggedUser);

        return useCase;
    }
}
