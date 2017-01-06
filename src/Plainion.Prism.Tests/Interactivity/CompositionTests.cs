
using NUnit.Framework;
using Plainion.Prism.Tests.Interactivity.Fakes;

namespace Plainion.Prism.Tests.Interactivity
{
    /// <summary>
    /// Test correct composition of Views/ViewModels for views created via "interactivity"
    /// </summary>
    [TestFixture]
    class CompositionTests
    {
        //[Test]
        public void StartAndStopApp()
        {
            Tasks.Tasks.StartSTATask(() =>
            {
                var app = new App();
                app.Run();
            });

            App.WaitForBeingLoaded();

            App.Close();
        }
    }
}
