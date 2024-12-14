using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectionManager : MonoBehaviour
{
    public float warningRange;
    public float deadRange;
    public float detectionAngleWarn;
    public float detectionAngleDead;
    public Transform enemyEye;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public Tilemap detectionTilemap;
    public TileBase warningTile;
    public TileBase deadTile;

    private Vector2 currentFacingDirection;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        currentFacingDirection = transform.up;
        UpdateDetectionTiles();
        CheckRanges();
    }

    void CheckRanges()
    {
        // Collider2D warn = DetectPlayer(warningRange, detectionAngleWarn);
        Collider2D dead = DetectPlayer(deadRange, detectionAngleDead);
        if (dead != null)
        {
            HandleRed(dead);
        }
    }

    public bool IsPlayerInWarning() {
        return DetectPlayer(warningRange, detectionAngleWarn) != null;
    }

    public Collider2D DetectPlayer(float range, float angle)
    {
        float halfAngle = angle / 2f;
        for (int i = 0; i <= 10; i++)
        {
            float checkAngle = -halfAngle + (angle / 10) * i;
            Vector2 direction = Quaternion.Euler(0, 0, checkAngle) * currentFacingDirection;
            RaycastHit2D hit = Physics2D.Raycast(enemyEye.position, direction, range, playerMask | obstacleMask);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                return hit.collider;
            }
        }
        return null;
    }

    void UpdateDetectionTiles()
    {
        detectionTilemap.ClearAllTiles();
        HighlightTiles(warningRange, warningTile, detectionAngleWarn);
        HighlightTiles(deadRange, deadTile, detectionAngleDead);
    }

    void HighlightTiles(float range, TileBase tile, float angle)
    {
        Vector3Int basePosition = detectionTilemap.WorldToCell(enemyEye.position);
        int tileRange = Mathf.FloorToInt(range);
        float halfAngle = angle / 2f;

        for (int x = -tileRange; x <= tileRange; x++)
        {
            for (int y = -tileRange; y <= tileRange; y++)
            {
                Vector3Int tilePosition = new Vector3Int(basePosition.x + x, basePosition.y + y, basePosition.z);
                Vector3 tileWorldPosition = detectionTilemap.CellToWorld(tilePosition) + detectionTilemap.cellSize / 2;
                Vector2 directionToTile = tileWorldPosition - enemyEye.position;

                if (Vector2.Distance(tileWorldPosition, enemyEye.position) <= range)
                {
                    float angleToTile = Vector2.SignedAngle(currentFacingDirection, directionToTile.normalized);
                    if (Mathf.Abs(angleToTile) <= halfAngle)
                    {
                        // Check if the tile is in line of sight
                        RaycastHit2D hit = Physics2D.Raycast(enemyEye.position, directionToTile.normalized, range, obstacleMask);
                        if (hit.collider == null || Vector2.Distance(hit.point, enemyEye.position) >= Vector2.Distance(tileWorldPosition, enemyEye.position))
                        {
                            detectionTilemap.SetTile(tilePosition, tile);
                        }
                    }
                }
            }
        }
    }

    void HandleRed(Collider2D colider)
    {
        Debug.Log("Immediate threat detected.");
        FindFirstObjectByType<SceneTransition>(FindObjectsInactive.Include).GameOver();
    }

    // void HandleYellow(Collider2D colider)
    // {
    //     Debug.Log("Player detected in warning range.");
    //     // FindFirstObjectByType<PlayerMovement>().inWarn = true;
    // }
}
