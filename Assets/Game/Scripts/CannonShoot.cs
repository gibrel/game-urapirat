using UnityEngine;

namespace Urapirat.Combat
{
    public class CannonShoot : MonoBehaviour
    {
        [SerializeField] private GameObject cannonBall;
        [SerializeField] private float cannonBallInpulse = 20f;
        [SerializeField] private float timeBetweenShots = 2f;
        [SerializeField] private float maximunShootDistance = 10f;
        [SerializeField] private int cannonGroup = 0;
        [SerializeField] private bool isPlayer = false;

        private float lastShootTime;
        private Transform playerTransform;

        public bool IsPlayer { get { return isPlayer; } }
        public int CannonGroup { get { return cannonGroup; } }

        private void Awake()
        {
            lastShootTime = -timeBetweenShots;

            if (!isPlayer)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                playerTransform = player.transform;
            }
        }

        public void Shoot()
        {
            if (Time.time >= lastShootTime + timeBetweenShots)
            {
                lastShootTime = Time.time;

                if (isPlayer)
                {
                    var obj = Instantiate(cannonBall, transform.position, Quaternion.identity);
                    obj.GetComponent<Rigidbody2D>().AddForce(cannonBallInpulse * transform.up);

                    PlaySound();
                }
                else
                {
                    var distance = playerTransform.position - transform.position;
                    if (Mathf.Abs(distance.magnitude) <= maximunShootDistance)
                    {
                        var direction = distance.normalized;

                        var obj = Instantiate(cannonBall, transform.position, Quaternion.identity);
                        obj.GetComponent<Rigidbody2D>().AddForce(cannonBallInpulse * new Vector2(direction.x, direction.y));

                        PlaySound();
                    }
                }
            }
        }

        private void PlaySound()
        {
            SoundManager.PlaySound(SoundManager.Sound.CannonFire, transform.position);
        }
    }
}
