using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Vector3[] posiblesPosiciones;
    private Transform jugador;
    [SerializeField] private Material glitch, normal;
    private NavMeshAgent agent;
    [SerializeField] private float tiempoDeAturdicion = 5f;
    [SerializeField] private float fov = 90, rangoFoto = 15;
    [SerializeField] private LayerMask cosoLayer;
    private float tiempoAturdido = 0;
    private Coroutine Aturdido;
    private SkinnedMeshRenderer skin;
    private bool puedePerseguir = true;
    private Animator animator;
    [SerializeField] private Collider golpe, golpe2;
    private bool atacando = false;
    [SerializeField] private Transform punto;
    [SerializeField] private AudioClip matar;
    [SerializeField] private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = Camera.main.GetComponent<Transform>();
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null) return;

        if (puedePerseguir && agent.enabled)
        {
            agent.SetDestination(jugador.position);
        }

        if (!agent.pathPending && agent.enabled)
        {
            if (agent.remainingDistance < 1f)
            {
                Debug.Log(agent.remainingDistance);
                if (!atacando)
                {
                    animator.SetTrigger("Ataque");
                    audioSource.PlayOneShot(matar);
                    atacando = true;
                }

            }
        }
    }
    
    private void OnEnable()
    {
        Camara.fotoTomada += FotoTomada;
    }

    private void OnDisable()
    {
        Camara.fotoTomada -= FotoTomada;
    }

    public void ActivarGolpe()
    {
        golpe.enabled = true;
        golpe2.enabled = true;
        
    }

    public void DesactivarGolpe()
    {
        golpe.enabled = false;
        golpe2.enabled = false;
        atacando = false;
    }
    
    void FotoTomada()
    {
        Vector3 direccion = (punto.position - jugador.position).normalized;
        
        float angulo = Vector3.Angle(jugador.forward, direccion);
    
        float distancia = Vector3.Distance(jugador.position, punto.position);
        
        if (angulo <= fov / 2f)
        {
            if (distancia <= rangoFoto)
            {
                RaycastHit hit;
                if (Physics.Raycast(jugador.position, direccion, out hit, rangoFoto, cosoLayer))
                {
                    Debug.Log("Golpeó: " + hit.transform.name + " | Tag: " + hit.transform.tag);
                    Debug.DrawRay(jugador.position, direccion * rangoFoto, Color.red, 10f);
                    if (hit.transform.IsChildOf(transform) || hit.transform == transform)
                    {
                        Debug.Log(("alcanzado"));
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
        animator.SetBool("Idle", true);
        
        puedePerseguir = false;
        skin.material = glitch;
        agent.isStopped = true;
        
        while (tiempoAturdido < tiempoDeAturdicion/2)
        {
            tiempoAturdido += Time.deltaTime;
            yield return null;
        }
        
        agent.enabled = false;
        transform.position = BuscarPuntoMasLejano();
        yield return new WaitForSeconds(tiempoDeAturdicion);
        agent.enabled = true;
        puedePerseguir = true;
        skin.material = normal;
        agent.isStopped = false;
        animator.SetBool("Idle", false);
 
        Aturdido = null;
        yield return null;
    }

    private Vector3 BuscarPuntoMasLejano()
    {
        Vector3 puntoMasLejano = jugador.position;

        foreach (var posible in posiblesPosiciones)
        {
            if((Vector3.Distance(jugador.position, posible)) > (Vector3.Distance(jugador.position, puntoMasLejano)))
            {
                puntoMasLejano = posible;
            }
        }

        return puntoMasLejano;
    }
}
