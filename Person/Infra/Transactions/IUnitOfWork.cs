using System.Threading.Tasks;

namespace Person.Infra.Transactions {
    public interface IUnitOfWork {
        Task Commit();
    }
}
