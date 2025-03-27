using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [ShowInInspector] private ISaveLoader[] _saveLoaders;
        private IGameRepository _gameRepository;

        [Inject]
        public void Construct(ISaveLoader[] saveLoaders, IGameRepository gameRepository)
        {
            _saveLoaders = saveLoaders;
            _gameRepository = gameRepository;
        }

        [Button]
        public void SaveGame()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(_gameRepository);
            }

            _gameRepository.SaveState();
        }

        [Button]
        public void LoadGame()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame(_gameRepository);
            }
            
            _gameRepository.LoadState();
        }
    }
}