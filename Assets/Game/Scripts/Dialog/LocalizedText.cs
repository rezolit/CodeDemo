using Services;
using UnityEngine;
using TMPro;
using Zenject;

namespace Interaction
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class LocalizedText : MonoBehaviour
	{
		#region Fields, Props

		[SerializeField]
		private TextMeshProUGUI _textMeshProMeshPro;
		
		private string _key;
		private LocalizationService _localizationService;

		public TextMeshProUGUI TextMeshPro {
			get => _textMeshProMeshPro;
			
			set => _textMeshProMeshPro = value;
		}

		#endregion
		
		[Inject]
		private void Init(LocalizationService localizationService)
		{
			_localizationService = localizationService;
			_textMeshProMeshPro = GetComponent<TextMeshProUGUI>();
			_localizationService.OnLanguageChanged += Relocalize;
		}

		private void OnDestroy()
		{
			_localizationService.OnLanguageChanged -= Relocalize;
		}

		private void Relocalize()
		{
			Localize();
		}

		public void Localize(string newKey = null)
		{
			if (newKey != null)
				_key = newKey;
			_textMeshProMeshPro.text = _localizationService.GetTranslate(_key);
		}
	}
}