// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float smoothing;

    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, center.position, Time.deltaTime * smoothing);
    }
}
