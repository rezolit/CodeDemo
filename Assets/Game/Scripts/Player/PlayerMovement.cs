using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] [Range(0.0f, 5000.0f)]
        private float _movementSpeed;

        private Rigidbody2D _rigidbody2D;
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
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _playerInput.OnEnableInput += EnableMovement;
            _playerInput.OnDisableInput += DisableMovement;
        }

        private void OnDestroy()
        {
	        _playerInput.OnEnableInput -= EnableMovement;
	        _playerInput.OnDisableInput -= DisableMovement;
        }

        private void Update()
        {
            _rigidbody2D.AddForce(_playerInput.MovementInput * (_movementSpeed * Time.deltaTime));
        }

        private void EnableMovement() => enabled = true;
        
        private void DisableMovement() => enabled = false;
        

        #endregion

    }
}
