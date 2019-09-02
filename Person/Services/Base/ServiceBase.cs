using Person.Core.Notifications.Events;
using Person.Infra.Transactions;
using System;
using System.Threading.Tasks;

namespace Person.Services.Base {
    public class ServiceBase : IServiceBase {

        #region - attributes -

        private readonly IUnitOfWork _uow;
        protected readonly IEventNotifier _notify;

        #endregion

        #region - ctor -

        public ServiceBase(IServiceProvider provider) {
            _uow = (IUnitOfWork)provider.GetService(typeof(IUnitOfWork));
            _notify = (IEventNotifier)provider.GetService(typeof(IEventNotifier));
        }

        #endregion

        #region - persistence -

        public async Task Commit() {
            if (!_notify.IsValid)
                return;

            await _uow.Commit();
        }

        public async Task CommitForce() => await _uow.Commit();

        #endregion
    }
}
