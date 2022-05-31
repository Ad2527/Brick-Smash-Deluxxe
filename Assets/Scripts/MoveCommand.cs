using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Command 
{
    Transform moved;
    // Start is called before the first frame update
    public Command(Transform target)
    {
        //storing the paddle's transform as our object to act on
        moved = target;
    }

    public void ExecutePaddleMoveCommand(float horizontal, float speed)
    {
        //command to move left/right, with parsed params horizontal and speed
        moved.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
    }

    public void ExecutePushCommand(float ScreenEdge) {
        // push away from left screen edge
        moved.position = new Vector2(ScreenEdge, moved.position.y);
    }
    public void ChangePaddleSizeCommand(float scale) {
        //changes the paddle size when called by the scale variable
        var newScale = moved.localScale;
        newScale.x *= scale;
        moved.localScale = newScale;
    }

}
