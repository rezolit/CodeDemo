using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
	public class DecisionWindow : MonoBehaviour
	{
		public Action DecisionMade;
		
		[SerializeField]
		private List<DecisionButton> _buttons;

		[SerializeField]
		private Transform _buttonsStartPoint;

		private void Awake()
		{
			Init();	
		}

		private void Init()
		{
			HideAllButtons();
			foreach (DecisionButton decisionButton in _buttons) {
				decisionButton.Clicked += MakeDecision;
			}
		}

		private void HideAllButtons()
		{
			foreach (DecisionButton decisionButton in _buttons) {
				decisionButton.gameObject.SetActive(false);
			}
		}

		public void ShowDecisionWindow(List<Decision> decisions)
		{
			if (decisions.Count > _buttons.Count) {
				Debug.LogError("Not enough button for all decisions");
			}
			
			HideAllButtons();

			for (int i = 0; i < decisions.Count; ++i) {
				_buttons[i].transform.position = 
					_buttonsStartPoint.position + Vector3.down * i * ((Screen.height / 2) / decisions.Count);
				_buttons[i].BindDecision(decisions[i]);
			}
		}

		private void MakeDecision(Decision decision)
		{
			HideAllButtons();
			DecisionMade?.Invoke();
		}
	}
}