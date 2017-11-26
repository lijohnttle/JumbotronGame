using Android.App;
using Android.OS;
using Android.Views;

namespace JumbotronGame.AndroidApp.Views.Fragments
{
    public class QuizCompletedFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.QuizCompletedFragment, container, false);
        }
    }
}