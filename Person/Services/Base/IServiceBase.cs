using System.Threading.Tasks;

namespace Person.Services.Base {
    public interface IServiceBase {
        Task Commit();
        Task CommitForce();
    }
}
