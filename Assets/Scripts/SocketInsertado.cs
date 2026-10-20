using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SocketInsertado : MonoBehaviour
{

    [SerializeField] private GameObject muerte, reloj;
    [SerializeField] private XRGrabInteractable ojos;
    [SerializeField] private Rigidbody ojoRB;

    [SerializeField] private AudioClip ojito;
    [SerializeField] private AudioSource audioSource;
    private bool usado = false;

    public int idCorrecto = 1;
    [SerializeField] private GameObject[] cosos;
    
    public void Insetar(SelectEnterEventArgs args)
    {
        if (usado) return;

        var ojo = args.interactableObject.transform.GetComponentInParent<Marcar>();
        
        if (ojo != null && ojo.id == idCorrecto)
        {
            audioSource.PlayOneShot(ojito);
            muerte.SetActive(false);
            reloj.SetActive(true);
            foreach (var coso in cosos)
            {
                coso.SetActive(true);
            }
            ojoRB.isKinematic = true;
            ojos.enabled = false;
        }
    }

}