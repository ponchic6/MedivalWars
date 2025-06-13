namespace Code.Infrastructure.Services
{
    public interface ISaveService
    {
        public int GetMaxLevel();
        public void SetMaxLevel(int value);
    }
}