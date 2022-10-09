using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Saga.Core.Engine.StateMachine.Abstractions;
using Saga.Core.Engine.StateMachine.Graph;

namespace Seedwork.ProcessManager.Tests
{
    [TestFixture]
    internal sealed class Test
    {
        //Background:
        //Given 
        //And a blog named "Greg's anti-tax rants"
        //And a customer named "Dr. Bill"
        //And a blog named "Expensive Therapy" owned by "Dr. Bill"

        //Given state machine initialized in state A
        //and

        [Test]
        public async Task PlaygroundTest()
        {
            var pm = new ProcessManagerPrimer();
            var graphvizGenerator = new GraphvizGenerator<ProcessManagerPrimer>(pm);

            var builder = new DirectedGraphBuilder();
            IProcessVisitor visitor = new BuildGraphFromProcessVisitor(builder);
            await pm.Accept(visitor);
            var graph = builder.Build();
            var validator = new GraphValidatorBuilder()
                .Use<GraphHasExactlyOneWayOutRule>()
                .Use<GraphWithoutCyclesRule>()
                .Build();

            var valid = validator.Validate(graph);
            valid.Should().BeTrue();

            var dotDefinition = graphvizGenerator
                .RebuildAdjacencyGraph()
                .GenerateDotDefinition();

            var queue = new Queue<IProcessEvent>();
            var context = new EventReactionContext<ProcessManagerPrimer>(pm, queue);
            pm.StartProcess(context);

            while (queue.Count > 0)
            {
                var candidate = queue.Dequeue();
                await pm.PropagateEventAsync(candidate, context, CancellationToken.None);
                candidate.MarkAsPropagated();
            }

            var messageSpread = new MessageSpreader<ProcessManagerPrimer>(pm);

            var root_checked = new RootChecked
            {
                IsSuccessfully = true,
            };
            await messageSpread.SpreadMessageAsync(root_checked, CancellationToken.None);

            var a_checked = new AChecked
            {
                IsSuccessfully = true,
            };
            await messageSpread.SpreadMessageAsync(a_checked, CancellationToken.None);

            var b1_checked = new BChecked1();
            await messageSpread.SpreadMessageAsync(b1_checked, CancellationToken.None);

            var c_checked = new CChecked();
            await messageSpread.SpreadMessageAsync(c_checked, CancellationToken.None);

            var b2_checked = new BChecked2();
            await messageSpread.SpreadMessageAsync(b2_checked, CancellationToken.None);

            dotDefinition = graphvizGenerator
                .RebuildAdjacencyGraph()
                .GenerateDotDefinition();

            GatherProcessStateVisitor gatherProcessStateVisitor = new GatherProcessStateVisitor();
            await pm.Accept(gatherProcessStateVisitor);

            RestoreProcessStateFromSavedStateVisitor restoreProcessStateVisitor = new RestoreProcessStateFromSavedStateVisitor(gatherProcessStateVisitor.GatheredState);

            pm = new ProcessManagerPrimer();
            await pm.Accept(restoreProcessStateVisitor);

            graphvizGenerator = new GraphvizGenerator<ProcessManagerPrimer>(pm);

            dotDefinition = graphvizGenerator
                .RebuildAdjacencyGraph()
                .GenerateDotDefinition();

            messageSpread = new MessageSpreader<ProcessManagerPrimer>(pm);

            var b3_checked = new BChecked3();
            await messageSpread.SpreadMessageAsync(b3_checked, CancellationToken.None);

            var e_checked = new EChecked();
            await messageSpread.SpreadMessageAsync(e_checked, CancellationToken.None);

            dotDefinition = graphvizGenerator
                .RebuildAdjacencyGraph()
                .GenerateDotDefinition();
        }
    }
}