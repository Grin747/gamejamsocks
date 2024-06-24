using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Cat : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Meow()
    {
        int randomValue = new Random().Next(0, 9);
        _source.clip = clips[randomValue];
        _source.Play();
    }
}