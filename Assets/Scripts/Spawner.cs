using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject entity;
    private float _timerDelay = 10;
    [SerializeField] private int maxEntity;

    private void LateUpdate()
    {
        if (_timerDelay > 0)
        {
            _timerDelay -= Time.deltaTime;
        }
        else
        {
            var entities = FindObjectsOfType<EnemyController>();
            if (entities.Length <= maxEntity)
            {
                _timerDelay = 10;
                Instantiate(entity, transform.position, transform.rotation);
            }
        }
    }
}