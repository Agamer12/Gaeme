using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance
    {
        get
        {
            return s_Instance;
        }
    }

    //Objects
    public GameObject player;

    public Transform movePoint;
    //Components
    private Rigidbody2D rb;
    private Animator anim;

    //Variables
    public float moveSpeed;
    private static PlayerMovement s_Instance;

    void Awake()
    {
        s_Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Component references
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                //animation
                anim.SetBool("isMoving", true);
                anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("lastMoveY", 0f);

            }

            if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

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
}
