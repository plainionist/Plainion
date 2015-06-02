using System.ComponentModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Mvvm;
using Plainion.AppFw.Wpf.Model;
using Plainion.AppFw.Wpf.Services;

namespace Plainion.AppFw.Wpf.ViewModels
{
    [Export( typeof( TitleViewModel<> ) )]
    public class TitleViewModel<TProject> : BindableBase where TProject : ProjectBase
    {
        private string myApplicationName;
        private string myTitle;
        private IProjectService<TProject> myProjectService;

        [ImportingConstructor]
        public TitleViewModel( IProjectService<TProject> projectService )
        {
            myProjectService = projectService;

            myProjectService.ProjectChanging += OnProjectChanging;
            myProjectService.ProjectChanged += OnProjectChanged;
        }

        public string ApplicationName
        {
            get { return myApplicationName; }
            set
            {
                myApplicationName = value;
                if ( myTitle == null )
                {
                    Title = myApplicationName;
                }
            }
        }

        private void OnProjectChanging( TProject oldProject )
        {
            if ( myProjectService.Project != null )
            {
                myProjectService.Project.PropertyChanged -= OnProjectPropertyChanged;
            }
        }

        private void OnProjectChanged( TProject newProject )
        {
            if ( myProjectService.Project == null || string.IsNullOrEmpty( myProjectService.Project.Location ) )
            {
                Title = ApplicationName;
            }
            else
            {
                Title = string.Format( "{0} - {1}", ApplicationName, myProjectService.Project.Location );
            }

            if ( myProjectService.Project != null )
            {
                myProjectService.Project.PropertyChanged += OnProjectPropertyChanged;
            }
        }

        private void OnProjectPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == "IsDirty" )
            {
                Title = string.Format( "{0} - {1}{2}", ApplicationName, myProjectService.Project.Location, myProjectService.Project.IsDirty ? "*" : string.Empty );
            }
        }

        public string Title
        {
            get { return myTitle; }
            set { SetProperty( ref myTitle, value ); }
        }
    }
}
