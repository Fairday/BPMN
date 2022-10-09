using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class GatherProcessStateVisitor : IProcessVisitor
    {
        public GatherProcessStateVisitor()
        {
            GatheredState = new StateMachineStateDto();
        }

        public StateMachineStateDto GatheredState { get; }

        public Task Visit<TProcessManager>(ProcessManager<TProcessManager> processManager)
            where TProcessManager : IProcessManager<TProcessManager>
        {
            if (processManager == null)
                throw new ArgumentNullException(nameof(processManager));

            GatheredState.IsAborted = processManager.IsAborted;
            GatheredState.IsFinished = processManager.IsFinished;
            GatheredState.IsStarted = processManager.IsStarted;

            return Task.CompletedTask;
        }

        public Task Visit<TProcessManager>(IState<TProcessManager> state)
            where TProcessManager : IProcessManager<TProcessManager>
        {
            if (state == null) 
                throw new ArgumentNullException(nameof(state));

            var stateDto = new StateDto
            {
                IsActive = state.IsActive,
                IsInitial = state.IsInitial,
                IsFinal = state.IsFinal,
                Name = state.Name,
                IsDisabled = state.IsDisabled
            };

            GatheredState.States[stateDto.Name] = stateDto;
            foreach (var leavePrerequisite in state.LeavePrerequisites)
            {
                if (state.IsPrerequisiteResolved(leavePrerequisite))
                    stateDto.ResolvedEventPrerequisites.Add(leavePrerequisite.Name);
            }

            return Task.CompletedTask;
        }

        public Task Visit(IConnection connection)
        {
            if (connection == null) 
                throw new ArgumentNullException(nameof(connection));

            var transitionDto = new TransitionDto
            {
                From = connection.From.Name,
                To = connection.To.Name,
                IsProduced = connection.IsProduced,
                Name = connection.Name,
                IsDisabled = connection.IsDisabled,
            };

            GatheredState.Transitions[transitionDto.Name] = transitionDto;

            return Task.CompletedTask;
        }

        public Task Visit(IProcessEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var eventDto = new EventDto
            {
                Name = @event.Name,
                IsPropagated = @event.IsPropagated,
                IsRaised = @event.IsRaised
            };

            if (@event.GetType().IsGenericType)
            {
                var genericArgumentType = @event.GetType().GetGenericArguments()[0];
                eventDto.DataTypeName = genericArgumentType.Name;
                eventDto.DataTypeFullName = genericArgumentType.FullName;
                eventDto.DataTypeFullNameWithAssembly = genericArgumentType.AssemblyQualifiedName;
                var withMessage = (IProcessEvent)@event;
                var message = withMessage.Message;
                if (message != null)
                    eventDto.DataAsJson = JsonConvert.SerializeObject(message);
            }

            GatheredState.Events[eventDto.Name] = eventDto;

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