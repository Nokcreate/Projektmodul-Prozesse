using System.Collections;
using UnityEngine;

public class MatressExpander : MonoBehaviour
{
    public GameObject[] layers = new GameObject[10];
    public float expansionDistance = 0.1f;
    public OVRInput.Button triggerButton = OVRInput.Button.PrimaryIndexTrigger;

    private void Start()
    {
        // Sie müssen diese Zeilen durch den Namen Ihrer GameObjects ersetzen.
        layers[0] = GameObject.Find("AI_Sensor");
        layers[1] = GameObject.Find("Boden_oben_AI_Sensor");
        layers[2] = GameObject.Find("Boden_unten_AI_Sensor");
        layers[3] = GameObject.Find("BodenLuftKissen");
        layers[4] = GameObject.Find("BodenVisko");
        layers[5] = GameObject.Find("Fassung");
        layers[6] = GameObject.Find("Laken");
        layers[7] = GameObject.Find("Luftkissen");
        layers[8] = GameObject.Find("ViskoCube");
        layers[9] = GameObject.Find("WärmeKälteLayer");
    }

    private void Update()
    {
        if (OVRInput.GetDown(triggerButton))
        {
            StartCoroutine(ExpandLayers());
        }
    }

    private IEnumerator ExpandLayers()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            Vector3 newPosition = layers[i].transform.position;
            newPosition.y += i * expansionDistance;
            layers[i].transform.position = newPosition;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
