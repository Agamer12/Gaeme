using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] lines;
    private uint count;
    private Dialog text;

    void Awake()
    {
        // Correctly use GetComponent to fetch the Dialog component from the GameObject named "DialogBox"
        text = GameObject.Find("DialogBox").GetComponent<Dialog>();
    }

    void Start()
    {
        count = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (count <= lines.Length)
            {
                text.StartDialog(lines[count]);
            }
            else
            {
                if (lines[lines.Length - 1] != null)
                {
                    text.StartDialog(lines[lines.Length - 1]);
                }
            }
            count++;
        }
    }
}
