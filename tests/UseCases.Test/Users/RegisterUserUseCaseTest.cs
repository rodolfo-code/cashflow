using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users;
public class RegisterUserUseCaseTest
{
    [Fact(DisplayName = nameof(Register_User_Success))]
    [Trait("UseCase", "Register User UseCase")]
    public async Task Register_User_Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateuseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(DisplayName = nameof(Name_Empty_ThrowException))]
    [Trait("UseCase", "Register User UseCase")]
    public async Task Name_Empty_ThrowException()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateuseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact(DisplayName = nameof(CreateUser_WithExistingEmail_ThrowsUserAlreadyExistsException))]
    [Trait("UseCase", "Register User UseCase")]
    public async Task CreateUser_WithExistingEmail_ThrowsUserAlreadyExistsException()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateuseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUserUseCase CreateuseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeRepository = UseWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = new PasswordEncrypterBuilder().Build();
        var JwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOnlyRepositoryBuilder();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistActiveUserEmail(email);
        }

        return new RegisterUserUseCase(mapper, passwordEncripter, readRepository.Build(), writeRepository, JwtTokenGenerator, unitOfWork);
    }
}
