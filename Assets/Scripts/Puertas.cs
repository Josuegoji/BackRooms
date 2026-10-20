using System.Collections;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    [SerializeField] private BoxCollider puertaQuitar;
    [SerializeField] private Renderer renderQuitar, renderPoner;
    private Coroutine cambio;
    [SerializeField] private float velocidad = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderPoner.material.SetFloat("_Progreso", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Si");
            cambio ??= StartCoroutine(cambiar());
        }
    }

    private IEnumerator cambiar()
    {
        float t = 0;

        while (t <= 1)
        {
            t +=  Time.deltaTime/velocidad;
            
            renderPoner.material.SetFloat("_Progreso", 1 - t);
            yield return null;   
        }
        renderPoner.material.SetFloat("_Progreso", 0);

        t = 0;
        while (t <= 1)
        {
            t +=  Time.deltaTime/velocidad;
            
            renderQuitar.material.SetFloat("_Progreso", t);
            yield return null;   
        }
        renderQuitar.material.SetFloat("_Progreso", 1);
        
        puertaQuitar.enabled = false;
        
        gameObject.SetActive(false);
        yield return null;
        
        
    }
}
