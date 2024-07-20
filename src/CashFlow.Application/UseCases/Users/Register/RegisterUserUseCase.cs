using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUsersWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _useReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    public RegisterUserUseCase(
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository useReadOnlyRepository,
        IUsersWriteOnlyRepository userWriteOnlyRepository, 
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _useReadOnlyRepository = useReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredUserJson>(user);
    }

    private async Task validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _useReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if(emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
