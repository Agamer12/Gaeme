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

    // Objects
    public GameObject player;

    public Transform movePoint;
    // Components
    private Rigidbody2D rb;
    private Animator anim;
    private TurnManager turnManager;

    // Variables
    public float moveSpeed = 5f;
    public LayerMask collisionLayer;
    private static PlayerMovement s_Instance;

    void Awake()
    {
        s_Instance = this;
        GameObject turnManagerObject = GameObject.Find("TurnManager");

        if (turnManagerObject != null)
        {
            turnManager = turnManagerObject.GetComponent<TurnManager>();
        }
        else
        {
            Debug.LogError("TurnManager GameObject not found in the scene.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Component references
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Align the movePoint to the grid
        movePoint.parent = null;
        movePoint.position = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0f
        );
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the movePoint at a fixed speed
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Allow movement only when it's the player's turn
        if (turnManager.isPlayerTurn)
        {
            // Ensure the player is at the movePoint
            if (Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                {
                    // Move horizontally by one grid unit
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    if (Physics2D.OverlapCircle(movePoint.position, 0.1f, collisionLayer))
                    {
                        movePoint.position = player.transform.position;
                        return;
                    }
                    // Snap to grid
                    movePoint.position = new Vector3(
                        Mathf.Round(movePoint.position.x),
                        Mathf.Round(movePoint.position.y),
                        0f
                    );

                    // Animation
                    anim.SetBool("isMoving", true);
                    anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    anim.SetFloat("lastMoveY", 0f);

                    turnManager.setPlayerTurn(false);
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    // Move vertically by one grid unit
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    if (Physics2D.OverlapCircle(movePoint.position, 0.1f, collisionLayer))
                    {
                        movePoint.position = player.transform.position;
                        return;
                    }

                    // Snap to grid
                    movePoint.position = new Vector3(
                        Mathf.Round(movePoint.position.x),
                        Mathf.Round(movePoint.position.y),
                        0f
                    );

                    // Animation
                    anim.SetBool("isMoving", true);
                    anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                    anim.SetFloat("lastMoveX", 0f);

                    turnManager.setPlayerTurn(false);
                }
                else
                {
                    // Stop movement animation if no input
                    anim.SetBool("isMoving", false);
                }
            }
        }

        // Trigger turn change when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            turnManager.setPlayerTurn(true);
        }
    }
}
