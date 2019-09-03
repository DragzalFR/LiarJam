using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_Life : MonoBehaviour
{
    public Sprite[] LifeSprites;
    public Player Prisoner;
    private int Life;

    public void Start()
    {
        Life = Prisoner.UI_Life;
        var spriteIndex = Mathf.Max(0, Life - 1);
        GetComponent<Image>().sprite = LifeSprites[spriteIndex];
    }

    public void Update()
    {
        if(Life != Prisoner.UI_Life)
        {
            Life = Prisoner.UI_Life;
            var spriteIndex = Mathf.Max(0, Life - 1);
            GetComponent<Image>().sprite = LifeSprites[spriteIndex];
        }
    }
}
