using System.Collections;
using UnityEngine;

/*
 * Script de elevador para que sea llamado por una palanca.
 * El elevador se movera hacia arriba o hacia abajo dependiendo de su estado actual.
 */
[RequireComponent(typeof(Rigidbody))]
public class Elevator : MonoBehaviour
{
    public float alturaElevacion = 5f;
    public float velocidad = 2f;

    private Vector3 posicionInicial;
    private bool estaArriba = false;
    private bool enMovimiento = false;
    private Rigidbody rb;
    [SerializeField] private bool automatico = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        posicionInicial = transform.position;

        if (automatico)
        {
            StartCoroutine(Subir());
        }
    }
    

    public void ActivarElevador()
    {
        if (enMovimiento) return;

        if (estaArriba)
        {
            StartCoroutine(Bajar());
        }
        else
        {
            StartCoroutine(Subir());
        }
    }

    private IEnumerator Subir()
    {
        enMovimiento = true;

        Vector3 destino = posicionInicial + Vector3.up * alturaElevacion;

        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            rb.MovePosition(Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.fixedDeltaTime
            ));
            /*
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidad * Time.deltaTime
            );
            */
            yield return null;
        }

        transform.position = destino;
        estaArriba = true;
        enMovimiento = false;
        if (automatico)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(Bajar());
        }
    }

    private IEnumerator Bajar()
    {
        enMovimiento = true;

        while (Vector3.Distance(transform.position, posicionInicial) > 0.01f)
        {
            rb.MovePosition(Vector3.MoveTowards(
                transform.position,
                posicionInicial,
                velocidad * Time.fixedDeltaTime
            ));
            /*
            transform.position = Vector3.MoveTowards(
                transform.position,
                posicionInicial,
                velocidad * Time.deltaTime
            );
            */
            yield return null;
        }

        transform.position = posicionInicial;
        estaArriba = false;
        enMovimiento = false;
        if (automatico)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(Subir());
        }
    }
}