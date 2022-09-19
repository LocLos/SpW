using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _fallowTarget;
    private Vector3 offset = new Vector3(1, 1, -10);

    private void LateUpdate()
    {
        if (_fallowTarget == null)
            return;

        transform.position = _fallowTarget.position + offset;
    }

    public void SetTarget(Transform target) =>
        _fallowTarget = target;
}
