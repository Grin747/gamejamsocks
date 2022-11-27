using UnityEngine;

public class CollectingPlayer : PlayerController
{
    [SerializeField] private int catCount;
    private bool _isNearCat;
    private bool _isNearRocket;
    private GameObject catNear;

    protected override void PlayerAction()
    {
        if (_isNearCat && Input.GetButtonDown("Fire1"))
        {
            catCount++;
            Destroy(catNear);
        }

        if (_isNearRocket && Input.GetButtonDown("Fire1"))
        {
            GlobalState.CatsCount += catCount;
            catCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Cat":
                Debug.Log("cat near");
                _isNearCat = true;
                catNear = other.gameObject;
                break;
            case  "Rocket":
                _isNearRocket = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Cat":
                Debug.Log("exit");
                _isNearCat = false;
                catNear = null;
                break;
            case "Rocket":
                _isNearRocket = false;
                break;
        }
    }
}