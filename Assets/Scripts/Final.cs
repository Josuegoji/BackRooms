using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    [SerializeField] private GameObject malo, camara;
    [SerializeField] private TrackedPoseDriver trackedPoseCamara, trackedPoseIzq, trackedPoseDer;
    [SerializeField] private GameObject[] maniquies;
    [SerializeField] private BoxCollider puertaPoner;
    [SerializeField] private Renderer renderPoner;
    [SerializeField] private float velocidad = 2;
    [SerializeField] private Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    [SerializeField] private AudioSource audi;
    [SerializeField] private GameObject locomotion;
    private bool coso = false;

    void Start()
    {
        renderPoner.material.SetFloat("_Progreso", 1);
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.Log("ColorAdjustments encontrado");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!coso)
            {
                coso = true;
                StartCoroutine(CerrarPuerta());
            }
            
        }

    }
    
    private IEnumerator CerrarPuerta()
    {
        locomotion.SetActive((false));
        trackedPoseCamara.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        trackedPoseDer.enabled = false;
        trackedPoseIzq.enabled = false;
        camara.SetActive(false);
        
        yield return new WaitForSeconds(1);
        
        float t = 0;
        while (t <= 1)
        {
            t +=  Time.deltaTime/velocidad;
            
            renderPoner.material.SetFloat("_Progreso", 1-t);
            yield return null;   
        }
        renderPoner.material.SetFloat("_Progreso", 0);
        puertaPoner.enabled = true;
        
        yield return new WaitForSeconds(2);

        colorAdjustments.colorFilter.Override(Color.black);

        foreach (var maniquie in maniquies)
        {
            maniquie.SetActive(true);
        }
        
        malo.SetActive(true);
        
        yield return new WaitForSeconds(5);
        
        colorAdjustments.colorFilter.Override(Color.white);
        
        yield return new WaitForSeconds(4);
        
        audi.Play();
        malo.GetComponent<Animator>().enabled = true;
        
        yield return new WaitForSeconds(23);
        SceneManager.LoadScene("Fin");

    }
}
