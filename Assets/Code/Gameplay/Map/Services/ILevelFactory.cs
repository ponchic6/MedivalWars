namespace Code.Gameplay.Map.Services
{
    public interface ILevelFactory
    {
        public void SetDecorLevel(int levelIndex);
        public void CleanMap();
        public void StartLevel(int choseLevelValue);
        public void RestartLevel();
    }
}