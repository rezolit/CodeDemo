using Ineraction;
using Services;
using UnityEngine;
using Zenject;

namespace Dialog
{
	/// <summary>
	/// Lets you to see the dialog box and make decisions during the dialog
	/// </summary>
	public class NPC : MonoBehaviour, IInteractable
	{
		#region Fields

		[SerializeField]
		private SpeechSequence _speechSequence;
		[SerializeField]
		private DecisionsList _decisionsList;
		
		private bool _isInteractingWithPlayer;
		private TextInteractionService _textInteractionService;

		#endregion

		#region Methods

		[Inject]
		private void Init(TextInteractionService textInteractionService)
		{
			_textInteractionService = textInteractionService;
			_textInteractionService.OnTextInteractionEnd += InteractionEnd;
		}

		private void OnDestroy()
		{
			_textInteractionService.OnTextInteractionEnd -= InteractionEnd;
		}

		// It works only once (by unsubscribe from event) just for example
		private void InteractionEnd()
		{
			_textInteractionService.BeginDecision(_decisionsList);
			_textInteractionService.OnTextInteractionEnd -= InteractionEnd;
		}

		public void OnInteract()
		{
			_textInteractionService.BeginSpeeches(_speechSequence);
		}
	

		public void ShowHint()
		{
			GetComponent<SpriteRenderer>().color = Color.green;
		}

		public void HideHint()
		{
			GetComponent<SpriteRenderer>().color = Color.white;
		}

		#endregion
	}
}