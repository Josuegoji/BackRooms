using UnityEngine;

public class Marcar : MonoBehaviour
{
    public int id;
    
    [SerializeField] private AudioClip ojito;
    [SerializeField] private AudioSource audioSource;

    public void sonidito()
    {
        audioSource.PlayOneShot(ojito);
    }
}