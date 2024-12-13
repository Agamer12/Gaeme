using UnityEngine;
using TMPro;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string line;  // This holds the current line of text to display
    public float textSpeed;

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == line)
            {
                gameObject.SetActive(false);
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = line;
            }
        }
    }

    public void StartDialog(string newLine) // Corrected method name and parameter name
    {
        gameObject.SetActive(true);
        line = newLine;
        textComponent.text = "";
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
