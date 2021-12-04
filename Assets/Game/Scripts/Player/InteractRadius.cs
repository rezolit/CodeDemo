using System.Collections.Generic;
using Ineraction;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractRadius : MonoBehaviour
    {

        #region Fields & Properties

        public IInteractable NearestInteractableObject
        {
            get {
                if (_nearInteractableObjects.Count == 0) {
                    return null;
                }
                var nearest = _nearInteractableObjects[0];
                float minDistance = Vector3.Distance(transform.position, nearest.transform.position);

                foreach (var @object in _nearInteractableObjects) {
                    var distance = Vector3.Distance(transform.position, @object.transform.position);
                    if (distance < minDistance) {
                        nearest = @object;
                        minDistance = distance;
                    }
                }
                return nearest.GetComponent<IInteractable>();
            }
        }

        [SerializeField] [Range(0.0f, 5.0f)] 
        private float _triggerRadius;

        private List<GameObject> _nearInteractableObjects;

        #endregion

        #region Methods

        private void Start()
        {
            _nearInteractableObjects = new List<GameObject>();
        }

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = _triggerRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
	        if (collision.TryGetComponent(out IInteractable interactable)) {
				_nearInteractableObjects.Add(collision.gameObject);
				interactable.ShowHint();
	        }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
	        if (collision.TryGetComponent(out IInteractable interactable)) {
		        _nearInteractableObjects.Remove(collision.gameObject);
		        interactable.HideHint();
	        }
        }

        #endregion
    }
}
