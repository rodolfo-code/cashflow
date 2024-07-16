//using CashFlow.Communication.Enums;
using CashFlow.Application.UseCases;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;


namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact(DisplayName = nameof(Success))]
    [Trait("Validators", "Register Expense Validator")]
    public void Success()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Error_When_Title_Empty))]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    [Trait("Validators", "Register Expense Validator")]
    public void Error_When_Title_Empty(string title)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact(DisplayName = nameof(Error_When_Date_In_Future))]
    [Trait("Validators", "Register Expense Validator")]
    public void Error_When_Date_In_Future()
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        request.Date = DateTime.UtcNow.AddDays(1);

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.ContainSingle(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_BE_THE_FUTURE));
    }

  
    [Fact(DisplayName = nameof(Error_When_PaymentType_Does_Not_Exist))]
    [Trait("Validators", "Register Expense Validator")]
    public void Error_When_PaymentType_Does_Not_Exist()
    { 
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        request.PaymentType = (PaymentType)20;

        // Act
        var result = validator.Validate(request);

    // Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle()
            .And.ContainSingle(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }
[InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10_000)]
    [Trait("Validators", "Register Expense Validator")]
    public void Error_When_Amount_Is_Zero_Or_Negative(decimal amount)
    {
        // Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;
        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .And.ContainSingle(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
