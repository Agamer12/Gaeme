using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn;
    public bool interupt;
    public float waitTime;

    private bool change;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerTurn = true;
        change = false;
        interupt = false;

        StartCoroutine(ManagePlayerTurn());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn != change && isPlayerTurn)
        {
            Debug.Log("isPlayerTurn: "+ isPlayerTurn);
            change = isPlayerTurn;
        }
        
        // isPlayerTurn = true;
    }

    public void setPlayerTurn(bool val)
    {
        isPlayerTurn = val;
    }

    public void setInterupt(bool val)
    {
        interupt = val;
    }

    IEnumerator ManagePlayerTurn()
    {
        while (true)
        {
            // Wait until there are no interruptions
            yield return new WaitUntil(() => !interupt);

            // Set player's turn when no interruptions are present
            if (!isPlayerTurn)
            {
                Debug.Log("It's now the player's turn.");
                setPlayerTurn(true);
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
