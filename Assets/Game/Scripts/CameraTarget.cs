using UnityEngine;

namespace Urapirat.Camera
{
    public class CameraTarget : MonoBehaviour
    {
        private enum Orientation
        {
            X, Y, Z
        }

        private GameObject atachedObject;

        [SerializeField][Range(0, 20)] private float smothTime = 5;
        [SerializeField] private float cameraHeight = 10;
        [SerializeField] private Orientation orientation = Orientation.X;


        private void Awake()
        {
            atachedObject = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 finalPosition = Vector3.SmoothDamp(transform.position, atachedObject.transform.position, ref velocity, smothTime * Time.deltaTime);

            switch (orientation)
            {
                case Orientation.X:
                    finalPosition.x = -cameraHeight; break;
                case Orientation.Y:
                    finalPosition.y = -cameraHeight; break;
                default:
                    finalPosition.z = -cameraHeight; break;
            }

            transform.position = finalPosition;
        }

    }
}