using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum keyType 
{
    TeleportingKeys,
    MovesAwayKey,
    TreasureKey,
    NormalKey,
};

public class Key : MonoBehaviour
{
    [SerializeField]
    private Color[] colors = default;
    private static int colorsIndex = 0;

    private int noOfAnimsDone;
    private Rigidbody2D rb;

    public bool movesAwayHorizontally;
    public Vector3 teleportAddress;
    public int noOfAnimsRequired = 1;
    public keyType dropDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        colorsIndex = 1;
        if (colorsIndex < colors.Length)
        {
            GetComponent<SpriteRenderer>().color = colors[colorsIndex];
        }
    }

    public bool PickUp(Vector2 playerPostion) 
    {    
        if(this.noOfAnimsDone != this.noOfAnimsRequired) 
        {
            switch((int) dropDown) 
            {
                case 0:
                    Teleport();
                    break;
                
                case 1:
                    MoveAway(playerPostion);
                    break;

                case 2:
                    TreasureOpens();
                    break;

                default:
                    SimplePickUp();
                    return true;
            }

            this.noOfAnimsDone++;
        }
        else 
        {
            SimplePickUp();
            return true;
        }

        return false;
    } 

    private void Teleport() 
    {
        this.gameObject.transform.position = teleportAddress;
    }

    private void MoveAway(Vector2 playerPostion) 
    {
        Vector2 currentPosition = this.transform.position;
        if(movesAwayHorizontally) 
        {
            if(currentPosition.x < playerPostion.x) 
            {
                rb.AddForce(transform.right * -7, ForceMode2D.Impulse);
            }
            else 
            {
                rb.AddForce(transform.right * 7, ForceMode2D.Impulse);
            }
        }
        else 
        {
            if(currentPosition.y < playerPostion.y) 
            {
                rb.AddForce(transform.up * -7, ForceMode2D.Impulse);
            }
            else 
            {
                rb.AddForce(transform.up * 7, ForceMode2D.Impulse);
            }
        }
    }

    private void TreasureOpens() 
    {
        Debug.Log("Opening treasure");
        // SimplePickUp();
        // Summon gaurds
    }

    private void SimplePickUp() 
    {
        colorsIndex++;
        if(colorsIndex < colors.Length)
        {
            Debug.Log("Color become " + colors[colorsIndex].ToString());

            Key[] keys = FindObjectsOfType<Key>();

            Debug.Log(keys.Length);
            foreach (var key in keys)
            {
                key.gameObject.GetComponent<SpriteRenderer>().color = colors[colorsIndex];
            }
        }

        Debug.Log("Picking you");
    }
}
