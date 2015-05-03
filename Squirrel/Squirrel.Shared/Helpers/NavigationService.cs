using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Squirrel.Helpers
{
    public class NavigationService : INavigationService
    {
        public int BackStackDepth
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.BackStackDepth;
            }
        }

        public bool CanGoBack
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.CanGoBack;
            }
        }

        public Type CurrentPageType
        {
            get
            {
                var frame = ((Frame)Window.Current.Content);
                return frame.CurrentSourcePageType;
            }
        }

        public virtual void GoBack()
        {
            var frame = ((Frame)Window.Current.Content);

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public virtual void NavigateTo(Type sourcePageType)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType);
        }

        public virtual void NavigateTo(Type sourcePageType, object parameter)
        {
            ((Frame)Window.Current.Content).Navigate(sourcePageType, parameter);
        }

        public virtual void GoForward()
        {
            var frame = ((Frame)Window.Current.Content);

            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }

        public virtual void GoHome()
        {
            var frame = ((Frame)Window.Current.Content);

            while (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}
