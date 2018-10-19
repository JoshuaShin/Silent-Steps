using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MovingFootsteps
{
    private float roomRaidus = 0.9f;

    void Start ()
    {
        StartAiRandomMovement();
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
}
