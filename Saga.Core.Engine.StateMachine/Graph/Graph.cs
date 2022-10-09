using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class Graph : IGraph
    {
        private readonly Dictionary<string, ISet<string>> _adjacency;
        private readonly Dictionary<string, ISet<string>> _prerequisites;
        private readonly ISet<string> _vertices;

        public Graph()
        {
            _adjacency = new Dictionary<string, ISet<string>>();
            _prerequisites = new Dictionary<string, ISet<string>>();
            _vertices = new HashSet<string>();
        }

        // ReSharper disable once IdentifierTypo
        public ISet<string> Vertices => _vertices;

        public IDictionary<string, ISet<string>> Adjacency => _adjacency;
        public IDictionary<string, ISet<string>> Prerequisites => _prerequisites;

        internal void AddEdge(string v, string w)
        {
            EnsureInitialized(_adjacency, v);
            EnsureInitialized(_adjacency, w);
            EnsureInitialized(_prerequisites, v);
            EnsureInitialized(_prerequisites, w);

            _adjacency[v].Add(w);
            _prerequisites[w].Add(v);

            _vertices.Add(v);
            _vertices.Add(w);
        }

        private void EnsureInitialized(IDictionary<string, ISet<string>> map, string key)
        {
            if (!map.ContainsKey(key))
                map[key] = new HashSet<string>();
        }
    }
}