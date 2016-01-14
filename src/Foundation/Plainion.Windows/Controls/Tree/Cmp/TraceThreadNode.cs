using System;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    public class TraceThreadNode : NodeBase
    {
        private bool myIsChecked;

        public string ThreadIdText
        {
            get { return Name == null ? string.Format("Tid={0}", 2525) : string.Format("(Tid={0})", 2525); }
        }

        public bool IsChecked
        {
            get { return myIsChecked; }
            set { SetProperty(ref myIsChecked, value); }
        }

        internal override void ApplyFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                IsFilteredOut = false;
            }
            else if (filter == "*")
            {
                IsFilteredOut = Name == null;
            }
            else
            {
                IsFilteredOut = (Name == null || !Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    && !2525.ToString().Contains(filter);
            }
        }
    }
}
