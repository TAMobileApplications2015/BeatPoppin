namespace BeatPoppin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;
    using Commands;
    using Pages;
    using Data;
    using Data.Models;

    public class AppShellViewModel
    {
        private Frame root;
        private ICommand playCommand;
        private ICommand homeCommand;
        private ICommand helpCommand;
        private ICommand aboutUsCommand;
        private ICommand showMoreCommand;

        public AppShellViewModel(Frame root)
        {
            this.root = root;
        }

        public ICommand ShowMore
        {
            get
            {
                if (this.showMoreCommand == null)
                {
                    this.showMoreCommand = new RelayCommandWithParams(this.ExecShowMore);
                }

                return this.showMoreCommand;
            }
        }

        public ICommand Home
        {
            get
            {
                if (this.homeCommand == null)
                {
                    this.homeCommand = new RelayCommand(this.ExecHome);
                }

                return this.homeCommand;
            }
        }

        public ICommand Play
        {
            get
            {
                if (this.playCommand == null)
                {
                    this.playCommand = new RelayCommand(this.ExecPlay);
                }

                return this.playCommand;
            }
        }

        public ICommand Help
        {
            get
            {
                if (this.helpCommand == null)
                {
                    this.helpCommand = new RelayCommand(this.ExecHelp);
                }

                return this.helpCommand;
            }
        }

        public ICommand AboutUs
        {
            get
            {
                if (this.aboutUsCommand == null)
                {
                    this.aboutUsCommand = new RelayCommand(this.ExecAboutUs);
                }

                return this.aboutUsCommand;
            }
        }

        private void ExecShowMore(object obj)
        {
            var splitViewContainer = obj as SplitView;
            splitViewContainer.OpenPaneLength = splitViewContainer.ActualWidth;
            splitViewContainer.IsPaneOpen = !splitViewContainer.IsPaneOpen;
        }

        private async void ExecPlay()
        {
            root.Navigate(typeof(GamePage));
        }

        private void ExecHome()
        {
            root.Navigate(typeof(HomePage));
        }

        private void ExecHelp()
        {
            root.Navigate(typeof(HelpPage));
        }

        private void ExecAboutUs()
        {
            root.Navigate(typeof(AboutUsPage));
        }
    }
}
