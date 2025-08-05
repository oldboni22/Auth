using AutoMapper;
using RepositoryAbstractions;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    public IUserService User { get; }
    
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        var factory = new ServiceFactory(repositoryManager, mapper);

        User = factory.CreateUserService();
    }
}