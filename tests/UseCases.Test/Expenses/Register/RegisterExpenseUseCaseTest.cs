﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Mapper;
using FluentAssertions;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using CommonTestUtilities;
using CommonTestUtilities.Entities;

namespace UseCases.Test.Expenses.Register;
public class RegisterExpenseUseCaseTest
{

    [Fact(DisplayName = nameof(Register_Expense_Successfull))]
    [Trait("UseCases", "Register Expense UseCase")]
    public async Task Register_Expense_Successfull()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact(DisplayName = nameof(Error_Title_Empty))]
    [Trait("UseCases", "Register Expense UseCase")]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Theory(DisplayName = nameof(Error_When_Ammount_Less_Than_1))]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10_000)]
    [Trait("UseCases", "Register Expense UseCase")]
    public async Task Error_When_Ammount_Less_Than_1(decimal amount)
    {

        var loggedUser = UserBuilder.Build();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;


        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    private RegisterExpenseUseCase CreateUseCase(User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user); ;

        var useCase = new RegisterExpenseUseCase(repository, unitOfWork, mapper, loggedUser);

        return useCase;
    }
}
