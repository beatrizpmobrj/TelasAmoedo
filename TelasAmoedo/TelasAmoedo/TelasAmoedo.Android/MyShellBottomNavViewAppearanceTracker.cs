using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TelasAmoedo.Droid
{
    public class MyShellBottomNavViewAppearanceTracker : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {
        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {

            bottomView.SetBackgroundResource(Resource.Drawable.bottombackground);
        }
    }
}