using System.Runtime.CompilerServices;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Helpers
{
    public static class ValidationFlowSequenceBuilder
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow MustBe(this IFlowElement flowElement)
        {
            return new ProcessUnitValidationFlow(flowElement);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow Active(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow NotActive(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow Completed(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow NotCompleted(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow Disabled(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProcessUnitValidationFlow NotDisabled(this ProcessUnitValidationFlow validationFlow)
        {
            var processUnit = validationFlow.FlowElement;
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFlowElement ThenUnit(this ProcessUnitValidationFlow validationFlow, IFlowElement nextValidatingUnit)
        {
            return nextValidatingUnit;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityValidationFlow AlsoActivity(this ProcessUnitValidationFlow validationFlow, IActivity activity)
        {
            return new ActivityValidationFlow(activity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityValidationFlow MustBe(this ActivityValidationFlow validationFlow)
        {
            return validationFlow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ActivityValidationFlow NotRunning(this ActivityValidationFlow validationFlow)
        {
            var activity = validationFlow.Activity;
            return validationFlow;
        }
    }
}