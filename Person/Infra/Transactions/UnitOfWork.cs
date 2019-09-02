using System;
using System.Threading.Tasks;

namespace Person.Infra.Transactions {
    public class UnitOfWork : IUnitOfWork, IDisposable {
        private readonly AppDbContext db;
        private bool disposed = false;

        public UnitOfWork(AppDbContext db) {
            this.db = db;
        }

        public async Task Commit() {
            await db.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        /// <summary>
        /// to clear GBC
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
