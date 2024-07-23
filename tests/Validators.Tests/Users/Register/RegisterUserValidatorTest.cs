using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact(DisplayName = nameof(Register_Success))]
    [Trait("Validators", "Register User Validator")]
    public void Register_Success()
    {
        // Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Error_Name_Empty))]
    [Trait("Validators", "Register User Validator")]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Name_Empty(string name)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.NAME_EMPTY);
    }

    [Theory(DisplayName = nameof(Error_Email_Empty))]
    [Trait("Validators", "Register User Validator")]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.EMAIL_EMPTY);
    }

    [Fact(DisplayName = nameof(Error_Email_Invalid))]
    [Trait("Validators", "Register User Validator")]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "rodolfo.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.EMAIL_INVALID);
    }

    [Fact(DisplayName = nameof(Error_Email_Invalid))]
    [Trait("Validators", "Register User Validator")]
    public void Error_Password_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceErrorMessages.INVALID_PASSWORD);
    }
}
