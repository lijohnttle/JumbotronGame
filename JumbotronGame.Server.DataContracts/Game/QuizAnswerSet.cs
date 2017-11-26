namespace JumbotronGame.Server.DataContracts.Game
{
    public enum QuizAnswerNumber
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3
    }

    public class QuizAnswerSet : Entity
    {
        public string UserPhone { get; set; }

        public int QuizeId { get; set; }

        public QuizAnswerNumber?[] Answers { get; set; }
    }
}