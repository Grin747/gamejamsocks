using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip clip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1Gamepad"))
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
