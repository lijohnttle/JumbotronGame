using Android.App;
using Android.Widget;

namespace JumbotronGame.AndroidApp.Views
{
    public static class ActionBarHelper
    {
        public static Toolbar SetupActionBar(Activity activity)
        {
            var actionbar = activity.FindViewById<Toolbar>(Resource.Id.actionBar);
            activity.SetActionBar(actionbar);

            activity.ActionBar.Title = activity.GetString(Resource.String.app_name);

            return actionbar;
        }
    }
}