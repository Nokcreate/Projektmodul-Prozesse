using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct LayerInfo
{
    public GameObject layerObject;
    public string layerInformation;
}

public class MatressExpander : MonoBehaviour
{
    public LayerInfo[] layers;
    public GameObject infoPanel;
    public Text infoText;
    public float expansionDistance = 0.2f;
    public OVRInput.Button triggerButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Button selectButton = OVRInput.Button.PrimaryHandTrigger;
    public float moveSpeed = 1.0f;
    public Vector3 boundsMin = new Vector3(-5.0f, 0.0f, -5.0f);
    public Vector3 boundsMax = new Vector3(5.0f, 5.0f, 5.0f);

    private bool isExpanded = false;
    private Vector3[] originalPositions;
    private LayerInfo selectedLayer;
    private Color originalColor;

    private void Start()
    {
        originalPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            originalPositions[i] = layers[i].layerObject.transform.position;
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(triggerButton, OVRInput.Controller.Active) || Input.GetMouseButtonDown(0))
        {
            isExpanded = !isExpanded;
            StartCoroutine(isExpanded ? ExpandLayers() : ContractLayers());
        }

        // Layer selection and deselection only when the mattress is expanded
        if (isExpanded)
        {
            if (OVRInput.GetDown(selectButton, OVRInput.Controller.Active) || Input.GetMouseButtonDown(1))
            {
                SelectLayer();
            }

            if ((OVRInput.GetUp(selectButton, OVRInput.Controller.Active) && selectedLayer.layerObject != null) || Input.GetMouseButtonUp(1))
            {
                DeselectLayer();
            }

            if (OVRInput.Get(selectButton, OVRInput.Controller.Active) && selectedLayer.layerObject != null)
            {
                MoveSelectedLayer();
            }
        }
    }


    private void SelectLayer()
    {
        Ray ray;
        float rayStartDistance = 0.05f;  // To start the ray a little bit in front of the controller
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch) && OVRInput.Get(selectButton))
        {
            var controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            var controllerForward = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            ray = new Ray(controllerPosition + controllerForward * rayStartDistance, controllerForward);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out var hit))
        {
            SelectLayerBasedOnHit(hit);
        }
    }

    private void MoveSelectedLayer()
    {
        Vector3 delta;

        if (OVRInput.Get(selectButton))
        {
            // Get controller velocity for VR
            Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            delta = new Vector3(controllerVelocity.x, controllerVelocity.y, controllerVelocity.z);
        }
        else
        {
            // Get mouse position for non-VR
            delta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }

        Vector3 newPosition = selectedLayer.layerObject.transform.position + delta * Time.deltaTime * moveSpeed;

        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, boundsMin.x, boundsMax.x),
            Mathf.Clamp(newPosition.y, boundsMin.y, boundsMax.y),
            Mathf.Clamp(newPosition.z, boundsMin.z, boundsMax.z)
        );

        selectedLayer.layerObject.transform.position = newPosition;
    }



    private void SelectLayerBasedOnHit(RaycastHit hit)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (hit.collider.gameObject == layers[i].layerObject)
            {
                if (selectedLayer.layerObject != null)
                {
                    selectedLayer.layerObject.GetComponent<Renderer>().material.color = originalColor;
                }

                selectedLayer = layers[i];
                originalColor = selectedLayer.layerObject.GetComponent<Renderer>().material.color;

                selectedLayer.layerObject.GetComponent<Renderer>().material.color = Color.green;

                infoPanel.SetActive(true);
                infoText.text = "Informationen über die Schicht: " + selectedLayer.layerInformation;
                break;
            }
        }
    }


    private void DeselectLayer()
    {
        selectedLayer.layerObject.GetComponent<Renderer>().material.color = originalColor;
        selectedLayer = default;
    }

    private IEnumerator ExpandLayers()
    {
        for (float f = 0; f <= 1; f += Time.deltaTime)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                Vector3 newPosition = originalPositions[i] + new Vector3(0, i * expansionDistance, 0) * f;
                layers[i].layerObject.transform.position = newPosition;
            }

            yield return null;
        }
    }

    private IEnumerator ContractLayers()
    {
        for (float f = 0; f <= 1; f += Time.deltaTime)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                Vector3 newPosition = Vector3.Lerp(originalPositions[i] + new Vector3(0, i * expansionDistance, 0), originalPositions[i], f);
                layers[i].layerObject.transform.position = newPosition;
            }

            yield return null;
        }
    }
}
