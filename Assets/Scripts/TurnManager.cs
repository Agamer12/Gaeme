using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn;
    public bool interupt;
    public float waitTime;

    private bool change;
    public int turn;
    private int warnCount;
    private bool dead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerTurn = true;
        change = false;
        interupt = false;

        turn = 0;
        warnCount = 0;
        dead = false;

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
        
        if (dead)
        {
            killPlayer();
        }
    }

    public void setPlayerTurn(bool val)
    {
        isPlayerTurn = val;
    }

    public void setInterupt(bool val)
    {
        interupt = val;
    }

    public void killPlayer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
                setPlayerTurn(true);
                turn++;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
