using UnityEngine;
using TMPro;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string line;
    public float textSpeed;

    private TurnManager tm;

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        tm = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == line)
            {
                gameObject.SetActive(false);
                tm.interupt = false;
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = line;
            }
        }
    }

    public void StartDialog(string newLine)
    {
        tm.interupt = true;
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
