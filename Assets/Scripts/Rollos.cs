using System;
using UnityEngine;

public class Rollos : MonoBehaviour
{


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Camarita"))
        {
            other.gameObject.GetComponent<Camara>().Recargar();
            Destroy(gameObject);
        }
    }
}
