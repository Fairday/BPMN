using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessUnitStorage
    {
        Task StoreAsync(string key, string value, CancellationToken token);
        Task StoreAsync<T>(string key, T value, CancellationToken token);
        Task<string> GetAsync(string key, CancellationToken token);
        Task<T> GetAsync<T>(string key, CancellationToken token);
        Task<bool> ExistsAsync(string key, CancellationToken token);
        Task<Type> GetValueTypeAsync(string key, CancellationToken token);
        Task RemoveAsync(string key, CancellationToken token);
    }
}