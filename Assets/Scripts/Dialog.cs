using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                        textComponent.text = lines[index];
            }
        }
        
    }

    void StartDiolog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index]) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
