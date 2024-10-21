using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Objects
    public GameObject player;

    //Components
    private Rigidbody2D rb;

    //Variables
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.5)
        {
            player.transform.position = new Vector3(player.transform.position.x+Input.GetAxisRaw("Horizontal")*moveSpeed*Time.deltaTime, player.transform.position.y, player.transform.position.z);
            
        }

        if(Mathf.Abs(Input.GetAxisRaw("Vertical"))>0.5)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+Input.GetAxisRaw("Vertical")*moveSpeed*Time.deltaTime, player.transform.position.z);
        }
    }
}
