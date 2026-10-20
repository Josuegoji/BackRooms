using UnityEngine;
using UnityEngine.SceneManagement;

// Crea un Empty y agregale esta basura y sha, todo ready.
// Asume que el transform a seguir es el Camera.main.
// Necesita capsule collider para cargar la escena de derrota.
// Setea IsTrigger = true en Start.
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class CutreVolador : MonoBehaviour
{
    public float speed = 3.5f;
    public float floatingHeight = 1.5f;
    public float rotationSpeed = 5f;
    public string defeatScene = "EscenaDerrotaAquiTilin";
    public string playerTag = "Player";

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = Camera.main.transform;
        GetComponent<CapsuleCollider>().isTrigger = true;
    }

    private void Update()
    {
        if (!playerTransform) return;

        Vector3 targetPoint = new Vector3(playerTransform.position.x, floatingHeight, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        transform.rotation = playerTransform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            SceneManager.LoadScene(defeatScene);
    }
}