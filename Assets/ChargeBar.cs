using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [Range(0, 1)]
    public float percentage;
    public Text textField;

    public SpriteRenderer backgroundSpriteRenderer;
    public SpriteRenderer borderSpriteRenderer;
    public SpriteRenderer barSpriteSpriteRenderer;

    public bool defaultVisible = false;

    void Start()
    {
        showBar(false);
    }

    // Update is called once per frame
    void SetSize(float sizeNormalised)
    {
        percentage = sizeNormalised;
    }

    public void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp(percentage, 0, 1), 1f);
        if(textField != null) {
            textField.text = Mathf.Floor(percentage * 100) + "%";
        }
    }

    public void showBar(bool show){
        backgroundSpriteRenderer.enabled = show;
        borderSpriteRenderer.enabled = show;
        barSpriteSpriteRenderer.enabled = show;
    }
}
