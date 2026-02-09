using UnityEngine;

namespace Assignment01
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Portal _enterPortal, _exitPortal;
        [SerializeField] private float _velocityPreservation = 1f;
        [SerializeField] private bool _preserveVelocityDirection = true;
        [SerializeField] private float _transferOffset = 0.5f;
        [SerializeField] private float _transferCooldown = 0.5f;

        private float _lastTransferTime = -1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"[Portal] OnTriggerEnter2D called on {gameObject.name}, colliding with {collision.gameObject.name}");
            
            if (_exitPortal == null)
            {
                Debug.Log($"[Portal] {gameObject.name} is an EXIT portal - not transferring ball");
                return;
            }
            
            if (collision.CompareTag("Ball"))
            {
                if (Time.time - _lastTransferTime < _transferCooldown)
                {
                    Debug.Log($"[Portal] Ball still in cooldown. Skipping transfer.");
                    return;
                }

                Debug.Log($"[Portal] Ball detected! Transferring to exit portal...");
                TransferBallToExit(collision);
            }
        }

        private void TransferBallToExit(Collider2D ballCollider)
        {
            Rigidbody2D ballRB = ballCollider.GetComponent<Rigidbody2D>();
            if (ballRB == null)
            {
                Debug.LogError("[Portal] Ball has no Rigidbody2D!");
                return;
            }

            Debug.Log($"[Portal] === TRANSFERRING BALL from {gameObject.name} to {_exitPortal.gameObject.name} ===");
            Debug.Log($"[Portal] Ball original position: {ballCollider.transform.position}");
            Debug.Log($"[Portal] Ball original velocity: {ballRB.linearVelocity}");
            
            Vector2 originalVelocity = ballRB.linearVelocity;

            Vector2 exitDirection = _exitPortal.transform.up;
            Vector3 newPosition = _exitPortal.transform.position + (Vector3)(exitDirection * _transferOffset);
            ballCollider.transform.position = newPosition;
            
            Debug.Log($"[Portal] Ball new position: {newPosition}");
            Debug.Log($"[Portal] Exit direction: {exitDirection}");

            if (_preserveVelocityDirection)
            {
                Vector2 newVelocity = originalVelocity * _velocityPreservation;
                ballRB.linearVelocity = newVelocity;
                Debug.Log($"[Portal] New velocity (preserved direction): {newVelocity}");
            }
            else
            {
                float speed = originalVelocity.magnitude * _velocityPreservation;
                ballRB.linearVelocity = exitDirection * speed;
                Debug.Log($"[Portal] New velocity (exit direction): {ballRB.linearVelocity}");
            }

            ballRB.angularVelocity = ballRB.angularVelocity * _velocityPreservation;
            
            // Set cooldown on exit portal to prevent immediate re-entry
            if (_exitPortal != null)
            {
                _exitPortal._lastTransferTime = Time.time;
                Debug.Log($"[Portal] Set cooldown on exit portal {_exitPortal.gameObject.name}");
            }
            
            Debug.Log($"[Portal] === TRANSFER COMPLETE ===");
        }
    }
}