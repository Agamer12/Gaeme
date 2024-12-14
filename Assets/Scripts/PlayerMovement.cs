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
    public Animator anim;
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
        // anim = GetComponent<Animator>();

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
        var atMovePoint = Vector3.Distance(transform.position, movePoint.position) <= 0.1f;
        if (turnManager.isPlayerTurn)
        {
            // Ensure the player is at the movePoint
            if (atMovePoint)
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

                    anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    anim.SetFloat("lastMoveY", 0f);
                    anim.Play("PlayerMovement");

                    canWarn = true;
                    Invoke("DoWarn", 0.5f);
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

                    anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                    anim.SetFloat("lastMoveX", 0f);
                    anim.Play("PlayerMovement");
                    canWarn = true;
                    Invoke("DoWarn", 0.5f);
                }
            }
        }

        if (!AtTarget())
        {
            turnManager.setPlayerTurn(false);
        }


        // // Trigger turn change when Space is pressed
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     turnManager.setPlayerTurn(true);
        // }
    }

    public void TeleportPlayer(Vector3 newPosition)
    {
        // Set the player's and movePoint's positions to the new location
        transform.position = newPosition;
        movePoint.position = newPosition;
    }

    
    private string[] warningLines = {
        "*a chill runs up your spine*",
        "*creak*",
        "I think I heard something...",
        "It's too quiet...",
        "*rustle*",
        "What was that?",
        "...",
        "*silence*",
    };

    private bool canWarn = true;
    public bool AtTarget() { return Vector3.Distance(transform.position, movePoint.position) <= 0.1f; }

    public void DoWarn() {
        var dialog = FindFirstObjectByType<Dialog>(FindObjectsInactive.Include);
        
        var detectors = FindObjectsByType<DetectionManager>(FindObjectsSortMode.None);
        var shouldWarn = false;
        foreach (var detector in detectors)
        {
            if (detector.IsPlayerInWarning())
            {
                shouldWarn = true;
                break;
            }
        }


        if (!dialog.gameObject.activeSelf && shouldWarn && canWarn)
        {
            Debug.Log("WARHN");
            dialog.StartDialog(warningLines[Random.Range(0, warningLines.Length)]);
            canWarn = false;
        }
    }
}
