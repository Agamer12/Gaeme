using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn;

    private bool change;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerTurn = false;
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn != change)
        {
            Debug.Log("isPlayerTurn: "+ isPlayerTurn);
            change = isPlayerTurn;
        }
    }

    public void setPlayerTurn(bool val)
    {
        isPlayerTurn = val;
    }
}
