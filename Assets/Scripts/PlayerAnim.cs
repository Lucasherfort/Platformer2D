using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Movement movement;
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GraphicsFeedbacksSettings.Instance.ActivePlayerAnimation){
            float height = Mathf.Clamp(1 + Mathf.Abs(movement.velocity.y) / 50, 1, 10);
            float width = 1.5f - height / 2;
            transform.localScale = new Vector2(width, height);
        }else{
            transform.localScale = new Vector2(1, 1);
        }
    }
}
