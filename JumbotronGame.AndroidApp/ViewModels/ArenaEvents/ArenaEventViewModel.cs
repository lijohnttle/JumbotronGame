using JumbotronGame.AndroidApp.Repositories;
using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.AndroidApp.ViewModels.Quiz;
using JumbotronGame.Server.DataContracts.Arena;
using JumbotronGame.Server.DataContracts.Game;
using System.Linq;

namespace JumbotronGame.AndroidApp.ViewModels.ArenaEvents
{
    public class ArenaEventViewModel : SyncItemViewModel<ArenaEvent, ArenaEventCriteria>
    {
        #region Fields

        private string _header;
        private QuizViewModel _quiz;
        private readonly IRepository<QuizAnswerSet, QuizAnswerSetCriteria> _answerSetRepository;

        #endregion Fields

        #region .ctor

        public ArenaEventViewModel(IRepository<ArenaEvent, ArenaEventCriteria> repository,
            IRepository<QuizAnswerSet, QuizAnswerSetCriteria> answerSetRepository)
            : base(repository)
        {
            _answerSetRepository = answerSetRepository;

            StartReceiveNotifications();
        }

        #endregion .ctor

        #region Properties

        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
        }

        public QuizViewModel Quiz
        {
            get => _quiz;
            set => Set(ref _quiz, value);
        }

        #endregion Properties

        #region Methods

        protected override void Map(ArenaEvent entity)
        {
            Id = entity.Id;
            Header = entity.Header;

            if (entity.Quiz == null)
            {
                Quiz = null;
            }
            else
            {
                var quizEntity = entity.Quiz;

                Quiz = new QuizViewModel(_answerSetRepository)
                {
                    Id = quizEntity.Id,
                    Questions = quizEntity.Questions?.Select(t =>
                    {
                        return new QuizQuestionViewModel
                        {
                            Question = t.Question,
                            AnswerA = t.AnswerA,
                            AnswerB = t.AnswerB,
                            AnswerC = t.AnswerC,
                            AnswerD = t.AnswerD
                        };
                    }).ToArray()
                };
            }
        }

        #endregion Methods
    }
}