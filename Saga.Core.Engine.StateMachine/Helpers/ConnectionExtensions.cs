using System.Threading;
using System.Threading.Tasks;
using Saga.Core.Engine.StateMachine.Abstractions;
using Saga.Core.Engine.StateMachine.Exceptions;
using Seedwork.Functional.MaybeModel;

namespace Saga.Core.Engine.StateMachine.Helpers
{
    public static class ConnectionExtensions
    {
        public static async Task<bool> TryActivateAsync(this Maybe<IConnection> connection, CancellationToken cancellationToken)
        {
            if (!connection.HasValue) 
                return false;

            await connection.Value.ActivateAsync(cancellationToken);
            return true;
        }

        public static async Task<bool> TryDisableAsync(this Maybe<IConnection> connection, CancellationToken cancellationToken)
        {
            if (!connection.HasValue)
                return false;

            await connection.Value.DisableAsync(cancellationToken);
            return true;
        }

        public static async Task<bool> TryHandleMessageAsync<TMessage>(this Maybe<IConnection> connection, TMessage message, CancellationToken cancellationToken) 
            where TMessage : class
        {
            if (!connection.HasValue)
                return false;

            await connection.Value.ImpartMessageAsync(message, cancellationToken);
            return true;
        }

        public static TData GetDataOrThrow<TData>(this Maybe<IConnection> connection)
        {
            if (!connection.HasValue)
                ProcessManagerException.Throw();

            return connection.Value.GetDataOfType<TData>();
        }

        public static void AddDataOrThrow<TData>(this Maybe<IConnection> connection, TData data)
        {
            if (!connection.HasValue)
                ProcessManagerException.Throw();

            connection.Value.SetData(data);
        }
    }
}