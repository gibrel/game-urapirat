using UnityEngine;

namespace Urapirat.Movement
{
    public class ShipOverWaves : MonoBehaviour
    {
        [SerializeField] private float period = 3.0f;
        [SerializeField] private float angleWiggle = 30f;

        private void Update()
        {
            float angluarVelocity = 2 * Mathf.PI / period;
            transform.Rotate(Vector3.up, angleWiggle * Time.deltaTime * Mathf.Sin(angluarVelocity * Time.time));
        }
    }
}
