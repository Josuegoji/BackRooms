using UnityEngine;

public class BotonElevador : MonoBehaviour, IUsable
{
    [SerializeField] private AudioSource audioSource;
    private bool usado = false;
    public void Usar()
    {
        if (usado) return;
        usado = true;
        audioSource.Play();
    }
}
