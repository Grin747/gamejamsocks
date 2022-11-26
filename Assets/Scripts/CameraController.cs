// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float smoothing = 10f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, center.position, Time.deltaTime * smoothing);
    }
}
