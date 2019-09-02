using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Person.Core.Notifications.Events {
    public sealed class EventNotifier : IEventNotifier {
        #region - parameters -

        private readonly Notifier _notifier;
        private bool _disposed;

        #endregion

        #region - ctor -

        public EventNotifier() => _notifier = new Notifier();

        #endregion

        #region - methods -

        // use for fast validations
        public void When<N>(bool hasError, string message) {
            if (hasError)
                _notifier.Notifications
                    .Add(new Notification {
                        Key = typeof(N).Name,
                        Value = message,
                        StatusCode = 400
                    });
        }

        public void Add<N>(string message) => _notifier.Notifications.Add(new Notification { Key = typeof(N).Name, Value = message, StatusCode = 400 });

        public void AddException<N>(string message, Exception exception = null) {
            var stack = new StackTrace(exception);
            var frames = stack.GetFrames();

            var trace = new string[frames.Count()];

            var i = 0;

            var ex = exception?.InnerException?.InnerException == null
                ? exception?.Message
                : exception.InnerException.InnerException.Message;

            var lines = ex != null && ex.Contains(Environment.NewLine)
                ? ex.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                : new[] { ex };

            foreach (var x in frames)
                trace[i++] = $"{i}. ↓ {x.GetMethod().Name}";

            _notifier.Notifications.Add(new Notification {
                Key = typeof(N).Name,
                Value = message,
                Exception = lines,
                StatusCode = 500,
                Call = trace
            });
        }

        public bool IsValid => !_notifier.HasAny;

        public void Validate<T>(T model, AbstractValidator<T> validator) {
            if (model == null) {
                Add<EventNotifier>("Não encontrado.");
                return;
            }
            validator.Validate(model).Errors?.Where(f => f != null)
                .ToList().ForEach(v => {
                    Add<EventNotifier>(v.ErrorMessage);
                });
        }

        public IEnumerable<Notification> GetNotifications() => _notifier.Notifications.AsEnumerable();

        #endregion

        #region - dispose -

        private void Dispose(bool disposing) {
            if (!_disposed)
                if (disposing)
                    _notifier.Notifications.Clear();

            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
