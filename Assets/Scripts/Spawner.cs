using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
// ReSharper disable ArrangeTypeMemberModifiers

public class Spawner : MonoBehaviour
{
    private float _timerDelay = 10;
    private bool _isVisible;
    
    [SerializeField] private int maxEntity;
    [SerializeField] private GameObject entity;
    [SerializeField] private Transform leftBar;
    [SerializeField] private Transform rightBar;

    void LateUpdate()
    {
        if (_timerDelay > 0)
        {
            _timerDelay -= Time.deltaTime;
        }
        else
        {
            var entities = FindObjectsOfType<EnemyController>()
                .Count(x =>
                {
                    var tx = x.transform.position.x;
                    return tx > leftBar.position.x &&
                           tx < rightBar.position.x;
                });
            
            if (entities <= maxEntity && !_isVisible)
            {
                var controller = entity.GetComponent<EnemyController>();
                controller.leftBar = leftBar;
                controller.rightBar = rightBar;
                _timerDelay = 10;
                Instantiate(entity, transform.position, transform.rotation);
            }
        }
    }

    void OnBecameVisible()
    {
        _isVisible = true;
    }

    void OnBecameInvisible()
    {
        _isVisible = false;
    }
}