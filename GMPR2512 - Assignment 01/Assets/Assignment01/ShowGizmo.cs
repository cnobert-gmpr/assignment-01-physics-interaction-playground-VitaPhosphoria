using UnityEngine;

namespace Assignment01
{
    public class ShowGizmo : MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor = Color.red;
        [SerializeField] private float _radius = 0.5f;

        void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}