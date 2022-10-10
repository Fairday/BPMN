C# implementation of BPMN (https://camunda.com/bpmn/reference/#overview)

Work status: in progress

Possible DSL example of defining BPMN directly from C# code.

```C#
namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public class NestedProcess : Process<OutputE>
    {
        public NestedProcess(string name) : base(name)
        {
            After(OutputEHandler)
                .Then(Completed);
        }

        public IActionTaskWithInput<OutputE> OutputEHandler { get; }
    }

    public class NestedProcessWithResult : ProcessWithResult<OutputE, ProcessedOutputE>
    {
        public NestedProcessWithResult(string name) : base(name)
        {
            After(OutputEHandler)
                .ThenClarifyType(OutputEHandler)
                .ExtractData(u => u.Output)
                .TransmitTo(Finish);
        }

        public IActionTaskWithInputAndResult<OutputE, ProcessedOutputE> OutputEHandler { get; }
    }

    public class ExampleProcess : Process<ExampleInput>
    {
        public ExampleProcess(string name) : base(name)
        {
            EventA
                .Setup()
                .OverrideName("Overriden Event A");

            StartAt(EventA)
                .ExtractData(u => u.CaughtMessage)
                .TransmitTo(TaskA)
                .ExtractData(u => u.Output)
                .TransmitTo(TaskAB)
                .ExtractData(e => e.ReplyMessage)
                .TransmitTo(TaskC);

            StartAt(EventB)
                .ExtractData(u => u.CaughtMessage)
                .TransmitTo(TaskB)
                .Then(EventС)
                .ThenClarifyType(EventС)
                .ExtractData(u => u.CaughtMessage)
                .TransmitTo(TaskCC);

            StartAt(AAA)
                .Then(TaskAAA)
                .Then(TaskBBB)
                .Then(TaskCCC)
                .Then(TaskDDD);

            StartAt(EventD)
                .ThenParallel(TaskE, TaskF, TaskG, TaskJ);

            AfterResult(TaskE)
                .TransmitTo(TaskEE);

            AfterResult(TaskE)
                .TransmitTo(NestedProcess);

            AfterResult(TaskE)
                .TransmitTo(NestedProcessWithResult)
                .ExtractData(u => u.Result)
                .TransmitTo(ProcessedOutputEHandler);

            AfterResult(TaskF)
                .TransmitTo(TaskFF);

            AfterResult(TaskG)
                .TransmitTo(TaskGG);

            AfterResult(TaskJ)
                .TransmitTo(TaskJJ);

            AfterAll(TaskEE, TaskFF, TaskGG, TaskJJ, TaskDDD, TaskCC, TaskC, NestedProcess, ProcessedOutputEHandler)
                .Then(Completed);
        }

        public ICatchingMessageEvent<MessageA> EventA { get; }
        public ICatchingMessageEvent<MessageB> EventB { get; }
        public ICatchingMessageEvent<MessageС> EventС { get; }
        public IActionTaskWithInputAndResult<MessageA, OutputA> TaskA { get; }
        public IActionTaskWithInput<MessageB> TaskB { get; }
        public IRequestReplyTask<OutputA, RequestB, ReplyB> TaskAB { get; }
        public ISendMessageTask<ReplyB, ReplyB> TaskC { get; }
        public ISendMessageTask<MessageС, MessageCC> TaskCC { get; }
        public ITimerEvent EventD { get; }
        public IEmptyEvent AAA { get; }
        public IActionTask TaskAAA { get; }
        public IActionTask TaskBBB { get; }
        public IActionTask TaskCCC { get; }
        public IActionTask TaskDDD { get; }
        public ITerminationEvent TerminationA { get; }
        public IActionTaskWithResult<OutputE> TaskE { get; }
        public IActionTaskWithResult<OutputF> TaskF { get; }
        public IActionTaskWithResult<OutputG> TaskG { get; }
        public IActionTaskWithResult<OutputJ> TaskJ { get; }
        public IActionTaskWithInput<OutputE> TaskEE { get; }
        public IActionTaskWithInput<OutputF> TaskFF { get; }
        public IActionTaskWithInput<OutputG> TaskGG { get; }
        public IActionTaskWithInput<OutputJ> TaskJJ { get; }
        public NestedProcess NestedProcess { get; }
        public NestedProcessWithResult NestedProcessWithResult { get; }
        public IActionTaskWithInput<ProcessedOutputE> ProcessedOutputEHandler { get; }
    }

    #region messages

    public class ExampleInput
    {

    }

    public class MessageA
    {

    }

    public class MessageB
    {

    }

    public class MessageС
    {

    }

    public class OutputA
    {

    }
    public class RequestB
    {

    }
    public class ReplyB
    {

    }

    public class MessageCC
    {

    }

    public class OutputE
    {

    }

    public class ProcessedOutputE
    {

    }

    public class OutputF
    {

    }
    public class OutputG
    {

    }
    public class OutputJ
    {

    }
    #endregion
}
```
