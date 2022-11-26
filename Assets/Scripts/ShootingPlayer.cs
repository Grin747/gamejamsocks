using UnityEngine;

public class ShootingPlayer : PlayerController
{
    [SerializeField] private Weapon weapon;
    protected override void PlayerAction()
    {
        if (Input.GetButtonDown("Fire1Gamepad"))
        {
            weapon.Shoot();
        }
    }
}
