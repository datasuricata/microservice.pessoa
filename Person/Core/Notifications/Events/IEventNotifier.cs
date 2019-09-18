using FluentValidation;
using System;
using System.Collections.Generic;

namespace Person.Core.Notifications.Events {
    public interface IEventNotifier : IDisposable {

        void Add<N>(string message);
        void When<N>(bool hasError, string message);
        void AddException<N>(string message, Exception exception = null);
        void Validate<T>(T model, AbstractValidator<T> validator);
        bool IsValid { get; }
        IEnumerable<Notification> GetNotifications();

    }
}
