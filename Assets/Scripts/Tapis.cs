using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapis : MonoBehaviour
{
    [SerializeField]
    private Transform[] squares = null;
    private int[] squaresR;

    public float speed = 0.1f;

    private Transform playerTransform;

    private void Awake () {
        squaresR = new int[squares.Length];
        for(int i = 0; i < squares.Length; ++i){
            Transform square = squares[i];
            if(square.localPosition.x == 0.25f){
                squaresR[i] = 0;
            }else if(square.localPosition.y == 0.45f){
                squaresR[i] = 1;
            }else if(square.localPosition.x == -0.25f){
                squaresR[i] = 2;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        for(int i = 0; i < squares.Length; ++i){
            Transform square = squares[i];

            switch (squaresR[i]) {
                case 0 : 
                    square.localPosition += Vector3.down * dt * speed;
                    if(square.localPosition.y < -0.45f){
                        square.localPosition = new Vector3(0.25f + (square.localPosition.y + 0.45f), -0.45f, -1);
                        squaresR[i] = 1;
                    }
                    break;
                case 1 : 
                    square.localPosition += Vector3.left * dt * speed * 5;
                    if(square.localPosition.x < -0.25f){
                        square.localPosition = new Vector3(-0.25f, -0.45f - (square.localPosition.x + 0.25f), -1);
                        squaresR[i] = 2;
                    }
                    break;
                case 2 : 
                    square.localPosition += Vector3.up * dt * speed;
                    if(square.localPosition.y > 0.45f){
                        square.localPosition = new Vector3(-0.25f + (square.localPosition.y - 0.45f) , 0.45f, -1);
                        squaresR[i] = 3;
                    }
                    break;
                case 3 : 
                    square.localPosition += Vector3.right * dt * speed * 5;
                    if(square.localPosition.x > 0.25f){
                        square.localPosition = new Vector3(0.25f, 0.45f - (square.localPosition.x - 0.25f), -1);
                        squaresR[i] = 0;
                    }
                    break;
            }
        }

        if(playerTransform) playerTransform.position += Vector3.right * speed * dt * transform.localScale.y;
    }

    public void AffectPlayer (Transform t) {
        playerTransform = t;
    }

    public void UnaffectPlayer () {
        playerTransform = null;
    }
}
