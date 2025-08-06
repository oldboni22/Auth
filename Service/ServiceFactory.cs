using AutoMapper;
using Jwt.Abstractions;
using RepositoryAbstractions;
using Service.Contracts;

namespace Service;

public class ServiceFactory(IRepositoryManager repositoryManager, IMapper mapper, IJwtManager jwtManager)
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    private readonly IJwtManager _jwtManager = jwtManager;

    public IUserService CreateUserService() => new UserService(_repositoryManager, _mapper, _jwtManager);
}