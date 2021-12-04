using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteract : MonoBehaviour
    {

        #region Fields

        [SerializeField] private LayerMask      _interactLayer;
        [SerializeField] private InteractRadius _interactRadius;

        private PlayerInput _playerInput;

        #endregion

        #region Methods

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.Interact.performed += Interact;
            
            _playerInput.OnEnableInput += EnableInteract;
            _playerInput.OnDisableInput += DisableInteract;
        }

        private void OnDestroy()
        {
            _playerInput.Interact.performed -= Interact;
            _playerInput.OnEnableInput -= EnableInteract;
            _playerInput.OnDisableInput -= DisableInteract;
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
	        if (enabled) {
		        _interactRadius.NearestInteractableObject?.OnInteract();
	        }
        }
        
        private void EnableInteract() => enabled = true;
        
        private void DisableInteract() => enabled = false;

        #endregion

    }
}
