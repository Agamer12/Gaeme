using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public GameObject room;
    public GameObject door;

    public GameObject[] Loadlist;
    public GameObject[] UnloadList;
    public float delayBeforeTeleport; 

    public bool magicTP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (magicTP && Input.GetKeyDown(KeyCode.T))
        {
            GameObject player = GameObject.Find("Player");
            StartCoroutine(TeleportAfterDelay(player.GetComponent<Collider2D>()));
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(TeleportAfterDelay(collider));
        }
    }

    IEnumerator TeleportAfterDelay(Collider2D collider)
    {
        foreach (GameObject obj in Loadlist)
        {
            obj.SetActive(true);
        }

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayBeforeTeleport);

        Vector3 offset = collider.transform.position - gameObject.transform.position;
        Vector3 newPosition = door.transform.position + offset;

        newPosition.x = Mathf.Round(newPosition.x);
        newPosition.y = Mathf.Round(newPosition.y);

        PlayerMovement.Instance.TeleportPlayer(newPosition);

        if (Camera.main != null)
        {
            Vector3 cameraPosition = room.transform.position;
            cameraPosition.z = Camera.main.transform.position.z;
            Camera.main.transform.position = cameraPosition;
        }

        foreach (GameObject obj in UnloadList)
        {
            obj.SetActive(false);
        }
    }
}
