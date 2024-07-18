using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Exception.ExceptionBase;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUsersWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterUserUseCase(IUsersWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        validate(request);

        var user = _mapper.Map<User>(request);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };

        //await _repository.Add(entity);

        //await _unitOfWork.Commit();

        //return _mapper.Map<ResponseRegisteredUserJson>(entity);
    }

    private void validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
