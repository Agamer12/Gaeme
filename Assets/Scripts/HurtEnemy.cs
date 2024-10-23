using UnityEngine;

public class HurtEnemy : MonoBehaviour
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

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("detected enemy");
        if(col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyHealthManager>().currentHealth-=damageToGive;
        }
    }
}
