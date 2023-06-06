using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            this.transform.position = this.transform.position + new Vector3(0, 1, 0);

            // Create a ray from the mouse cursor on screen in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // If we hit this object, then move it up
                if (hit.transform == this.transform)
                {
                    this.transform.position = this.transform.position + new Vector3(0, 1, 0);
                }
            }
        }
    }
}
