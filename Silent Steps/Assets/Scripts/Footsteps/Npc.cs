using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MovingFootsteps
{
    private enum Ai { Random, Aggressive }

    [SerializeField]
    private Ai ai;
    [SerializeField]
    private float roomRaidus = 0.9f;

    void Start ()
    {
        if (ai == Ai.Random)
        {
            StartAiRandomMovement();
        }
        else if (ai == Ai.Aggressive)
        {
            //StartAiAggressiveMovement();
            StartCoroutine(temporaryFootstepDelayThing());
        }
    }

    IEnumerator temporaryFootstepDelayThing() // TODO: FOR DEMO ONLY!!!
    {
        yield return new WaitForSeconds(Random.Range(0f, 2f));
        StartAiAggressiveMovement();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void StartAiRandomMovement()
    {
        StartCoroutine(AiRandomMovement());
    }

    IEnumerator AiRandomMovement()
    {
        while(true)
        {
            MoveTo(new Vector2(Random.Range(-roomRaidus, roomRaidus), Random.Range(-roomRaidus, roomRaidus)));
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }        
    }

    private void StartAiAggressiveMovement()
    {
        StartCoroutine(AiAggressiveMovement());
    }

    IEnumerator AiAggressiveMovement()
    {
        while (true)
        {
            MoveTo(GameManager.instance.GetPlayer().transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
}
