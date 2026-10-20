using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerInteractorCercano : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerAction; // Acción del trigger
    [SerializeField] private float radioBusqueda = 0.08f; // Radio de detección
    [SerializeField] private LayerMask capasUsables; // Filtra qué objetos son interactuables

    private bool triggerPresionadoAnterior = false; // Para detectar el "clic"

    private void Update()
    {
        // Lee el valor del trigger (0–1 en RV, 0/1 en teclado)
        bool triggerPresionado = triggerAction != null && triggerAction.action.ReadValue<float>() > 0.5f;

        // Solo ejecuta en el momento en que se presiona
        if (triggerPresionado && !triggerPresionadoAnterior)
        {
            IntentarUsar();
        }

        triggerPresionadoAnterior = triggerPresionado;
    }

    private void IntentarUsar()
    {
        // Busca colliders cercanos dentro del radio
        Collider[] hits = Physics.OverlapSphere(transform.position, radioBusqueda, capasUsables);

        float mejorDistancia = float.MaxValue;
        IUsable mejorUsable = null;

        foreach (Collider hit in hits)
        {
            // Obtiene un IUsable en el objeto o su jerarquía
            IUsable usable = hit.GetComponentInParent<IUsable>();
            if (usable == null) continue;

            // Distancia real al collider
            float distancia = Vector3.Distance(transform.position, hit.ClosestPoint(transform.position));

            // Se queda con el más cercano
            if (distancia < mejorDistancia)
            {
                mejorDistancia = distancia;
                mejorUsable = usable;
            }
        }

        // Ejecuta la acción en el objeto más cercano
        mejorUsable?.Usar();
    }
    private void OnDrawGizmosSelected()
    {
        // Visual del radio en editor
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}