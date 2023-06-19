using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;  // Geschwindigkeit der Kameradrehung

    private float yaw = 0.0f;  // Horizontal rotation
    private float pitch = 0.0f;  // Vertical rotation

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Input.GetKey(KeyCode.Space))
        {
            movement.y += 1;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            movement.y -= 1;
        }

        transform.position = transform.position + movement * speed * Time.deltaTime;

        // Hinzufügen der Kameradrehung per Rechtsklick
        if (Input.GetMouseButton(1))  // 1 steht für die rechte Maustaste
        {
            yaw += rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= rotationSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
