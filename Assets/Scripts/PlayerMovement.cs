using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Objects
    private GameObject Player;

    //Components
    private Rigidbody2D rb;

    //Variables
    [SerializeField] 
    private float moveSpeed;

    private struct PlayerDirection {
        int x, y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.5)
        {
            Player.transform.position = new Vector3(Player.transform.position.x+Input.GetAxisRaw("Horizontal")*moveSpeed*Time.deltaTime, Player.transform.position.y, Player.transform.position.z);
            //TODO changed set player direction
        }

        if(Mathf.Abs(Input.GetAxisRaw("Vertical"))>0.5)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y+Input.GetAxisRaw("Vertical")*moveSpeed*Time.deltaTime, Player.transform.position.z);
            //TODO changed set player direction
        }
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }

    public void setMoveSpeed(float value) {
        moveSpeed = value;
    }
}
