using AutoMapper;
using Jwt.Abstractions;
using RepositoryAbstractions;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    public IUserService User { get; }
    
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IJwtManager jwtManager)
    {
        var factory = new ServiceFactory(repositoryManager, mapper, jwtManager);

        User = factory.CreateUserService();
    }
}