using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MatressExpander : MonoBehaviour
{
    public GameObject[] layers = new GameObject[10];
    public float expansionDistance = 0.2f;
    public OVRInput.Button triggerButton = OVRInput.Button.PrimaryIndexTrigger;
    public float moveSpeed = 1.0f; // Geschwindigkeit, mit der sich die Schichten bewegen
    private bool isExpanded = false;
    private Vector3[] originalPositions; // speichert die ursprünglichen Positionen der Schichten

    private void Start()
    {
        originalPositions = new Vector3[layers.Length];
        // Ersetzen Sie diese Zeilen durch den Namen Ihrer GameObjects.
        layers[0] = GameObject.Find("Laken");        
        layers[1] = GameObject.Find("Boden_oben_AI_Sensor");
        layers[2] = GameObject.Find("AI_Sensor");
        layers[3] = GameObject.Find("Boden_unten_AI_Sensor");
        layers[4] = GameObject.Find("BodenLuftKissen");
        layers[5] = GameObject.Find("BodenVisko");
        layers[6] = GameObject.Find("Luftkissen");
        layers[7] = GameObject.Find("ViskoCube");
        layers[8] = GameObject.Find("WärmeKälteLayer");

        // Speichert die ursprünglichen Positionen der Schichten
        for (int i = 0; i < layers.Length; i++)
        {
            originalPositions[i] = layers[i].transform.position;
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(triggerButton) || Input.GetMouseButtonDown(0))
        {
            if (isExpanded)
            {
                StartCoroutine(ContractLayers());
                isExpanded = false;
            }
            else
            {
                StartCoroutine(ExpandLayers());
                isExpanded = true;
            }
        }
    }


    private IEnumerator ExpandLayers()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            Vector3 targetPosition = originalPositions[i] + new Vector3(0, (layers.Length - 1 - i) * expansionDistance, 0);
            yield return StartCoroutine(MoveToTarget(layers[i].transform, targetPosition));
        }
    }


    private IEnumerator ContractLayers()
    {
        for (int i = layers.Length - 1; i >= 0; i--)
        {
            Vector3 targetPosition = originalPositions[i];
            yield return StartCoroutine(MoveToTarget(layers[i].transform, targetPosition));
        }
    }

    private IEnumerator MoveToTarget(Transform transform, Vector3 targetPosition)
    {
        while ((transform.position - targetPosition).magnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // um sicherzustellen, dass das Ziel exakt erreicht wird
    }
}
