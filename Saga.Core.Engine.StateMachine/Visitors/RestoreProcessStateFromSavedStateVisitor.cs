using System;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class RestoreProcessStateFromSavedStateVisitor : IProcessVisitor
    {
        private readonly StateMachineStateDto _stateMachineStateDto;

        public RestoreProcessStateFromSavedStateVisitor(StateMachineStateDto stateMachineStateDto)
        {
            _stateMachineStateDto = stateMachineStateDto ?? throw new ArgumentNullException(nameof(stateMachineStateDto));
        }

        public Task Visit<TProcessManager>(ProcessManager<TProcessManager> processManager) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            if (processManager == null)
                throw new ArgumentNullException(nameof(processManager));

            processManager.IsAborted = _stateMachineStateDto.IsAborted;
            processManager.IsFinished = _stateMachineStateDto.IsFinished;
            processManager.IsStarted = _stateMachineStateDto.IsStarted;

            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IState<TProcessManager> state) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var stateDto = _stateMachineStateDto.States[state.Name];
            if (state is StateMachineState<TProcessManager> stateMachineState)
            {
                stateMachineState.IsDisabled = stateDto.IsDisabled;
                foreach (var resolvedEventPrerequisite in stateDto.ResolvedEventPrerequisites)
                {
                    var prerequisite = stateMachineState.GetLeavePrerequisite(resolvedEventPrerequisite);
                    stateMachineState.ResolvePrerequisite(prerequisite);
                }
            }
            return Task.CompletedTask;
        }

        public Task Visit(IConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            var transitionDto = _stateMachineStateDto.Transitions[connection.Name];
            if (connection is Connection stateMachineTransition)
            {
                stateMachineTransition.IsDisabled = transitionDto.IsDisabled;
                stateMachineTransition.IsProduced = transitionDto.IsProduced;

                if (stateMachineTransition.IsProduced || stateMachineTransition.IsDisabled)
                {
                    var fromState = stateMachineTransition.From;
                    var toState = stateMachineTransition.To;

                    ResolveTransition(fromState, connection);
                    ResolvePrerequisite(toState, connection);
                }
            }

            void ResolveTransition(IState state, IConnection resolvedTransition)
            {
                var stateType = state.GetType();
                var methodInfo = stateType.GetMethod("ResolveTransition", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new ArgumentNullException("stateType.GetMethod(\"ResolveTransition\", BindingFlags.Instance | BindingFlags.NonPublic)");
                methodInfo.Invoke(state, new object[] { resolvedTransition });
            }

            void ResolvePrerequisite(IState state, IConnection resolvedTransition)
            {
                var stateType = state.GetType();
                var methodInfo = stateType.GetMethod("ResolvePrerequisite", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] {typeof(IConnection)}, null) ?? throw new ArgumentNullException("stateType.GetMethod(\"ResolvePrerequisite\", BindingFlags.Instance | BindingFlags.NonPublic)");
                methodInfo.Invoke(state, new object[] { resolvedTransition });
            }

            return Task.CompletedTask;
        }

        public Task Visit(IProcessEvent @event)
        {
            if (@event == null) 
                throw new ArgumentNullException(nameof(@event));

            var eventDto = _stateMachineStateDto.Events[@event.Name];

            if (@event is ProcessEventBase stateMachineEvent)
            {
                stateMachineEvent.IsPropagated = eventDto.IsPropagated;
                stateMachineEvent.IsRaised = eventDto.IsRaised;
            }

            if (@event.GetType().IsGenericType && eventDto.DataAsJson is not null)
            {
                var eventDataType = Type.GetType(eventDto.DataTypeFullNameWithAssembly) ?? throw new ArgumentNullException("Type.GetType(eventDto.DataTypeFullNameWithAssembly)");
                var eventData = JsonConvert.DeserializeObject(eventDto.DataAsJson, eventDataType);

                var stateMachineEventType = typeof(ProcessMessageEvent<>).MakeGenericType(eventDataType);
                var methodInfo = stateMachineEventType.GetMethod(nameof(ProcessMessageEvent<object>.FillMessage), BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new ArgumentNullException("stateMachineEventType.GetMethod(nameof(StateMachineEvent<object>.FillMessage))");
                methodInfo.Invoke(@event, new[] { eventData });
            }

            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IBehavior<TProcessManager> behavior) 
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IActivity<TProcessManager> state)
            where TProcessManager : IProcessManager<TProcessManager>
        {
            return Task.CompletedTask;
        }
    }
}