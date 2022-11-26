using UnityEngine;

public class ShootingPlayer : PlayerController
{
    [SerializeField] private Weapon _weapon;
    protected override void PlayerAction()
    {
        if (Input.GetButtonDown("Fire1Gamepad"))
        {
            _weapon.Shoot();
        }
    }
}
