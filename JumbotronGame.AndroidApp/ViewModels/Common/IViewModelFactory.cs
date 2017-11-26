namespace JumbotronGame.AndroidApp.ViewModels.Common
{
    public interface IItemFactory<TItem>
    {
        TItem Create();
    }
}