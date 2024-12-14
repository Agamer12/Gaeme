using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] movePoints;
    public int waitTurns;
    public Transform eye;

    private TurnManager tm;
    private int nextMovePointIndex;

    private int turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tm = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        foreach (Transform obj in movePoints)
        {
            obj.parent = null;
        }
        turn = tm.turn;
        nextMovePointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (turn < tm.turn)
        {
            if (Vector3.Distance(transform.position, movePoints[nextMovePointIndex].transform.position) <= 0.1f)
            {
                nextMovePointIndex = (nextMovePointIndex + 1) % movePoints.Length;
                turn += waitTurns;
                tm.setInterupt(false);
                return;
            }

            Vector3 vec = (movePoints[nextMovePointIndex].transform.position - transform.position).normalized;
            tm.setInterupt(true);
            StartCoroutine(MoveToNextPoint(vec));
        }
    }

    private IEnumerator MoveToNextPoint(Vector3 vec)
    {
        turn = tm.turn;
        Vector3 currentPos = transform.position;
        Vector3 newPos = transform.position + vec;
        
        float distance = Vector3.Distance(currentPos, newPos);
        float journeyLength = distance;
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, newPos) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(currentPos, newPos, fractionOfJourney);

            yield return null;
        }

        transform.position = new Vector3(newPos.x, newPos.y, 0);

        if (Vector3.Distance(transform.position, movePoints[nextMovePointIndex].transform.position) <= 0.1f)
        {
            transform.rotation = movePoints[nextMovePointIndex].transform.rotation;
        }
        else
        {
            float angle = (float)Math.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        }

        tm.setInterupt(false);
    }
}
