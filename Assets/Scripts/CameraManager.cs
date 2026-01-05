using UnityEngine;

namespace ChessGame
{
    public class CameraManager : MonoBehaviour
    {
        public Camera cam1;
        public Camera cam2;
        public float cornerSize = 0.15f; 

        void Start()
        {
            if (cam1 != null) Switch(cam1);
        }

        void Update()
        {
            // Check if user clicked the corner
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x >= Screen.width * (1 - cornerSize) && 
                    Input.mousePosition.y >= Screen.height * (1 - cornerSize))
                {
                    if (cam1.gameObject.activeSelf) Switch(cam2);
                    else Switch(cam1);
                }
            }
        }

        void Switch(Camera c)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(false);
            
            c.gameObject.SetActive(true);
            
            // Fix audio listener
            if (cam1.GetComponent<AudioListener>()) cam1.GetComponent<AudioListener>().enabled = (c == cam1);
            if (cam2.GetComponent<AudioListener>()) cam2.GetComponent<AudioListener>().enabled = (c == cam2);
        }
    }
}
