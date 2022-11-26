using UnityEngine;

public class CollectingPlayer : PlayerController
{
    [SerializeField] private int catCount;
    private bool isNearCat = false;

    protected override void PlayerAction()
    {
        if (Input.GetButtonDown("Fire1") && isNearCat)
        {
            catCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Cat"))
        {
            Debug.Log("cat near");
            isNearCat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Cat"))
        {
            Debug.Log("exit");
            isNearCat = false;
        }
    }
}