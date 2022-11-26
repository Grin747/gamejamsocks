using UnityEngine;

public class CollectingPlayer : PlayerController
{
    protected override void PlayerAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("collect");
        }
    }
}
