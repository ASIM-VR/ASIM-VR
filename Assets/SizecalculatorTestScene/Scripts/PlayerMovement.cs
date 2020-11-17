using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{

    public float mainSpeed = 10.0f;
    public float shiftAdd = 25.0f;
    public float maxShift = 100.0f;
    public float camSens = 0.15f;

    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun = 1.0f;

    // Update is called once per frame
    void Update()
    {
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        Vector3 direction = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            direction *= totalRun * shiftAdd;
            direction.x = Mathf.Clamp(direction.x, -maxShift, maxShift);
            direction.y = Mathf.Clamp(direction.x, -maxShift, maxShift);
            direction.z = Mathf.Clamp(direction.x, -maxShift, maxShift);
        }
        else 
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            direction *= mainSpeed;
        }

        direction *= Time.deltaTime;
        transform.Translate(direction);
        
    }
        private Vector3 GetBaseInput()
        {
           Vector3 direction = new Vector3();

            // Forward
            if (Input.GetKey(KeyCode.W))
                direction += new Vector3(0, 0, 1);

            if (Input.GetKey(KeyCode.S))
                direction += new Vector3(0, 0, -1);
            
            if (Input.GetKey(KeyCode.A))
                direction += new Vector3(-1, 0, 0);
            
            if (Input.GetKey(KeyCode.D))
                direction += new Vector3(1, 0, 0);

            if (Input.GetKey(KeyCode.Space))
                direction += new Vector3(0, 1, 0);

            if (Input.GetKey(KeyCode.LeftControl))
                direction += new Vector3(0, -1, 0);
            
            return direction;
        }
}
