using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using JumbotronGame.AndroidApp.ViewModels.Quiz;
using JumbotronGame.Server.DataContracts.Game;
using System.Collections.Generic;

namespace JumbotronGame.AndroidApp.Views.Fragments
{
    public class QuizFragment : Fragment
    {
        #region Fields

        private Dictionary<View, Binding> _bindings = new Dictionary<View, Binding>();

        #endregion Fields

        #region .ctor

        public QuizFragment()
        {

        }

        public QuizFragment(QuizViewModel quiz)
        {
            ViewModel = quiz;
        }

        #endregion .ctor

        #region Properties

        public QuizViewModel ViewModel { get; }

        public TextView QuestionElement { get; private set; }

        public Button AnswerAElement { get; private set; }

        public Button AnswerBElement { get; private set; }

        public Button AnswerCElement { get; private set; }

        public Button AnswerDElement { get; private set; }

        #endregion Properties

        #region Methods

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.QuizFragment, container, false);

            QuestionElement = view.FindViewById<TextView>(Resource.Id.QuizQuestion);
            AnswerAElement = view.FindViewById<Button>(Resource.Id.QuizAnswerA);
            AnswerBElement = view.FindViewById<Button>(Resource.Id.QuizAnswerB);
            AnswerCElement = view.FindViewById<Button>(Resource.Id.QuizAnswerC);
            AnswerDElement = view.FindViewById<Button>(Resource.Id.QuizAnswerD);

            _bindings.Add(QuestionElement, this.SetBinding(() => ViewModel.CurrentQuestion.Question, () => QuestionElement.Text));
            _bindings.Add(AnswerAElement, this.SetBinding(() => ViewModel.CurrentQuestion.AnswerA, () => AnswerAElement.Text));
            _bindings.Add(AnswerBElement, this.SetBinding(() => ViewModel.CurrentQuestion.AnswerB, () => AnswerBElement.Text));
            _bindings.Add(AnswerCElement, this.SetBinding(() => ViewModel.CurrentQuestion.AnswerC, () => AnswerCElement.Text));
            _bindings.Add(AnswerDElement, this.SetBinding(() => ViewModel.CurrentQuestion.AnswerD, () => AnswerDElement.Text));

            AnswerAElement.Click += OnAnswerAElementClick;
            AnswerBElement.Click += OnAnswerBElementClick;
            AnswerCElement.Click += OnAnswerCElementClick;
            AnswerDElement.Click += OnAnswerDElementClick;

            return view;
        }

        private async void SetAnswer(QuizAnswerNumber answer)
        {
            var question = ViewModel?.CurrentQuestion;
            if (question != null)
            {
                question.CurrentAnswer = answer;

                ViewModel.CurrentQuestionIndex += 1;

                await ViewModel.SaveAnswerSetAsync().ConfigureAwait(false);
            }
        }

        private void OnAnswerAElementClick(object sender, System.EventArgs e)
        {
            SetAnswer(QuizAnswerNumber.A);
        }

        private void OnAnswerBElementClick(object sender, System.EventArgs e)
        {
            SetAnswer(QuizAnswerNumber.B);
        }

        private void OnAnswerCElementClick(object sender, System.EventArgs e)
        {
            SetAnswer(QuizAnswerNumber.C);
        }

        private void OnAnswerDElementClick(object sender, System.EventArgs e)
        {
            SetAnswer(QuizAnswerNumber.D);
        }

        public override void OnDestroyView()
        {
            foreach (var binding in _bindings)
            {
                binding.Value.Detach();
            }

            _bindings.Clear();

            base.OnDestroyView();
        }

        #endregion Methods
    }
}