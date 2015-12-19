namespace BeatPoppin.ViewModels
{
    using System;
    using System.ComponentModel;
    using Microsoft.QueryStringDotNET;
    using NotificationsExtensions.Toasts;
    using Windows.UI.Notifications;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private const int ToastedMessageExpirationTimeInSeconds = 5;
        private readonly string[] ToastMessageIconPaths = new string[]
        {
            "ms-appx:///Icons/appbar.smiley.frown.png",
            "ms-appx:///Icons/appbar.smiley.happy.png"
        };

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ToastMessage(string title, string content, ToastMessageIconsEnum icon)
        {
            //    string title = "Andrew sent you a picture";
            //    string content = "Check this out, Happy Canyon in Utah!";
            //string image = "http://blogs.msdn.com/cfs-filesystemfile.ashx/__key/communityserver-blogs-components-weblogfiles/00-00-01-71-81-permanent/2727.happycanyon1_5B00_1_5D00_.jpg";
            string logo = this.ToastMessageIconPaths[(int)icon];

            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                TitleText = new ToastText()
                {
                    Text = title
                },

                BodyTextLine1 = new ToastText()
                {
                    Text = content
                },

                //InlineImages =
                //{
                //    new ToastImage()
                //    {
                //        Source = new ToastImageSource(image)
                //    }
                //},

                AppLogoOverride = new ToastAppLogo()
                {
                    Source = new ToastImageSource(logo),
                    Crop = ToastImageCrop.Circle
                }
            };

            // Now we can construct the final toast content
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,

                // Arguments when the user taps body of toast
                Launch = new QueryString()
                {
                    { "action", "viewConversation" }
                    //{ "conversationId", conversationId.ToString() }
                }.ToString()
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());

            // Providing Expiration Time for Toast notification
            toast.ExpirationTime = DateTime.Now.AddSeconds(ToastedMessageExpirationTimeInSeconds);

            // provide identity to a toast notification, 
            // in order to target it at a later time, 
            // for the purposes of replacing or removing it.
            //toast.Tag = "1";
            //toast.Group = "wallPosts";

            // Send the notification Toast
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
