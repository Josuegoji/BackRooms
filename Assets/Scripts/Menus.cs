using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour, IUsable
{
    private bool usado = false;
    
    public void Usar()
    {
        if(usado) return;   
        usado = true;
        StartCoroutine(Reinicar());
    }

    private IEnumerator Reinicar()
    {
        float t = 0;
        Vector3 final = new Vector3(transform.position.x, transform.position.y, transform.position.z-.1f); 
        while (t < 1)
        {
            t += Time.deltaTime/2;
            transform.position = Vector3.Lerp(transform.position, final, t);
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
    }
}
