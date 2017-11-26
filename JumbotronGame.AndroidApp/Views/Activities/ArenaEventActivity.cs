using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using JumbotronGame.AndroidApp.ViewModels.ArenaEvents;
using JumbotronGame.AndroidApp.Views.Fragments;
using System.Collections.Generic;

namespace JumbotronGame.AndroidApp.Views.Activities
{
    [Activity(Label = "ArenaEventActivity")]
    public class ArenaEventActivity : Activity
    {
        #region Fields

        private readonly List<Binding> _bindings = new List<Binding>();

        #endregion Fields

        #region Properties

        public ArenaEventViewModel ViewModel { get; private set; }

        public TextView HeaderTextElement { get; private set; }

        public ProgressBar SyncProgressElement { get; private set; }

        public GridLayout QuizContainerElement { get; private set; }

        #endregion Properties

        #region Methods

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var arenaEventId = Intent.GetIntExtra("ArenaEventId", -1);
            var arenaEventHeader = Intent.GetStringExtra("ArenaEventHeader");

            ViewModel = App.Locator.ArenaEventFactory.Create();
            ViewModel.Id = arenaEventId;
            ViewModel.Header = arenaEventHeader;

            SetContentView(Resource.Layout.ArenaEventTemplate);

            HeaderTextElement = FindViewById<TextView>(Resource.Id.HeaderTextView);
            SyncProgressElement = FindViewById<ProgressBar>(Resource.Id.LoadingProgress);
            QuizContainerElement = FindViewById<GridLayout>(Resource.Id.QuizContainer);

            _bindings.Add(this.SetBinding(() => ViewModel.Header, () => HeaderTextElement.Text));
            _bindings.Add(this.SetBinding(() => ViewModel.Header, () => HeaderTextElement.Text));

            _bindings.Add(this.SetBinding(
                () => ViewModel.IsSynchronizing,
                () => QuizContainerElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Gone : ViewStates.Visible));
            _bindings.Add(this.SetBinding(
                () => ViewModel.IsSynchronizing,
                () => SyncProgressElement.Visibility,
                mode: BindingMode.OneWay)
                .ConvertSourceToTarget(t => t ? ViewStates.Visible : ViewStates.Gone));

            UpdateArenaEvent();
        }

        protected override void OnDestroy()
        {
            var quiz = ViewModel.Quiz;

            if (quiz != null)
            {
                quiz.QuizCompleted -= OnQuizCompleted;
            }

            base.OnDestroy();
        }

        private async void UpdateArenaEvent()
        {
            await ViewModel.SynchronizeAsync();

            var quiz = ViewModel.Quiz;

            if (quiz != null)
            {
                quiz.QuizCompleted -= OnQuizCompleted;
                quiz.QuizCompleted += OnQuizCompleted;

                var quizFragment = new QuizFragment(quiz);
                var fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Add(Resource.Id.QuizContainer, quizFragment);
                fragmentTransaction.Commit();
            }
        }

        private void OnQuizCompleted(object sender, System.EventArgs e)
        {
            var existFragment = FragmentManager.FindFragmentById(Resource.Id.QuizContainer);
            if (existFragment != null)
            {
                var removeFragmentTransaction = FragmentManager.BeginTransaction();
                removeFragmentTransaction.Remove(existFragment);
                removeFragmentTransaction.Commit();
            }

            var fragment = new QuizCompletedFragment();
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.QuizContainer, fragment);
            fragmentTransaction.Commit();
        }

        #endregion Methods
    }
}