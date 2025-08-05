using AutoMapper;
using RepositoryAbstractions;
using Service.Contracts;

namespace Service;

public class ServiceFactory(IRepositoryManager repositoryManager, IMapper mapper)
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public IUserService CreateUserService() => new UserService(_repositoryManager,_mapper);
}