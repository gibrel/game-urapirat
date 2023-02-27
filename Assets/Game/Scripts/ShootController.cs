using UnityEngine;
using Urapirat.Combat;

public class ShootController : MonoBehaviour
{
    [SerializeField] private CannonShoot[] cannons;

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(PauseMenu.GameIsPaused) return;

        foreach (CannonShoot cannon in cannons)
        {
            if (cannon.IsPlayer)
            {
                if (Input.GetMouseButton(cannon.CannonGroup))
                {
                    cannon.Shoot();
                }
            }
            else
            {
                cannon.Shoot();
            }
        }
    }
}
