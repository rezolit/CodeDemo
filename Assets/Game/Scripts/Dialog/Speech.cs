using System;
using UnityEngine;

namespace Dialog
{
	[Serializable]
	public class Speech
	{
		[SerializeField]
		private CharacterData _character;
		
		[SerializeField]
		private string phraseKey;
		
		public string PhraseKey => phraseKey;
		public CharacterData Character => _character;
	}
}