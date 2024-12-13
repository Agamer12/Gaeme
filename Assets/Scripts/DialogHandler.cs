using Unity.VisualScripting;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    private uint count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColisionEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Player"))
        {
            count++;
        }

        Debug.Log(count);
    }
}
