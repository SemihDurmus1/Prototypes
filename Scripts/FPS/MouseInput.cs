using UnityEngine;

public class MouseInput : MonoBehaviour
{

    public Transform player;
    public float mouseSens = 200f;
    private float xRotation;

    public GameObject panelPause;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }
    void Start()
    {
        
    }



    void Update()
    {
        float mouseXpos = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseYpos = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseYpos;

        xRotation = Mathf.Clamp(xRotation, -70f, +80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        

        player.Rotate(Vector3.up * mouseXpos);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelPause.active)
            {
                panelPause.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (panelPause.active == false)
            {
                Cursor.lockState = CursorLockMode.None;
                panelPause.SetActive(true);
            }
        }
    }
}
