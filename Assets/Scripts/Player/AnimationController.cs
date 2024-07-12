using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();
    Animator anim => GetComponent<Animator>();
    SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    Ray2D ray;
    Vector2 ScreenPointTransform = new Vector2();
    Vector2 rayDirectionInScreenPoint = new Vector2();

    string lookAtDirection;

    public void CalculateLookAtDirection()
    {
        ScreenPointTransform = Camera.main.WorldToScreenPoint(transform.position);
        ray = new Ray2D(ScreenPointTransform, Input.mousePosition);
        rayDirectionInScreenPoint = new Vector2(Input.mousePosition.x - ScreenPointTransform.x, Input.mousePosition.y - ScreenPointTransform.y);
        //Debug.Log(rayDirectionInScreenPoint);
        FlipSprite();
    }
    public void FlipSprite()
    {
        if (rayDirectionInScreenPoint.x < 0 )
        {
            spriteRenderer.flipX= true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }
    public void FlipSprite(float direction)
    {
        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
