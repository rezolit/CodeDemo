using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class LocationInstaller : MonoInstaller
	{
		[SerializeField]
		private Transform _startPoint;
		
		[SerializeField]
		private PlayerInput _playerCharacterPrefab;
		
		public override void InstallBindings()
		{
			BindPlayerInterface();
		}

		private void BindPlayerInterface()
		{
			PlayerInput playerInput = Container.InstantiatePrefabForComponent<PlayerInput>(
				_playerCharacterPrefab,
				_startPoint.position,
				Quaternion.identity,
				null
			);

			Container
				.Bind<PlayerInput>()
				.FromInstance(playerInput)
				.AsSingle();
		}
	}
}