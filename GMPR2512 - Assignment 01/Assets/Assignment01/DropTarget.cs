using UnityEngine;

namespace Assignment01
{
    public class DropTarget : MonoBehaviour
    {
        [SerializeField] private Color _hitColor = Color.seaGreen;
        [SerializeField] private float _hideDelay = 0.1f, _resetDelay = 2f;

        private bool _isDown = false;
        private SpriteRenderer _renderer;
        private Color _originalColor;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _originalColor = _renderer.color;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(!_isDown && collision.gameObject.CompareTag("Ball"))
            {
                _isDown = true;
                _renderer.color = _hitColor;
                Invoke(nameof(HideTarget), _hideDelay);
            }
        }
        void HideTarget()
        {
            gameObject.SetActive(false);
            Invoke(nameof(ResetTarget), _resetDelay);
        }
        void ResetTarget()
        {
            _renderer.color = _originalColor;
            gameObject.SetActive(true);
            _isDown = false;
        }
    }
}
