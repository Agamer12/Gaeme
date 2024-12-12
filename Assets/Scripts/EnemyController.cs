using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;
    public float speed = 3.0f;
    public float rotationSpeed = 5.0f;

    private PlayerMovement player;

    void Update()
    {
        player = LookForPlayer();
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.z = 0;

            MoveTowardsPlayer(direction);
            RotateTowardsPlayer(direction);
        }
    }

    private void MoveTowardsPlayer(Vector3 direction)
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void RotateTowardsPlayer(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private PlayerMovement LookForPlayer()
    {
        if (PlayerMovement.Instance == null)
        {
            return null;
        }

        Vector3 toPlayer = PlayerMovement.Instance.transform.position - transform.position;
        toPlayer.z = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            float angleBetween = Vector3.Angle(transform.right, toPlayer);
            if (angleBetween <= detectionAngle / 2)
            {
                Debug.Log("Player has been detected!");
                return PlayerMovement.Instance;
            }
        }

        return null;
    }
}
