using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleEffect : MonoBehaviour
{
    public Transform transformRender = null;

    private float progression = 0;
    private Vector2 initScale;
    private Vector2 targetVariation;

    private void Start () {
        initScale = transformRender.localScale;
        targetVariation = new Vector2(GraphicsFeedbacksSettings.Instance.JiggleScaleVariation / transform.localScale.x, GraphicsFeedbacksSettings.Instance.JiggleScaleVariation / transform.localScale.y);
    }

    private void Update() {
        progression += Time.deltaTime * GraphicsFeedbacksSettings.Instance.JiggleSpeed;

        targetVariation = Vector2.Lerp(targetVariation, new Vector2(GraphicsFeedbacksSettings.Instance.JiggleScaleVariation / transform.localScale.x, GraphicsFeedbacksSettings.Instance.JiggleScaleVariation / transform.localScale.y), Time.deltaTime * GraphicsFeedbacksSettings.Instance.BounceJumpSpeed);

        Vector2 scale = new Vector2();
        scale.x = initScale.x + (Mathf.Cos(progression) + 1) / 2 * targetVariation.x;
        scale.y = initScale.y + (Mathf.Sin(progression) + 1) / 2 * targetVariation.y;

        transformRender.localScale = scale;
    }

    public void JumpEffectX () {
        targetVariation.x = GraphicsFeedbacksSettings.Instance.BounceJumpScaleVariation / transform.localScale.x;
        progression = Mathf.PI / 2;
    }

    public void JumpEffectY () {
        targetVariation.y = GraphicsFeedbacksSettings.Instance.BounceJumpScaleVariation  / transform.localScale.y;
        progression = Mathf.PI;
    }
}
