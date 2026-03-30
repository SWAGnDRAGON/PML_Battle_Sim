using UnityEngine;
using UnityEngine.Events;

public class SpriteBillboarding : MonoBehaviour
{
    //Player Camera Ref
    private Camera mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mainCam != null)
        {
            // Create a rotation that only considers the camera's Y rotation
            transform.rotation = Quaternion.Euler(0f, mainCam.transform.rotation.eulerAngles.y, 0f);
        }
    }

}
