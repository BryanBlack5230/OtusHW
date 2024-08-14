using UnityEngine;

namespace ShootEmUp
{
	public class InputConfig: MonoBehaviour
	{   // планируется сделать DI с InputInstaller
		[field: SerializeField] public KeyCode LeftKey {get; private set;} = KeyCode.A;
		[field: SerializeField] public KeyCode RightKey {get; private set;} = KeyCode.D;
		[field: SerializeField] public KeyCode FireKey {get; private set;} = KeyCode.Space;
	}
}