using System;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    [SerializeField] private int numColisiones = 1;
    private int colisiones = 0;
    [SerializeField] private Vector2 posicionDeTeletransporte;
    private Vector2 offset;
    [SerializeField] private bool reinicio;
    
    public static event Action Reiniciado;
    
    private void OnEnable()
    {
        Reiniciado += Reiniciar;
    }

    private void OnDisable()
    {
        Reiniciado -= Reiniciar;
    }
    
    private void Reiniciar()
    {
        colisiones = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Si");
            colisiones++;
            if (colisiones >= numColisiones)
            {
                if(reinicio) Reiniciado?.Invoke();
                Teletransportar(other.GetComponentInParent<Transform>().parent);
            }
        }
    }

    private void Teletransportar(Transform jugador)
    {
        Debug.Log("Teletransportar");
        offset = new Vector2(jugador.position.x, jugador.position.z) - new Vector2(transform.position.x, transform.position.z);
        jugador.position = new Vector3(posicionDeTeletransporte.x+offset.x, jugador.position.y, posicionDeTeletransporte.y+offset.y);
    }
}
