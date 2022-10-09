using Saga.Core.Engine.StateMachine.Builders;
using Seedwork.Messaging.Contracts;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public class ProcessManagerPrimer : ProcessManager<ProcessManagerPrimer>
    {
        public ProcessManagerPrimer()
        {
            UnionEvents(Root_CheckedOk1, Root_CheckedOk2)
                .WhenRaisedAll()
                .ThenRaise(Root_Checked);

            Event(Root_CheckedNotOk)
                .WhenRaised()
                .ThenRaise(Aborted);

            During(Root1)
                .JustWait(Root_Checked)
                .ThenWhenLeave()
                .TransitionTo(Root3);

            During(Root2)
                .JustWait(Root_Checked)
                .ThenWhenLeave()
                .TransitionTo(Root3);

            WhenLeave(Root3)
                .TransitionTo(A)
                .TransitionTo(D)
                .TransitionTo(J);

            WhenEnter(A)
                .Send(_ => new CheckA())
                .ThenDuring()
                .WhenReceived(A_Checked, ctx => ctx.DoNothing())
                .ThenWhenLeave()
                .TransitionToIfElse(B, F, _ => A_Checked.Message.IsSuccessfully)
                .TransitionTo(C);

            WhenEnter(B)
                .Send(_ => new CheckB())
                .ThenDuring()
                .JustWait(B_Checked1)
                .JustWait(B_Checked2)
                .JustWait(B_Checked3)
                .ThenWhenLeave()
                //.TransitionToIfElse(C, F, _ => B_Checked.Message.IsSuccessfully)
                .TransitionTo(C);

            WhenEnter(C)
                .Send(_ => new CheckC())
                .ThenDuring()
                .JustWait(C_Checked)
                .ThenWhenLeave()
                //.TransitionToIfElse(D, F, _ => C_Checked.Message.IsSuccessfully);
                //check it
                //.TransitionTo(C);
                .TransitionTo(F);

            WhenEnter(D)
                //.Send(_ => new CheckD())
                .ThenDuring()
                //.JustWait(D_Checked)
                .ThenWhenLeave()
                //.TransitionToIfElse(E, F, _ => D_Checked.Message.IsSuccessfully)
                .TransitionTo(E);

            WhenEnter(E)
                .Send(_ => new CheckE())
                .ThenDuring()
                //.WhenReceived(E_Checked, (ctx) => ctx.AbortProcess())
                .JustWait(E_Checked)
                .ThenWhenLeave()
                .TransitionTo(F);

            During(F)
                .JustWait(Root_Checked);

            WhenLeave(F)
                .TransitionTo(M);

            WhenLeave(M)
                .TransitionTo(L);

            WhenLeave(J)
                .TransitionTo(M);

            WhenLeave(L)
                .TransitionTo(O);

            WhenLeave(O)
                .Finish();

            //наложение
            //WhenLeave(F)
            //    .Finish()
            //    //where f leave
            //    .Publish(_ => new AllChecksFinished());
        }

        public IState<ProcessManagerPrimer> Root1 { get; private set; }
        public IState<ProcessManagerPrimer> Root2 { get; private set; }
        public IState<ProcessManagerPrimer> Root3 { get; private set; }
        public IState<ProcessManagerPrimer> A { get; private set; }
        public IState<ProcessManagerPrimer> B { get; private set; }
        public IState<ProcessManagerPrimer> C { get; private set; }
        public IState<ProcessManagerPrimer> D { get; private set; }
        public IState<ProcessManagerPrimer> E { get; private set; }
        public IState<ProcessManagerPrimer> F { get; private set; }
        public IState<ProcessManagerPrimer> M { get; private set; }
        public IState<ProcessManagerPrimer> L { get; private set; }
        public IState<ProcessManagerPrimer> J { get; private set; }
        public IState<ProcessManagerPrimer> O { get; private set; }

        public IProcessMessageEvent<AChecked> A_Checked { get; private set; }
        public IProcessMessageEvent<BChecked1> B_Checked1 { get; private set; }
        public IProcessMessageEvent<BChecked2> B_Checked2 { get; private set; }
        public IProcessMessageEvent<BChecked3> B_Checked3 { get; private set; }
        public IProcessMessageEvent<CChecked> C_Checked { get; private set; }
        public IProcessMessageEvent<DChecked> D_Checked { get; private set; }
        public IProcessMessageEvent<EChecked> E_Checked { get; private set; }
        public IProcessEvent Root_Checked { get; private set; }
        public IProcessMessageEvent<RootCheckedOk1> Root_CheckedOk1 { get; private set; }
        public IProcessMessageEvent<RootCheckedOk2> Root_CheckedOk2 { get; private set; }
        public IProcessMessageEvent<RootCheckedNotOk> Root_CheckedNotOk { get; private set; }
    }

    public class AllChecksFinished : IEvent
    {
        public string CorrelationId { get; }
    }

    public class CheckA : ICommand
    {
        public string CorrelationId { get; }
        public string Destination { get; }
    }

    public class CheckB : ICommand
    {
        public string CorrelationId { get; }
        public string Destination { get; }
    }

    public class CheckC : ICommand
    {
        public string CorrelationId { get; }
        public string Destination { get; }
    }

    public class CheckD : ICommand
    {
        public string CorrelationId { get; }
        public string Destination { get; }
    }

    public class CheckE : ICommand
    {
        public string CorrelationId { get; }
        public string Destination { get; }
    }

    public class AChecked : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; init; }
    }

    public class BChecked1 : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class BChecked2 : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class BChecked3 : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class CChecked : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class DChecked : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class EChecked : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; }
    }

    public class RootCheckedOk1 : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; init; }
    }

    public class RootCheckedOk2 : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; init; }
    }

    public class RootCheckedNotOk : IEvent
    {
        public string CorrelationId { get; }
        public bool IsSuccessfully { get; init; }
    }
}