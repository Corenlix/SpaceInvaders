namespace Code.Levels
{
    public interface ILevelLoader
    {
        void LoadNext();
        void RestartCurrent();
    }
}