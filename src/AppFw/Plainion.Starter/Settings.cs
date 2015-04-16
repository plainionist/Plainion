using System.Collections.Generic;
using System.IO;

namespace Plainion.Starter
{
    public class Settings
    {
        public Settings()
        {
            ScriptDirectories = new List<string>();
            ScriptDirectories.Add( Path.Combine( Path.GetDirectoryName( GetType().Assembly.Location ), "Starter" ) );
        }

        public List<string> ScriptDirectories { get; set; }
    }
}
