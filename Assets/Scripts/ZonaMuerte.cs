using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaMuerte : MonoBehaviour
{
    [SerializeField] private GameObject Ojo;
    [SerializeField] private Vector3 posicion, rotacion;
    [SerializeField] private bool detectarOjo = false, matar = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !detectarOjo)
        {
            SceneManager.LoadScene("Muerte");
        }else if(other.gameObject.CompareTag("Player") && !detectarOjo && matar)
        {
            SceneManager.LoadScene("Muerte");
        }
        else if(other.gameObject.CompareTag("ELOjo") && detectarOjo)
        {
            Destroy(other.gameObject);
            Instantiate(Ojo, posicion, Quaternion.Euler(rotacion));
        }
        
    }
}
