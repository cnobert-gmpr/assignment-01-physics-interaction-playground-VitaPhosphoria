using UnityEngine;
using System.Collections;

namespace Assignment01
{
    public class FreezeField : MonoBehaviour
    {
        [SerializeField] private float freezeDuration = 2f;
        private Collider2D triggerCollider;

        private void Start()
        {
            triggerCollider = GetComponent<Collider2D>();
            if (triggerCollider != null)
            {
                triggerCollider.isTrigger = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    StartCoroutine(FreezePhysics(rb));
                }
            }
        }

        private IEnumerator FreezePhysics(Rigidbody2D rb)
        {
            // Store original physics state
            Vector2 originalVelocity = rb.linearVelocity;
            float originalAngularVelocity = rb.angularVelocity;
            float originalGravityScale = rb.gravityScale;
            RigidbodyConstraints2D originalConstraints = rb.constraints;

            // Freeze the rigidbody
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // Wait for freeze duration
            yield return new WaitForSeconds(freezeDuration);

            // Resume normal physics with original velocity
            rb.constraints = originalConstraints;
            rb.gravityScale = originalGravityScale;
            rb.linearVelocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
    }
}