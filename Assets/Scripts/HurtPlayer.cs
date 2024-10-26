using UnityEngine;

public class HurtPlayer : MonoBehaviour
{   
     public int damageToGive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("detected player");
            collision.gameObject.GetComponent<PlayerHealthManager>().currentHealth-=damageToGive;
        }
    }
}
