using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Services
{
	public class TextInteractionService : MonoBehaviour
	{
		#region Field, Props, Events

		public event  Action OnTextInteractionBegin;
		public event  Action OnTextInteractionEnd;
		
		[SerializeField]
		private Canvas _canvas;

		[SerializeField]
		private LocalizedText _speechText;

		[SerializeField]
		private LocalizedText _speakerName;

		[SerializeField]
		private Image _speakerImage;

		[SerializeField]
		private DecisionWindow _decisionWindow;

		[SerializeField]
		private Transform _dialogUiRoot;
		
		private int _currentPhrase;
		private string _targetText;
		
		private Coroutine           _printPhraseRoutine;
		private List<Speech>        _currentSpeeches;
		private LocalizationService _localizationService;

		private bool IsDialogPerformed { get; set; }

		private bool IsDecisionPerformed { get; set; }

		#endregion

		#region Methods
		
		[Inject]
		private void Init(InputService inputService)
		{
			inputService.SkipDialog.performed += ShowNextSpeech;
			_decisionWindow.DecisionMade += EndDecision;
			
			IsDecisionPerformed = false;
			IsDialogPerformed = false;
			HideDialogUI();
			HideDecisionUI();
		}

		#region Dialog(Speech)

		public void BeginSpeeches(SpeechSequence speechSequence)
		{
			_currentSpeeches   = speechSequence.Speeches;
			_currentPhrase       = 0;
			
			_printPhraseRoutine = StartCoroutine(PrintSpeech(speechSequence.Speeches[_currentPhrase]));
				
			IsDialogPerformed = true;
			ShowDialogUI();
			OnTextInteractionBegin?.Invoke();
		}

		private void EndDialog()
		{
			IsDialogPerformed = false;
			HideDialogUI();
			OnTextInteractionEnd?.Invoke();
		}
		
		private void ShowDialogUI()
		{
			_dialogUiRoot.gameObject.SetActive(true);
		}

		private void HideDialogUI()
		{
			_dialogUiRoot.gameObject.SetActive(false);
		}

		private IEnumerator PrintSpeech(Speech speech)
		{
			_speakerImage.sprite = speech.Character.Portrait;
			_speakerName.Localize(speech.Character.NameKey);
			_speakerName.TextMeshPro.color = speech.Character.Color;
			_speechText.Localize(speech.PhraseKey);
			_targetText = _speechText.TextMeshPro.text;
			_speechText.TextMeshPro.text = "";
			
			foreach (var letter in _targetText) {
				_speechText.TextMeshPro.text += letter;
				yield return new WaitForSecondsRealtime(0.05f);
			}
		}

		private void ShowNextSpeech(InputAction.CallbackContext ctx)
		{
			if (IsDialogPerformed == false) {
				return;
			}

			if (_speechText.TextMeshPro.text != _targetText) {
				_speechText.TextMeshPro.text = _targetText;
				StopCoroutine(_printPhraseRoutine);
				return;
			}
			
			if (_currentPhrase == _currentSpeeches.Count - 1) {
				EndDialog();
				return;
			}

			_currentPhrase++;
			_printPhraseRoutine = StartCoroutine(PrintSpeech(_currentSpeeches[_currentPhrase]));
		}

		#endregion

		#region Decision

		public void BeginDecision(DecisionsList decisions)
		{
			IsDecisionPerformed = true;
			ShowDecisionUI();
			_decisionWindow.ShowDecisionWindow(decisions.Decisions);
			OnTextInteractionBegin?.Invoke();
		}
		
		private void EndDecision()
		{
			IsDecisionPerformed = false;
			HideDecisionUI();
			OnTextInteractionEnd?.Invoke();
		}
		
		private void ShowDecisionUI()
		{
			_dialogUiRoot.gameObject.SetActive(true);
		}

		private void HideDecisionUI()
		{
			_dialogUiRoot.gameObject.SetActive(false);
		}

		#endregion
		
		#endregion
	}
}
