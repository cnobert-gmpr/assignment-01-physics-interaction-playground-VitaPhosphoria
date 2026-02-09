using System.Collections;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float _bumperForce = 50f, _litDuration = 0.2f;
    [SerializeField] private Color _litColor = Color.pink;

    private bool _isLit = false;
    private Color _originalColor;
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ball"))
        {
            #region Apply Force
            Vector2 normal = Vector2.zero;
            if(collision.rigidbody != null)
            {
                if(collision.contactCount > 0)
                {
                    ContactPoint2D contact = collision.GetContact(0);
                    normal = contact.normal;
                }
                else if(normal == Vector2.zero)
                {
                   Vector2 direction = (collision.rigidbody.position - (Vector2)transform.position).normalized;
                   normal = direction;
                }
                Vector2 impulse = -normal * _bumperForce;
                collision.rigidbody.AddForce(impulse, ForceMode2D.Impulse);
                
            }
            #endregion

            if(!_isLit)
            {
                StartCoroutine(LightUp());
            }
        }
    }

    private IEnumerator LightUp()
    {
        _isLit = true;
        _spriteRenderer.color = _litColor;
        yield return new WaitForSeconds(_litDuration);
        _spriteRenderer.color = _originalColor;
        _isLit = false;
    }
}
