using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.Server.DataContracts.Game;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JumbotronGame.AndroidApp.ViewModels.Quiz
{
    public class QuizViewModel : ExtendedViewModel, IItemViewModel
    {
        #region Fields

        private readonly IRepository<QuizAnswerSet, QuizAnswerSetCriteria> _answerSetRepository;
        private int _id;
        private QuizQuestionViewModel[] _questions;
        private int _currentQuestionIndex;
        private bool _isCompleted;

        #endregion Fields

        #region Events

        public event EventHandler QuizCompleted;

        #endregion Events

        #region .ctor

        public QuizViewModel(IRepository<QuizAnswerSet, QuizAnswerSetCriteria> answerSetRepository)
        {
            _answerSetRepository = answerSetRepository;
        }

        #endregion .ctor

        #region Properties

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public QuizQuestionViewModel[] Questions
        {
            get => _questions;
            set
            {
                if (Set(ref _questions, value))
                {
                    CurrentQuestionIndex = 0;
                }
            }
        }

        public QuizQuestionViewModel CurrentQuestion
        {
            get => _currentQuestionIndex >= 0 && _currentQuestionIndex < _questions?.Length ? _questions[_currentQuestionIndex] : null;
        }

        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            set
            {
                value = Math.Max(0, Math.Min((Questions?.Length ?? 0) - 1, value));

                if (Set(ref _currentQuestionIndex, value))
                {
                    RaisePropertyChanged(nameof(CurrentQuestion));
                }
            }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            private set
            {
                if (Set(ref _isCompleted, value))
                {
                    if (_isCompleted)
                    {
                        QuizCompleted?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        #endregion Properties

        #region Methods

        public async Task SaveAnswerSetAsync()
        {
            var questions = Questions;
            if (questions != null)
            {
                if (questions.All(t => t.CurrentAnswer.HasValue))
                {
                    IsCompleted = true;
                }

                await _answerSetRepository.SaveItemAsync(new QuizAnswerSet
                {
                    Answers = questions.Select(t => t.CurrentAnswer).ToArray(),
                    QuizeId = Id,
                    UserPhone = "some phone"
                }, CancellationToken.None);
            }
        }

        #endregion Methods
    }
}