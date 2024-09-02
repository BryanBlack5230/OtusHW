using UnityEngine;

namespace ShootEmUp
{
	[CreateAssetMenu(
        fileName = "InputConfig",
        menuName = "Configs/New InputConfig"
    )]
	public class InputConfig: ScriptableObject
	{
		[field: SerializeField] public KeyCode LeftKey {get; private set;} = KeyCode.A;
		[field: SerializeField] public KeyCode RightKey {get; private set;} = KeyCode.D;
		[field: SerializeField] public KeyCode FireKey {get; private set;} = KeyCode.Space;
	}
}