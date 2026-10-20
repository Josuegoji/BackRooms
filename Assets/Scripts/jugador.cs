using System;
using UnityEngine;

public class jugador : MonoBehaviour
{
    [SerializeField] private Transform cabeza;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip SonidoNormal, SonidoRnAgua;
    
    [SerializeField] private float alturaMin = .5f, alturaMax = 2f;

    private Vector3 ultimaPosicion;
    private float velocidad, minimaVelocidad = .2f;
    private bool repodruciendose;

    [SerializeField] private Agua aguita;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ultimaPosicion = cabeza.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        capsuleCollider.height = Mathf.Clamp(cabeza.localPosition.y, alturaMin, alturaMax);
        capsuleCollider.center = new Vector3(cabeza.localPosition.x, capsuleCollider.height/2, cabeza.localPosition.z);
        
        float movimiento = Vector3.Distance(cabeza.localPosition, ultimaPosicion);
        velocidad = movimiento / Time.deltaTime;

        if (velocidad > minimaVelocidad)
        {
            if (!_audioSource.isPlaying) 
            {
                _audioSource.Play();
                repodruciendose = true; 
            }
        }
        else
        {
            if (_audioSource.isPlaying) 
            {
                _audioSource.Pause();
                repodruciendose = false; 
            }
        }

        ultimaPosicion = cabeza.localPosition;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agua"))
        {
            _audioSource.clip = SonidoRnAgua;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agua") && aguita.vaciando)
        {
            _audioSource.clip = SonidoNormal;
        }
    }
}
