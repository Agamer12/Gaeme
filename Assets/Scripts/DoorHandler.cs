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

    private TurnManager tm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (magicTP && Input.GetKeyDown(KeyCode.T))
        {
            load();
            Vector3 newPosition = door.transform.position;

            newPosition.x = Mathf.Round(newPosition.x);
            newPosition.y = Mathf.Round(newPosition.y);
            PlayerMovement.Instance.TeleportPlayer(newPosition);

            Vector3 cameraPosition = room.transform.position;
            cameraPosition.z = Camera.main.transform.position.z;
            Camera.main.transform.position = cameraPosition;
            unload();
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
        load();

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayBeforeTeleport);

        Vector3 offset = collider.transform.position - transform.position;
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

        unload();

        tm.setPlayerTurn(true);
        tm.setInterupt(false);
    }

    private void load()
    {
        foreach (GameObject obj in Loadlist)
        {
            obj.SetActive(true);
        }
    }

    private void unload()
    {
        foreach (GameObject obj in UnloadList)
        {
            obj.SetActive(false);
        }
    }
}
