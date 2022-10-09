using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Saga.Core.Engine.StateMachine.Exceptions
{
    public sealed class ProcessManagerException : Exception
    {
        private ProcessManagerException()
        {
            throw new NotImplementedException();
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Throw()
        {
            throw new ProcessManagerException();
        }
    }
}