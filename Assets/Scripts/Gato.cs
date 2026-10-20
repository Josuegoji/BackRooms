using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;
    

public class Gato : MonoBehaviour
{

    private Transform principal;
    [SerializeField] private Material glitch, normal;
    private NavMeshAgent agent;
    [SerializeField] private float tiempoDeAturdicion = 5f;
    [SerializeField] private float fov = 90, rangoFoto = 50;
    [SerializeField] private LayerMask cosoLayer;
    private float tiempoAturdido = 0;
    private Coroutine Aturdido;
    private SkinnedMeshRenderer skin;
    
    
    private void Start()
    {
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        if (Camera.main != null) principal = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(principal.position);
    }

    private void OnEnable()
    {
        Camara.fotoTomada += FotoTomada;
    }

    private void OnDisable()
    {
        Camara.fotoTomada -= FotoTomada;
    }

    void FotoTomada()
    {
        Vector3 direccion = (transform.position - principal.position).normalized;
        float prodPunto = Vector3.Dot(principal.forward, direccion);
        float angulo = Mathf.Cos((fov / 2f) * Mathf.Deg2Rad);
        
        float distancia = Vector3.Distance(principal.position, transform.position);
        
        if (prodPunto >= angulo)
        {
            if (distancia <= rangoFoto)
            {
                RaycastHit hit;
                if (Physics.Raycast(principal.position, direccion, out hit, rangoFoto, cosoLayer))
                {
                    Debug.DrawRay(principal.position, direccion, Color.red);
                    if (hit.transform.IsChildOf(transform) || hit.transform == transform)
                    {
                        tiempoAturdido = 0;
                        if (Aturdido == null)
                        {
                            Aturdido = StartCoroutine(Aturdir());
                        }
                    }
                }
            }
        }
        
    }

    private IEnumerator Aturdir()
    {
        skin.material = glitch;
        agent.isStopped = true;
        
        while (tiempoAturdido < tiempoDeAturdicion)
        {
            tiempoAturdido += Time.deltaTime;
            yield return null;
        }
        
        skin.material = normal;
        agent.isStopped = false;
        Aturdido = null;
        yield return null;
    }
}
