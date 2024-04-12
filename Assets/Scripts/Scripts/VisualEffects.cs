using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    // Start is called before the first frame update
    ManualPlacement copy;
    FightController copy2;
    private float timer = 0f;
    void Start()
    {
        copy2 = GameObject.Find("Main Camera").GetComponent<FightController>();
        Debug.Log(gameObject.name);
        if (gameObject.name == "TripleDeck(Clone)" || gameObject.name == "DoubleDeck(Clone)" || gameObject.name == "singleDeck(Clone)")
        {
            Debug.Log("sfjoash");
            FuncForShips();
        }
  
      
    }

    public void FuncForShips()
    {
        GetComponent<Renderer>().sortingLayerID = SortingLayer.layers[ManualPlacement.listCurrentSize].id;
        Debug.Log(ManualPlacement.listCurrentSize);
        copy = GameObject.Find("Main Camera").GetComponent<ManualPlacement>();
        if (!copy.CanPlaceHere())
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (copy.CanPlaceHere() && Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name == "Missile(Clone)")
        {

            if (copy2.isPlayerTurn)
            {
                transform.position = new Vector2(transform.position.x + 10f * Time.deltaTime,transform.position.y);
            }
            else if (!copy2.isPlayerTurn)
            {
                transform.position = new Vector2(transform.position.x - 10f * Time.deltaTime, transform.position.y);
            }
                      
        }
        else if (gameObject.name == "Explosion(Clone)")
        {
            timer += Time.deltaTime;
            if (timer >= 0.3)
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.name == "WaterSplash(Clone)")
        {
            timer += Time.deltaTime;
            if (timer >= 0.7)
            {
                Destroy(gameObject);
            }
        }
    }
}
