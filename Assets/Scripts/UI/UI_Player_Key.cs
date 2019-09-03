using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_Key : MonoBehaviour
{
    public Sprite[] KeySprites;
    public Player Prisoner;
    private int Key;

    public void Start()
    {
        Key = Prisoner.UI_Key;
        var spriteIndex = Mathf.Max(0, Key);
        GetComponent<Image>().sprite = KeySprites[spriteIndex];
    }

    public void Update()
    {
        if (Key != Prisoner.UI_Key)
        {
            Key = Prisoner.UI_Key;
            var spriteIndex = Mathf.Max(0, Key);
            GetComponent<Image>().sprite = KeySprites[spriteIndex];
        }
    }
}
