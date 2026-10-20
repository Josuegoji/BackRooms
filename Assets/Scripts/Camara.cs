using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using TMPro;

public class Camara : MonoBehaviour
{
    [SerializeField] private InputActionReference TomarFoto;
    [SerializeField] private Camera camara;
    [SerializeField] private TMP_Text fotosRestantes;
    [SerializeField] private GameObject volumen;
    [SerializeField] private GameObject camaritaCanvas;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoCamarita;
    
    public static event Action fotoTomada;
    private bool CamaraActiva = false, UltimoEstadoCamara = false, puedeTomarFoto = true;
    private int numfotos = 3;

    // Update is called once per frame
    void Update()
    {
        fotosRestantes.text = numfotos.ToString();
        
        if (CamaraActiva != UltimoEstadoCamara)
        {
            UltimoEstadoCamara = CamaraActiva;
            if (CamaraActiva)
            {
                volumen.SetActive(true);
                camara.cullingMask |= (1 << LayerMask.NameToLayer("Oculto"));
                camara.cullingMask &= ~(1 << LayerMask.NameToLayer("Falso"));
                camaritaCanvas.SetActive(true);
                camara.fieldOfView = 50;
            }
            else
            {
                volumen.SetActive(false);
                camara.cullingMask &= ~(1 << LayerMask.NameToLayer("Oculto"));
                camara.cullingMask |= (1 << LayerMask.NameToLayer("Falso"));
                camaritaCanvas.SetActive(false);
                camara.fieldOfView = 60;
            }
        }

        if (puedeTomarFoto && CamaraActiva && TomarFoto != null && TomarFoto.action.ReadValue<float>() > .5f && numfotos > 0)
        {
            StartCoroutine(Tomar());
        }
    }


    private IEnumerator Tomar()
    {
        audioSource.PlayOneShot(sonidoCamarita);
        puedeTomarFoto = false;
        numfotos--;
        fotoTomada?.Invoke();
        yield return new WaitForSeconds(1f);
        puedeTomarFoto = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ojos"))
        {
            CamaraActiva = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ojos"))
        {
            CamaraActiva = false;
        }
    }

    public void Recargar()
    {
        numfotos += 3;
    }
}
