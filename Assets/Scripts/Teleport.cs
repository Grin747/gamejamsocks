using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTo;
    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTeleporting = true;
            var player = other.gameObject.GetComponent<PlayerController>();
            if (!player.IsTeleporting)
            {
                var position = teleportTo.transform.position;
                var x = position.x;
                var y = position.y;
                player.transform.position = new Vector2(x, y);
                player.Teleport();
            }
        }
    }
}