namespace JumbotronGame.Server.DataContracts.Game
{
    public class QuizQuestion : Entity
    {
        public string Question { get; set; }

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC { get; set; }

        public string AnswerD { get; set; }
    }
}