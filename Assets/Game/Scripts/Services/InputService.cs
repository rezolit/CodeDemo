using UnityEngine;
using UnityEngine.InputSystem;

namespace Services
{
	public class InputService : MonoBehaviour
	{

		#region Fields, Props

		public Vector2 MovementInput  => _movement.ReadValue<Vector2>();
		public InputAction Interact   => _interact;
		public InputAction SkipDialog => _skipDialog;
		
		private InputActions _inputActions;
		
		private InputAction  _movement;
		private InputAction  _interact;
		private InputAction  _skipDialog;

		#endregion

		#region Methods

		private void Awake()
		{
			Init();
		}

		private void Init()
		{
			_inputActions = new InputActions();

			_skipDialog = _inputActions.UI.SkipDialog;
			_movement   = _inputActions.Player.Movement;
			_interact   = _inputActions.Player.Interact;
		}

		private void OnEnable()
		{
			_skipDialog.Enable();
			_movement.Enable();
			_interact.Enable();
		}

		#endregion
	}
}
