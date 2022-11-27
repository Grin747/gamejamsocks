// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform center;
    [SerializeField] public float up;

    void LateUpdate()
    {
        var pos = center.position;
        pos.y += up;
        transform.position = pos;
    }
}
