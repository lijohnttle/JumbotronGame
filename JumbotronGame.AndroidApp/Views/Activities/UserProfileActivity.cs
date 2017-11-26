using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using JumbotronGame.AndroidApp.ViewModels.Users;
using System.Collections.Generic;

namespace JumbotronGame.AndroidApp.Views.Activities
{
    [Activity(Label = "UserProfileActivity")]
    public class UserProfileActivity : Activity
    {
        #region Fields

        private readonly List<Binding> _bindings = new List<Binding>();

        #endregion Fields

        #region Properties

        public UserProfileViewModel ViewModel { get; private set; }

        public View UserProfileContainerElement { get; private set; }

        public ProgressBar SyncProgressElement { get; private set; }

        public TextView FullNameElement { get; private set; }

        public TextView PointsElement { get; private set; }

        #endregion Properties

        #region Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserProfileTemplate);

            ViewModel = App.Locator.UserProfileFactory.Create();

            UserProfileContainerElement = FindViewById<View>(Resource.Id.UserProfile_Container);
            SyncProgressElement = FindViewById<ProgressBar>(Resource.Id.LoadingProgress);
            FullNameElement = FindViewById<TextView>(Resource.Id.UserProfile_fullName);
            PointsElement = FindViewById<TextView>(Resource.Id.UserProfile_points);

            _bindings.Add(this.SetBinding(
                () => ViewModel.IsSynchronizing,
                () => UserProfileContainerElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Gone : ViewStates.Visible));
            _bindings.Add(this.SetBinding(
                () => ViewModel.IsSynchronizing,
                () => SyncProgressElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Visible : ViewStates.Gone));

            _bindings.Add(this.SetBinding(
                () => ViewModel.FullName,
                () => FullNameElement.Text,
                mode: BindingMode.OneWay));
            _bindings.Add(this.SetBinding(
                () => ViewModel.Points,
                () => PointsElement.Text,
                mode: BindingMode.OneWay));

            ViewModel.Synchronize();
        }

        protected override void OnDestroy()
        {
            ViewModel.Dispose();

            base.OnDestroy();
        }

        #endregion Methods
    }
}