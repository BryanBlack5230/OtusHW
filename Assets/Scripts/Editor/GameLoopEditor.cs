using UnityEditor;
using UnityEngine;
namespace ShootEmUp
{
    [CustomEditor(typeof(GameLoopManager))]
	public class GameLoopEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			var gameLoopManager = (GameLoopManager) target;
			
			if (GUILayout.Button("Start Game"))
			{
				gameLoopManager.StartGame();
			}
			
			if (GUILayout.Button("Finish Game"))
			{
				gameLoopManager.FinishGame();
			}
			
			if (GUILayout.Button("Pause Game"))
			{
				gameLoopManager.PauseGame();
			}
			
			if (GUILayout.Button("Resume Game"))
			{
				gameLoopManager.ResumeGame();
			}
		}
	}
}

