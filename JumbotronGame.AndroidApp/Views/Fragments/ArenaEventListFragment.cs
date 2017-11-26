using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;
using JumbotronGame.AndroidApp.Views.Activities;
using System;
using System.Collections.Generic;

namespace JumbotronGame.AndroidApp.Views.Fragments
{
    public class ArenaEventListFragment : ListFragment
    {
        #region Fields

        private readonly Dictionary<View, List<Binding>> _itemBindings = new Dictionary<View, List<Binding>>();
        private ObservableAdapter<ArenaEventListItemViewModel> _arenaEventsAdapter;

        #endregion Fields

        #region .ctor

        public ArenaEventListFragment()
        {

        }

        public ArenaEventListFragment(IArenaEventListViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #endregion .ctor

        #region Properties

        public IArenaEventListViewModel ViewModel { get; }

        #endregion Properties

        #region Methods

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _arenaEventsAdapter = new ObservableAdapter<ArenaEventListItemViewModel>
            {
                DataSource = ViewModel.Items,
                GetTemplateDelegate = GetArenaEventTemplate
            };

            ListAdapter = _arenaEventsAdapter;

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            ListView.Divider = null;

            base.OnActivityCreated(savedInstanceState);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var item = _arenaEventsAdapter[position];
            var intent = new Intent(Activity, typeof(ArenaEventActivity));
            intent.PutExtra("ArenaEventId", item.Id);
            intent.PutExtra("ArenaEventHeader", item.Header);
            StartActivity(intent);
        }

        private View GetArenaEventTemplate(int position, ArenaEventListItemViewModel itemViewModel, View convertView)
        {
            // CleanUp & Initialize
            if (convertView != null)
            {
                if (_itemBindings.TryGetValue(convertView, out var existBindings))
                {
                    foreach (var b in existBindings)
                    {
                        b.Detach();
                    }

                    _itemBindings.Remove(convertView);
                }
            }
            else
            {
                convertView = Activity.LayoutInflater.Inflate(Resource.Layout.ArenaEventListItemTemplate, null);
            }

            var itemBindings = new List<Binding>();
            _itemBindings.Add(convertView, itemBindings);
            
            // Header
            var headerElement = convertView.FindViewById<TextView>(Resource.Id.HeaderTextView);
            var dateElement = convertView.FindViewById<TextView>(Resource.Id.DateTextView);

            itemBindings.Add(new Binding<string, string>(itemViewModel, () => itemViewModel.Header, headerElement, () => headerElement.Text, BindingMode.OneWay));
            itemBindings.Add(new Binding<DateTime, string>(itemViewModel, () => itemViewModel.Date, dateElement, () => headerElement.Text, BindingMode.OneWay)
                .ConvertSourceToTarget(t => t.ToShortDateString()));

            // Preview Image
            if (itemViewModel.Id == 0 || itemViewModel.Id == 1)
            {
                var previewImage = convertView.FindViewById<ImageView>(Resource.Id.PreviewImage);

                if (itemViewModel.Id == 0)
                {
                    previewImage.SetImageDrawable(Context.GetDrawable(Resource.Drawable.save_on_foods));
                }
                else
                {
                    previewImage.SetImageDrawable(Context.GetDrawable(Resource.Drawable.prospera_place));
                }
            }

            return convertView;
        }

        #endregion Methods
    }
}