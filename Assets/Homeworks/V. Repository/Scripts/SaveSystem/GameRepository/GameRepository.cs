using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new Dictionary<string, string>();
        private const string GAME_STATE_KEY = "GameState";

        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();

            if (_gameState.TryGetValue(key, out var jsonData))
            {
                data = JsonConvert.DeserializeObject<T>(jsonData);
                return true;
            } 
            data = default(T);
            return false;
        }
        
        public void SetData<T>(T data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var key = typeof(T).ToString();
            _gameState[key] = jsonData;
        }

        public void LoadState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                Debug.Log("Loaded Data:\n" + gameStateJson);
                _gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
                Debug.Log("Game loaded");
            }
            else
            {
                Debug.Log("No save found");
            }
        }

        public void SaveState()
        {
            var gameStateJson = JsonConvert.SerializeObject(_gameState);
            Debug.Log("Saved Data:\n" + gameStateJson);
            PlayerPrefs.SetString(GAME_STATE_KEY, gameStateJson);
            PlayerPrefs.Save();
            Debug.Log("Game saved");
        }
    }
}