using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    //script that just holds brick data
    public int points;
    public int hitsToBreak;
    public Sprite hitSprite;

    public void BreakBrick(){
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
}
