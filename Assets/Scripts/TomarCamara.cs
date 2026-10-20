using UnityEngine;

public class TomarCamara : MonoBehaviour, IUsable
{
    [SerializeField] private GameObject camaraObjeto;

    public void Usar()
    {
        camaraObjeto.SetActive(true);
        Debug.Log("Camara activada");
        Destroy(gameObject);
    }
}
