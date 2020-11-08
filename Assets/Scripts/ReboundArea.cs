using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundArea : MonoBehaviour
{
    private Movement movement;
    private PlayerConfiguration playerConfiguration;
    private PlayerJump playerJump;

    private void Awake () {
        playerConfiguration = Player.Instance.configPreset;
        playerJump = GetComponent<PlayerJump>();
    }
    
    private void Start()
    {
        movement = GetComponent<Movement>();
        EventManager.Instance.PlayerCollideWithGameObject += onCollideWithGO;
    }

    private void onCollideWithGO(GameObject GO, Vector3 normal)
    {
        if(GO.tag == "ReboundArea")
        {
            //float boundForce = playerJump.jumpActivated ? playerConfiguration.reboundCoefficientSucces : playerConfiguration.reboundCoefficientFail;
            //Debug.Log(playerJump.jumpActivated);

            float boundForce = playerConfiguration.reboundCoefficientSucces;

            if(normal.y != 0){
                movement.velocity.y = normal.y * boundForce;

                GO.GetComponent<JiggleEffect>().JumpEffectY();

                //playerJump.jumpLeft = 0;
            }else{
                movement.velocity.x = normal.x * boundForce;
                movement.velocity.y +=  boundForce / 2;
                movement.inertiaModifier = -playerConfiguration.inertiaSoustractor;

                GO.GetComponent<JiggleEffect>().JumpEffectX();
            }

            SoundBox.Instance.PlayBounceSound();
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.PlayerCollideWithGameObject -= onCollideWithGO;
    }
}
