// ReSharper disable ArrangeTypeMemberModifiers
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float smoothing;

    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, center.position+new Vector3(5,5), Time.deltaTime * smoothing);
    }
}