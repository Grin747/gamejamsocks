// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform center;

    void LateUpdate()
    {
        transform.position = center.position;
    }
}
