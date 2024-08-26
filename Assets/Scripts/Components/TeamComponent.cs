using Zenject;

namespace ShootEmUp
{
	public sealed class TeamComponent
	{
		public bool IsPlayer
		{
			get { return _isPlayer; }
		}
		
		private bool _isPlayer;
		
		public TeamComponent(bool isPlayer)
		{
			_isPlayer = isPlayer;
		}
	}
}