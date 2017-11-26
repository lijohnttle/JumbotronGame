using JumbotronGame.AndroidApp.ViewModels.Common;
using JumbotronGame.Server.DataContracts.Game;

namespace JumbotronGame.AndroidApp.ViewModels.Quiz
{
    public class QuizQuestionViewModel : ExtendedViewModel
    {
        public string Question { get; set; }

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC { get; set; }

        public string AnswerD { get; set; }

        public QuizAnswerNumber? CurrentAnswer { get; set; }
    }
}