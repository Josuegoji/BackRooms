using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Agua : MonoBehaviour
{
    [SerializeField] private Vector3 alturaOriginal, alturaFinal;
    [SerializeField] private float duracionRespiracion, duracionInundacion;
    public float duracionVaciado;
    private bool ahogandose = false;
    [SerializeField] private AudioClip Llenado, Vaciado;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    public bool vaciando = false;
    
    private Coroutine ahogo;
    
    void Start()
    {
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.Log("ColorAdjustments encontrado");
        }
    }
    
    public void IniciarInundacion()
    {
        StartCoroutine(Inundar());
    }
    
    public void IniciarVaciado()
    {
        StartCoroutine(Vaciar());
    }

    private IEnumerator Inundar()
    {
        audioSource.clip = Llenado;
        audioSource.Play();
        
        float t = 0;
        while (t < duracionInundacion)
        {
            t +=  Time.deltaTime;
            transform.localPosition = Vector3.Lerp(alturaOriginal, alturaFinal, t / duracionInundacion);
            yield return null;
        }
        transform.localPosition = alturaFinal;
        audioSource.Stop();
    }
    
    private IEnumerator Vaciar()
    {
        vaciando = true;
        audioSource.Stop();
        audioSource.clip = Vaciado;
        audioSource.Play();
        float t = 0;
        Vector3 alturaInicio = transform.localPosition;
        while (t < duracionVaciado)
        {
            t +=  Time.deltaTime;
            transform.localPosition = Vector3.Lerp(alturaInicio, alturaOriginal, t / duracionVaciado);
            yield return null;
        }
        transform.localPosition = alturaOriginal;
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") && !ahogandose)
        {
            ahogo = StartCoroutine(Ahogarse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") && ahogandose && vaciando)
        {
            StopCoroutine(ahogo);
            colorAdjustments.colorFilter.Override(Color.white);
        }
    }

    private IEnumerator Ahogarse()
    {
        ahogandose = true;
        yield return new WaitForSeconds(10);
        float t = 0f;
        while (t < duracionRespiracion)
        {
            t += Time.deltaTime;
            Color colorActual = Color.Lerp(Color.white, Color.black, t / duracionRespiracion);
            colorAdjustments.colorFilter.Override(colorActual);
            yield return null;
        }

        colorAdjustments.colorFilter.Override(Color.black);
        SceneManager.LoadScene("Muerte");
    }
}
