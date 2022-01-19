using System;
using UnityEngine;

namespace Dialog
{
	public enum DecisionType
	{
		Greeting,
		Ignore
	}
	
	[Serializable]
	public class Decision
	{
		[SerializeField]
		private string _phraseKey;

		[SerializeField]
		private DecisionType _decisionType;

		public string PhraseKey => _phraseKey;
		public DecisionType DecisionType => _decisionType;
	}
}