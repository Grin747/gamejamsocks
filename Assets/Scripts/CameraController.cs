// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float smoothing = 1000f;

    void LateUpdate()
    {
        Vector3.Lerp(transform.position, center.position, smoothing);
    }
}
