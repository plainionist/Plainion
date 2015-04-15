using System;
using System.Collections.Generic;
using Plainion.AppFw.Shell.Forms;

namespace Plainion.AppFw.Shell.UnitTests
{
    public class TestApp : FormsAppBase
    {
        public TestApp()
        {
            DictionaryArgument = new Dictionary<string, string>();
            ListArgument = new List<string>();
        }

        [Argument( Short = "-B", Description = "BoolArgument" )]
        public bool BoolArgument
        {
            get;
            set;
        }

        [Argument( Short = "-S", Description = "StringArgument" )]
        public string StringArgument
        {
            get;
            set;
        }

        [Argument( Short = "-L", Description = "ListArgument" )]
        public IList<string> ListArgument
        {
            get;
            set;
        }

        [Argument( Short = "-D", Description = "DictionaryArgument" )]
        public IDictionary<string, string> DictionaryArgument
        {
            get;
            set;
        }

        protected override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
