using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;
using JumbotronGame.AndroidApp.ViewModels.Main;
using JumbotronGame.AndroidApp.Views.Fragments;
using System.Collections.Generic;

namespace JumbotronGame.AndroidApp.Views.Activities
{
    [Activity(Label = "JumbotronGame.AndroidApp", MainLauncher = true)]
    public class MainActivity : ActivityBase
    {
        #region Fields

        private readonly Dictionary<View, Binding> _arenaEventHeaderBindings = new Dictionary<View, Binding>();
        private readonly List<Binding> _bindings = new List<Binding>();

        #endregion Fields

        #region Properties

        public IMainViewModel ViewModel => App.Locator.Main;

        public GridLayout ArenaEventsListContainerElement { get; private set; }

        public ProgressBar ArenaEventsSyncElement { get; private set; }

        #endregion Properties

        #region Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);

            ActionBarHelper.SetupActionBar(this);

            ArenaEventsListContainerElement = FindViewById<GridLayout>(Resource.Id.ArenaEventsListContainer);
            ArenaEventsSyncElement = FindViewById<ProgressBar>(Resource.Id.LoadingProgress);

            _bindings.Add(this.SetBinding(
                () => ViewModel.ArenaEvents.IsSynchronizing,
                () => ArenaEventsListContainerElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Gone : ViewStates.Visible));
            _bindings.Add(this.SetBinding(
                () => ViewModel.ArenaEvents.IsSynchronizing,
                () => ArenaEventsSyncElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Visible : ViewStates.Gone));

            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.ArenaEventsListContainer, new ArenaEventListFragment(ViewModel.ArenaEvents));
            fragmentTransaction.Commit();

            ViewModel.ArenaEvents.Synchronize();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.options_menu, menu);

            var userProfileItem = menu.FindItem(Resource.Id.action_userprofile);
            userProfileItem.SetIntent(new Intent(this, typeof(UserProfileActivity)));

            return true;
        }

        private View GetArenaEventTemplate(int position, ArenaEventListItemViewModel itemViewModel, View convertView)
        {
            // CleanUp & Initialize
            if (convertView != null)
            {
                if (_arenaEventHeaderBindings.TryGetValue(convertView, out var existBinding))
                {
                    existBinding.Detach();

                    _arenaEventHeaderBindings.Remove(convertView);
                }
            }
            else
            {
                convertView = LayoutInflater.Inflate(Resource.Layout.ArenaEventListItemTemplate, null);
            }

            // Header
            var headerElement = convertView.FindViewById<TextView>(Resource.Id.HeaderTextView);

            _arenaEventHeaderBindings.Add(convertView,
                new Binding<string, string>(itemViewModel, () => itemViewModel.Header, headerElement, () => headerElement.Text, BindingMode.OneWay));

            return convertView;
        }

        #endregion Methods
    }
}