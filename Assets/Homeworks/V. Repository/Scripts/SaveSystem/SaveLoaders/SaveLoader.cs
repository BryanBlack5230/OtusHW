using Zenject;

namespace SaveSystem
{
    public interface ISaveLoader
    {
        public void SaveGame(IGameRepository gameRepository);
        public void LoadGame(IGameRepository gameRepository);
        
    }
    
    public abstract class SaveLoader<TService, TData> : ISaveLoader
    {
        protected readonly TService service;

        [Inject]
        public SaveLoader(TService service)
        {
            this.service = service;
        }
        public void SaveGame(IGameRepository gameRepository)
        {
            TData data = ConvertToData();
            gameRepository.SetData(data);
        }

        public void LoadGame(IGameRepository gameRepository)
        {
            if (gameRepository.TryGetData(out TData data))
            {
                SetupData(data);
            }
            else
            {
                SetupDefaultData();
            }
        }

        protected abstract TData ConvertToData();
        protected abstract void SetupData(TData data);
        protected virtual void SetupDefaultData() {}
    }
}