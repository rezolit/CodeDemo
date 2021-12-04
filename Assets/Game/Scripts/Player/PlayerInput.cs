using System;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Zenject;

namespace Player
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerInput : MonoBehaviour
	{

		#region Fields, Props, Events

		public Action OnDisableInput;
		public Action OnEnableInput;
		
		private ReadonlyField<TextInteractionService> _dialogService;
		private ReadonlyField<InputService>  _inputService;
		
		public Vector2 MovementInput => _inputService.Value.MovementInput;
		public InputAction Interact => _inputService.Value.Interact;

		#endregion
		
		#region Methods

		[Inject]
		private void Init(InputService inputService, TextInteractionService textInteractionService)
		{
			_dialogService = new ReadonlyField<TextInteractionService>(textInteractionService);
			_inputService  = new ReadonlyField<InputService>(inputService);
			_dialogService.Value.OnTextInteractionBegin += DisableInput;
			_dialogService.Value.OnTextInteractionEnd += EnableInput;
		}
		
		private void OnDestroy()
		{
			_dialogService.Value.OnTextInteractionBegin -= DisableInput;
			_dialogService.Value.OnTextInteractionEnd -= EnableInput;
		}
		
		private void EnableInput() => OnEnableInput?.Invoke();

		private void DisableInput() => OnDisableInput?.Invoke();
		

		#endregion
	}
}
