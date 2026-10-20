using System;
using System.Collections;
using UnityEngine;

public class SalaAgua : MonoBehaviour
{
    [SerializeField] private Agua aguita;
    [SerializeField] private BoxCollider puertaPoner;
    [SerializeField] private Renderer renderPoner;
    [SerializeField] private float velocidad = 2;
    private bool activado = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderPoner.material.SetFloat("_Progreso", 1);
    }

    private IEnumerator CerrarPuerta()
    {
        float t = 0;
        while (t <= 1)
        {
            t +=  Time.deltaTime/velocidad;
            
            renderPoner.material.SetFloat("_Progreso", 1-t);
            yield return null;   
        }
        renderPoner.material.SetFloat("_Progreso", 0);
        puertaPoner.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !activado)
        {
            activado = true;
            StartCoroutine(CerrarPuerta());
            aguita.IniciarInundacion();
        }
    }
}
