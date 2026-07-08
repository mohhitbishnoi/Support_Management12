using Application.Dtos.Users;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.Entitis;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Queries;

public class GetUserByIdQuery:IRequest<Result<GetUserDto>>
{
    public int Id { get; set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;

    }
}
internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<GetUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<GetUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
        if (user == null)
        {
            return Result<GetUserDto>.BadRequest("User not found.");
        }

        var result = _mapper.Map<GetUserDto>(user);

        return Result<GetUserDto>.Success(result, "User .....");
    }
}
