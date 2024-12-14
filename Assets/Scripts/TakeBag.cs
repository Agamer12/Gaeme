using UnityEngine;

public class TakeBag : MonoBehaviour
{
    public GameObject bag;
    public GameObject door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            bag.SetActive(false);
            door.SetActive(true);
        }
    }
}
