 using System.Collections;
 using UnityEngine;
using TMPro;
public class PanelDeControl : MonoBehaviour
{
    [SerializeField] private TMP_Text[] digitos;
    [SerializeField] private int contrasenia;
    private int posicionActual = 0;
    [SerializeField] private BoxCollider puertaQuitar;
    [SerializeField] private Renderer renderQuitar;
    [SerializeField] private float velocidad = 2;
    [SerializeField] private Agua aguita;
    private float cooldown = 1;
    private bool bloqueado = false;
    [SerializeField] private AudioClip boton;
    [SerializeField] private AudioSource audioSource;

    public void PonerDigito(GameObject botonNumero)
    {
        audioSource.PlayOneShot(boton);
        if(bloqueado) return;
        bloqueado = true;
        Invoke(nameof(Desbloquear), cooldown);
        digitos[posicionActual].text = botonNumero.GetComponentInChildren<TMP_Text>().text;
        posicionActual++;
        if(posicionActual >= digitos.Length) ComprabarContrasenia();
    }

    private void Desbloquear()
    {
        bloqueado = false;
    }

    private void ComprabarContrasenia()
    {
        string textoUnido = "";
        foreach (var digito in digitos)
        {
            textoUnido += digito.text;
        }
        
        int intento = int.Parse(textoUnido);

        if (intento == contrasenia)
        {
            StartCoroutine(AbrirPuerta());
        }
        else
        {
            Reiniciar();
        }
    }
    
    
    private IEnumerator AbrirPuerta()
    {
        aguita.IniciarVaciado();
        yield return new WaitForSeconds(aguita.duracionVaciado);
        
        float t = 0;
        while (t <= 1)
        {
            t +=  Time.deltaTime/velocidad;
            
            renderQuitar.material.SetFloat("_Progreso", t);
            yield return null;   
        }
        renderQuitar.material.SetFloat("_Progreso", 1);

        puertaQuitar.enabled = false;
        this.enabled = false;
    }

    private void Reiniciar()
    {
        foreach (var digito in digitos)
        {
            digito.text = "";
        }
        posicionActual = 0;
    }
}
