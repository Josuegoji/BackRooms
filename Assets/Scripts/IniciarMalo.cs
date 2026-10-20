using System;
using TMPro;
using UnityEngine;

public class IniciarMalo : MonoBehaviour
{
    [SerializeField] private GameObject malito;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            malito.SetActive(true);
        }
    }
}
