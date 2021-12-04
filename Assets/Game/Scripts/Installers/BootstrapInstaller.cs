using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class BootstrapInstaller : MonoInstaller
	{
		
		[SerializeField]
		private LocalizationService _localizationServicePrefab;
		
		[SerializeField]
		private InputService _inputServicePrefab;
		
		[SerializeField]
		private TextInteractionService _textInteractionServicePrefab;
		
		
		public override void InstallBindings()
		{
			BindLocalizationService();
			BindInputService();
			BindTextInteractionService();
		}

		private void BindInputService()
		{
			Container
				.Bind<InputService>()
				.FromComponentInNewPrefab(_inputServicePrefab)
				.AsSingle();
		}
		
		private void BindLocalizationService()
		{
			var localizationService = Container.InstantiatePrefabForComponent<LocalizationService>(
				_localizationServicePrefab,
				Vector3.zero,
				Quaternion.identity,
				null
			);

			Container
				.Bind<LocalizationService>()
				.FromInstance(localizationService)
				.AsSingle();
		}
		
		private void BindTextInteractionService()
		{
			var dialogService = Container.InstantiatePrefabForComponent<TextInteractionService>(
				_textInteractionServicePrefab,
				Vector3.zero,
				Quaternion.identity,
				null
			);

			Container
				.Bind<TextInteractionService>()
				.FromInstance(dialogService)
				.AsSingle();
		}
	}
}