using System.Collections;
using UnityEngine;

public class CollectingPlayer : PlayerController
{
    [SerializeField] private int catCount;
    private bool _isNearCat;
    private bool _isNearRocket;
    private GameObject catNear;
    private Cat cat;

    protected override void PlayerAction()
    {
        if (_isNearCat && Input.GetButtonDown("Fire1"))
        {
            catCount++;
            cat = catNear.GetComponent<Cat>();
            cat.Meow();
            StartCoroutine(nameof(TakeCat));
        }

        if (_isNearRocket && Input.GetButtonDown("Fire1"))
        {
            GlobalState.CatsCount += catCount;
            // cat.Meow();
            catCount = 0;
            cat = null;
        }
    }

    private IEnumerator TakeCat()
    {
        yield return new WaitForSeconds(.7f);
        Destroy(catNear);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Cat":
                _isNearCat = true;
                catNear = other.gameObject;
                break;
            case  "Rocket":
                _isNearRocket = true;
                // if(cat != null)
                //     other.gameObject.GetComponent<Cat>().Meow();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Cat":
                _isNearCat = false;
                catNear = null;
                break;
            case "Rocket":
                _isNearRocket = false;
                break;
        }
    }
}