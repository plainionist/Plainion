using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;

namespace Plainion.Windows.Controls.Tree.Cmp
{
    [DebuggerDisplay("{Name}, PID={Model.ProcessId}")]
    public class TraceProcessNode : NodeBase<TraceProcess>
    {
        private IReadOnlyCollection<TraceThreadNode> myThreads;
        private ICollectionView myVisibleThreads;

        // only used if here are no threads
        private bool myIsChecked;

        public TraceProcessNode(TraceProcess model)
            : base(model)
        {
            // default if there are no threads
            myIsChecked = false;
        }

        public string ProcessIdText
        {
            get { return Name == null ? string.Format("Pid={0}", Model.ProcessId) : string.Format("(Pid={0})", Model.ProcessId); }
        }

        protected override void OnModelPropertyChanged(string propertyName)
        {
            if (propertyName == "Name")
            {
                OnPropertyChanged("ProcessIdText");
            }
        }

        public IReadOnlyCollection<TraceThreadNode> Threads
        {
            get { return myThreads; }
            set
            {
                if (SetProperty(ref myThreads, value))
                {
                    if (myThreads != null)
                    {
                        foreach (var t in myThreads)
                        {
                            PropertyChangedEventManager.AddHandler(t, OnThreadIsCheckedChanged, "IsChecked");
                        }
                    }
                }

                myVisibleThreads = null;
                OnPropertyChanged("VisibleThreads");
            }
        }

        private void OnThreadIsCheckedChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("IsChecked");
        }

        public bool? IsChecked
        {
            get
            {
                if (myThreads == null)
                {
                    return myIsChecked;
                }

                if (Threads.All(t => t.IsChecked))
                {
                    return true;
                }

                if (Threads.All(t => !t.IsChecked))
                {
                    return false;
                }

                return null;
            }
            set
            {
                if (myThreads == null)
                {
                    myIsChecked = value == null ? false : value.Value;
                }
                else
                {
                    foreach (var t in Threads)
                    {
                        t.IsChecked = value.HasValue && value.Value;
                    }
                }

                OnPropertyChanged("IsChecked");
            }
        }

        public ICollectionView VisibleThreads
        {
            get
            {
                if (myVisibleThreads == null && myThreads != null)
                {
                    myVisibleThreads = CollectionViewSource.GetDefaultView(Threads);
                    myVisibleThreads.Filter = i => !((TraceThreadNode)i).IsFilteredOut;

                    OnPropertyChanged("VisibleThreads");
                }
                return myVisibleThreads;
            }
        }

        internal override void ApplyFilter(string filter)
        {
            var tokens = filter.Split('|');
            filter = tokens[0];
            var threadFilter = tokens.Length > 1 ? tokens[1] : filter;

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
                    && !Model.ProcessId.ToString().Contains(filter);
            }

            foreach (var thread in Threads)
            {
                thread.ApplyFilter(threadFilter);

                if (!thread.IsFilteredOut && tokens.Length == 1)
                {
                    IsFilteredOut = false;
                }
            }

            VisibleThreads.Refresh();
        }
    }
}
