
using System.Collections.Generic;

namespace Plainion.Windows.Controls.Tree
{
    // used to store additional state to the actual INode model.
    // we cannot store state in NodeItem directly as those instances lifecylced ItemContainerGenerator.
    // esp. with virtualization enabled those items might be created on demand and destroyed frequently.
    class StateContainer
    {
        private readonly Dictionary<object, NodeState> myStates;
        private INode myDataContext;

        public StateContainer()
        {
            myStates = new Dictionary<object, NodeState>();
        }

        public INode DataContext
        {
            get { return myDataContext; }
            set
            {
                if (myDataContext != value)
                {
                    myDataContext = value;
                    myStates.Clear();
                }
            }
        }

        public NodeState GetRoot()
        {
            return GetOrCreate(DataContext);
        }

        internal NodeState GetOrCreate(object dataContext)
        {
            NodeState state;
            if (!myStates.TryGetValue(dataContext, out state))
            {
                state = new NodeState((INode)dataContext, this);
                myStates[dataContext] = state;
            }
            return state;
        }
    }
}
