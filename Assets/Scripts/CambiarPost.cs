using System;
using UnityEngine;

public class CambiarPost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agua"))
        {
            RenderSettings.fog = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agua"))
        {
            RenderSettings.fog = false;
        }
    }
}
