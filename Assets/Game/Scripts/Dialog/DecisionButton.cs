using System;
using UnityEngine;

namespace Interaction
{
	public class DecisionButton : MonoBehaviour
	{
		public Action<Decision> Clicked;

		private Decision _decision;

		[SerializeField]
		private LocalizedText _localizedText;
		
		public void BindDecision(Decision decision)
		{
			_decision = decision;
			_localizedText.Localize(_decision.PhraseKey);
			gameObject.SetActive(true);
		}
		
		public void OnClicked()
		{
			Clicked?.Invoke(_decision);
		}
	}
}