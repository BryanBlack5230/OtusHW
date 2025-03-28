namespace SaveSystem
{
    public interface IGameRepository
    {
        public void SaveState();
        public void LoadState();
        public void SetData<T>(T data);
        public bool TryGetData<T>(out T data);
    }
}