using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Objects
    public GameObject player;

    //Components
    private Rigidbody2D rb;
    private Animator anim;

    //Variables
    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Component references
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.5)
        {
            player.transform.position = new Vector3(player.transform.position.x+Input.GetAxisRaw("Horizontal")*moveSpeed*Time.deltaTime, player.transform.position.y, player.transform.position.z);

            //animation
            anim.SetBool("isMoving", true);
            anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("lastMoveY", 0f);

        }

        if(Mathf.Abs(Input.GetAxisRaw("Vertical"))>0.5)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+Input.GetAxisRaw("Vertical")*moveSpeed*Time.deltaTime, player.transform.position.z);

            //animation

            anim.SetBool("isMoving", true);
            anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            anim.SetFloat("lastMoveX", 0);
        }

        //Animation

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))<0.5&& Mathf.Abs(Input.GetAxisRaw("Vertical"))<0.5)
        {
            anim.SetBool("isMoving", false);
        }
    }
}
