using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Services
{
	public enum Language
	{
		None,
		En,
		Ru
	}

	public class LocalizationService : MonoBehaviour
	{

		#region Fields, Props, Events

		public Action OnLanguageChanged;
	
		[SerializeField]
		private TextAsset _textFile;
		
		private Dictionary<string, List<string>> _localization;
		
		private Language _selectedLanguage;

		private Language SelectedLanguage {
			get => _selectedLanguage;
			set {
				_selectedLanguage = value;
				OnLanguageChanged?.Invoke();
			} 
		}

		#endregion

		#region Methods

		private void Awake()
		{
			Init();
		}

		private void Init()
		{
			if (_localization == null) {
				LoadLocalization();	
			}

			if (Application.systemLanguage == SystemLanguage.Russian    ||
			    Application.systemLanguage == SystemLanguage.Ukrainian  ||
			    Application.systemLanguage == SystemLanguage.Belarusian ||
			    Application.systemLanguage == SystemLanguage.Latvian    ||
			    Application.systemLanguage == SystemLanguage.Estonian) {
				
				SelectedLanguage = Language.Ru;
			}
			else {
				SelectedLanguage = Language.En;
			}
		}

		/// <summary>
		/// Returns the translate for selected key 
		/// </summary>
		/// <param name="language">
		/// Specify this value if you want to receive
		/// a translation not for the selected language
		/// </param>
		public string GetTranslate(string key, Language language = Language.None)
		{
			if (language == Language.None) {
				language = _selectedLanguage;
			}

			if (_localization.ContainsKey(key)) {
				return _localization[key][(int)language];
			}

			return key;
		}

		private void LoadLocalization()
		{
			_localization = new Dictionary<string, List<string>>();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(_textFile.text);
		
			if (xmlDocument["Keys"] == null) {
				Debug.LogError("Keys not found in XML");
				return;
			}
		
			foreach (XmlNode key in xmlDocument["Keys"].ChildNodes) {
				if (key.Attributes == null) {
					Debug.LogError("Attributes not found in key [" + key + "]");
					return;
				}
			
				string keyStr = key.Attributes["name"].Value;
				var values = new List<string>();
			
				
				foreach (XmlNode translate in key.ChildNodes) {
					values.Add(translate.InnerText);
				}

				_localization[keyStr] = values;
			}
		}

		#endregion
	}
}