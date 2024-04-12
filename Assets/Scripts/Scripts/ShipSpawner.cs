//using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class ShipSpawner : MonoBehaviour
{
    public GameObject ShipPartPrefab;
    public List<GameObject> ShipParts = new List<GameObject>();
    public int DecksAmount, ShipsAmount;
    public int rotation = 1;
    private void Start()
    {
        PlaceShips();
    }
    private void Update()
    {


      

    }
    public bool isMouseOnShip()
    {
        foreach  (GameObject part in ShipParts)
        {
           Vector2 PrtPos = part.transform.position;
            if (PrtPos == GetClickedObjPos())
            {
                return true;
            }
        }
        return false;
        
    }
    public void PlaceShips()
    {
        EraseShips();
        SpawnShip(1, 4);
        SpawnShip(2, 3);
        SpawnShip(3, 2);
        SpawnShip(4, 1);
    }
    public Vector2 GetClickedObjPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i <= 99; i++)
            {
                Bounds bounds = GameObject.Find(i.ToString()).GetComponent<SpriteRenderer>().bounds;

                if (bounds.Contains(mousePosition2D))
                {
                   return GameObject.Find(i.ToString()).transform.position;
                }
            }                                   
            
        }


        return Vector2.zero; ;
    }

    private void SpawnShip(int ShipsAmount, int DecksAmount) //1 - diagonally 2 - horizontally
    {       
              
        for (int i = 0; i < ShipsAmount; i++)
        {
         
            int RandomPos = 0;
            bool canContinue = false;
          
            

            //rotation = UnityEngine.Random.Range(1, 3);
            while (true)
            {
               
                RandomPos = UnityEngine.Random.Range(0, 99);
                rotation = UnityEngine.Random.Range(1, 3);
                canContinue = true; // Устанавливаем значение по умолчанию перед началом проверки

                foreach (GameObject part in ShipParts)
                {
                   
                    Vector2 PrtPos = part.transform.position;

                    for (int j = 0; j <= DecksAmount; j++)
                    {
                        if (ShipParts.Count == 35)
                        {

                        }
                        if (rotation == 1)
                        {
                            if (RandomPos % 10 < 9 - DecksAmount)
                            {
                                if (GetCellPosition(RandomPos, j) == PrtPos || GetCellPosition(RandomPos, -1) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 10 + j) == PrtPos || GetCellPosition(RandomPos, -10 + j) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 9) == PrtPos || GetCellPosition(RandomPos, -11) == PrtPos)
                                {
                                    canContinue = false; break;
                                }

                            }
                            else
                            {
                                if (GetCellPosition(RandomPos, -j) == PrtPos || GetCellPosition(RandomPos, 1) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 10 - j) == PrtPos || GetCellPosition(RandomPos, -10 - j) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, -9) == PrtPos || GetCellPosition(RandomPos, 11) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                            }

                        }
                        else if (rotation == 2)
                        {
                            if (RandomPos + 10 * DecksAmount <= 99)
                            {
                                if (GetCellPosition(RandomPos, j * 10) == PrtPos || GetCellPosition(RandomPos, -10) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 1 + j * 10) == PrtPos || GetCellPosition(RandomPos, -1 + j * 10) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, -9) == PrtPos || GetCellPosition(RandomPos, -11) == PrtPos)
                                {
                                    canContinue = false; break;
                                }

                            }
                            else
                            {
                                if (GetCellPosition(RandomPos, -j * 10) == PrtPos || GetCellPosition(RandomPos, 10) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 1 - j * 10) == PrtPos || GetCellPosition(RandomPos, -1 - j * 10) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                                else if (GetCellPosition(RandomPos, 9) == PrtPos || GetCellPosition(RandomPos, 11) == PrtPos)
                                {
                                    canContinue = false; break;
                                }
                            }
                        }
                    }


                   
                    if (!canContinue)
                    {
                        
                        break;
                    }
                  
                }

                if (canContinue) // Если не было найдено совпадение, выходим из цикла while
                {
                   
                    break;
                }
            }

           
                DrawShip(RandomPos, DecksAmount, rotation);
                
            
          
           
        }


    }

    private void DrawShip(int Position, int DecksAmount, int rotation)
    {
        GameObject newShipPart;
        for (int i = 0; i < DecksAmount; i++)
        {
            if (Position % 10 < 9 - DecksAmount && rotation == 1)
            {
                newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find(Position.ToString()).transform.position.x, GameObject.Find((Position + i).ToString()).transform.position.y), Quaternion.identity);

            }
            else
            {
                if (rotation == 1)
                {
                    newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find(Position.ToString()).transform.position.x, GameObject.Find((Position - i).ToString()).transform.position.y), Quaternion.identity);
                }
                else if (Position + 10 * DecksAmount <= 99 && rotation == 2)
                {
                    newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find((Position + 10 * i).ToString()).transform.position.x, GameObject.Find((Position).ToString()).transform.position.y), Quaternion.identity);
                }
                else
                {
                    newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find((Position - 10 * i).ToString()).transform.position.x, GameObject.Find((Position).ToString()).transform.position.y), Quaternion.identity);
                }
            }

            ShipParts.Add(newShipPart);
        }
    }
   
  
    public Vector2 GetCellPosition(int CellNumber, int iterator)
    {
        if (CellNumber + iterator >= 0 && CellNumber + iterator <= 99)
        {
            return GameObject.Find((CellNumber + iterator).ToString()).transform.position;
        }
        else
        {
            return GameObject.Find((CellNumber).ToString()).transform.position;
        }
    }   
 
    public void EraseShips()
    {
        foreach (GameObject part in ShipParts)
        {
            Destroy(part);
        }
        ShipParts.Clear();
    }


}