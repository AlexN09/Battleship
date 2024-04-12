using System.Collections;
using System.Collections.Generic;
using System;
//using UnityEditor;
//using UnityEditor.VersionControl;
using UnityEngine;
//using System.Collections.ObjectModel;
using System.Linq;
//using Unity.VisualScripting;
//using NUnit.Framework.Interfaces;

public class ManualPlacement : Sounds
{
    public GameObject ShipPartPrefab, singleDeckShip, fourDeckShip, tripleDeckShip, doubleDeckShip;
    public List<GameObject> ShipParts = new List<GameObject>();
    public List<GameObject> ShipTextures = new List<GameObject>();
   
    public int currentCell = 13, oldCell = 13;
   public  int rotation = 1, oldRotation = 1, decksAmount = 4,listCount = 2, lastDeckAmount = 4;
    public static int listCurrentSize = 1;
    public static bool isPlacementDone = false;
    public static bool IsButtonPressed = false;
    ShipSpawner copy;
    private void Start()
    {
       
        copy = GameObject.Find("Main Camera").GetComponent<ShipSpawner>();
        //ReDrawShip(decksAmount);
    }
    private void Update()
    {


       
      
        if (!isPlacementDone)
        {
            if (KeyHandler() == 40 && ShipParts.Count > 4)
            {
                EraseShips();
                ShipParts.Clear();
                ReDrawShip(decksAmount);
            }



            if (BorderChecker(KeyHandler(), currentCell, rotation))
            {
                currentCell += KeyHandler();
            }

            else if (KeyHandler() == 20 && canRotateHere())
            {
                if (rotation == 1) rotation = 2;
                else rotation = 1;
                PlaySound(sounds[0]);

            }
            else if (KeyHandler() == 50 && ShipParts.Count > 4)
            {
                PlaySound(sounds[0]);
                for (int i = 0; i < decksAmount; i++)
                {



                    Destroy(ShipParts[ShipParts.Count - 1]);

                    ShipParts.RemoveAt(ShipParts.Count - 1);


                }
                Destroy(ShipTextures[ShipTextures.Count - 1]);
                ShipTextures.RemoveAt(ShipTextures.Count - 1);
                if (ShipParts.Count <= 4)
                {
                  
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 4;
                 
                }
                else if (ShipParts.Count >= 4 && ShipParts.Count <= 10)
                {
                  
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 3;
                  
                }
                else if (ShipParts.Count >= 9 && ShipParts.Count <= 16)
                {
                   
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 2;
                    
                }
                else if (ShipParts.Count >= 15 && ShipParts.Count < 20)
                {
                   
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 1;
                   
                }
                ReDrawShip(decksAmount);
            }
            else if (KeyHandler() == 30 && CanPlaceHere())
            {
                
                if (ShipParts.Count < 4)
                {
                   
                    lastDeckAmount = decksAmount;
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 4;
                    DrawShip(55, decksAmount, rotation);
                    PlaySound(sounds[1]);
                }
                else if (ShipParts.Count >= 4 && ShipParts.Count < 9)
                {
                    lastDeckAmount = decksAmount;
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 3;
                    DrawShip(55, decksAmount, rotation);
                    PlaySound(sounds[1]);
                }
                else if (ShipParts.Count >= 9 && ShipParts.Count < 15)
                {
                    lastDeckAmount = decksAmount;
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 2;
                    DrawShip(55, decksAmount, rotation);
                    PlaySound(sounds[1]);
                }
                else if (ShipParts.Count >= 15 && ShipParts.Count < 20)
                {
                    lastDeckAmount = decksAmount;
                    currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 1;
                    DrawShip(55, decksAmount, rotation);
                    PlaySound(sounds[1]);
                }

                else
                {
                    isPlacementDone = true;
                    copy.PlaceShips();
                    FightController.isFightActive = true;
                }
              

            }
            //if (ShipParts.Count <= 4)
            //{
                if (currentCell != oldCell || oldRotation != rotation || oldCell == 55)
                {
                  
                    ReDrawShip(decksAmount);
                    oldCell = currentCell;  
                    oldRotation = rotation;
                }
            //}
           
        }

        listCurrentSize = ShipTextures.Count;



    }
  
    public bool CanPlaceHere()
    {
        if (ShipParts.Count > decksAmount)
        {
            bool canPlaceHere = true;

            if (rotation == 1)
            {
                foreach (GameObject part in ShipParts.Take(ShipParts.Count - decksAmount))
                {
                    for (int i = 0; i <= decksAmount; i++)
                    {
                        Vector2 PrtPos = part.transform.position;
                        if (currentCell % 10 > 0 && currentCell % 10 < 9 && (currentCell + decksAmount - 1) % 10 != 9)
                        {
                            if (GetCellPosition(currentCell, i) == PrtPos ||
                         GetCellPosition(currentCell, 10 + i) == PrtPos || GetCellPosition(currentCell, -10 + i) == PrtPos ||
                         GetCellPosition(currentCell, -1) == PrtPos || GetCellPosition(currentCell, -11) == PrtPos || GetCellPosition(currentCell, 9) == PrtPos)
                            {
                                canPlaceHere = false;
                                break;
                            }
                           
                        }
                        else if (currentCell % 10 == 0)
                        {
                            if (GetCellPosition(currentCell, i) == PrtPos || GetCellPosition(currentCell, 10 + i) == PrtPos || GetCellPosition(currentCell, -10 + i) == PrtPos)
                            {
                                canPlaceHere = false;
                                break;
                            }

                        }
                        else if ((currentCell + decksAmount - 1) % 10 == 9)
                        {
                            if (GetCellPosition(currentCell, i-1) == PrtPos)
                            {
                                canPlaceHere = false;
                                break;
                            }
                            else if (i < decksAmount)
                            {
                                if (GetCellPosition(currentCell, 10 + i) == PrtPos || GetCellPosition(currentCell, -10 + i) == PrtPos)
                                {
                                    canPlaceHere = false;
                                    break;
                                }
                            }
                            else if (PrtPos == GetCellPosition(currentCell, -11) || PrtPos == GetCellPosition(currentCell, 9))
                            {
                                canPlaceHere = false;
                                break;
                            }
                        }
                     
                    }
                    if (!canPlaceHere)
                        break;
                }
            }
            else if (rotation == 2)
            {
                foreach (GameObject part in ShipParts.Take(ShipParts.Count - decksAmount))
                {
                    Vector2 PrtPos = part.transform.position;
                    for (int i = 0; i <= decksAmount; i++)
                    {
                        if (currentCell % 10 > 0 && currentCell % 10 < 9)
                        {
                            if (PrtPos == GetCellPosition(currentCell, i * 10) ||
                           GetCellPosition(currentCell, 1 + i * 10) == PrtPos || GetCellPosition(currentCell, -1 + i * 10) == PrtPos ||
                           GetCellPosition(currentCell, -9) == PrtPos || GetCellPosition(currentCell, -11) == PrtPos || GetCellPosition(currentCell, -10) == PrtPos)
                            {
                                canPlaceHere = false;
                                break;
                            }
                        }
                        else
                        {
                            if (currentCell % 10 == 0)
                            {
                                if (PrtPos == GetCellPosition(currentCell, i * 10) || GetCellPosition(currentCell, 1 + i * 10) == PrtPos || GetCellPosition(currentCell, -9) == PrtPos || GetCellPosition(currentCell, -10) == PrtPos)
                                {
                                    canPlaceHere = false;
                                    break;
                                }
                            }
                            else if (currentCell % 10 == 9)
                                {
                                if (PrtPos == GetCellPosition(currentCell, i * 10) || GetCellPosition(currentCell, -1 + i * 10) == PrtPos || GetCellPosition(currentCell, -11) == PrtPos || GetCellPosition(currentCell, -10) == PrtPos)
                                {
                                    canPlaceHere = false;
                                    break;
                                }
                            }
                          
                        }
                    }
                    if (!canPlaceHere)
                        break;
                }
            }
            return canPlaceHere;
        }
        else
        {
            return true;
        }
       
    }

    private bool canRotateHere()
    {
        if (rotation == 2)
        {
            bool canRotateHere = true;
            for (int i = 0; i < decksAmount - 1; i++)
            {

                if (((currentCell + decksAmount - 1) % 10 == 0 + i))
                {
                    canRotateHere = false; break;
                }
            }
            if (canRotateHere) return true;
           
            else  return false; 
          

        }
        else
        {
            if ((((decksAmount - 1) * 10) + currentCell) >= 0 && (((decksAmount - 1) * 10) + currentCell) <= 99)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool BorderChecker(int Direction, int cellNumber,int rotation)
    {
        if (rotation == 1)
        {
            if (currentCell + KeyHandler() >= 0 && currentCell + KeyHandler() <= 99 && rotation == 1)
            {
                if (KeyHandler() == 1 && (currentCell + decksAmount - 1) % 10 != 9 || KeyHandler() == -1 && currentCell % 10 != 0 || KeyHandler() == 10 || KeyHandler() == -10)
                {
                    PlaySound(sounds[0]);
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else if (KeyHandler() == 10 && (((decksAmount - 1) * 10) + currentCell) / 10 != 9 || KeyHandler() == -10 && currentCell / 10 != 0 || KeyHandler() == 1 || KeyHandler() == -1 && rotation == 2)
        {
            if (KeyHandler() == 1 && currentCell % 10 != 9 || KeyHandler() == -1 && currentCell % 10 != 0 || KeyHandler() == 10 || KeyHandler() == -10)
            {
                PlaySound(sounds[0]);
                return true;
            }
            else return false;
        }
        else return false;
    }
    private int KeyHandler()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            return -1;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            return 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            return -10;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            return 10;
        else if (Input.GetKeyDown(KeyCode.Space))
            return 20;
        else if (Input.GetKeyDown(KeyCode.Return))
            return 30;
        else if (Input.GetKeyDown(KeyCode.E))
            return 40;
        else if (Input.GetKeyDown(KeyCode.Backspace))
            return 50;
        else return 0;      
    }
    private void DrawShip(int Position, int DecksAmount, int rotation)
    {
        GameObject newShipPart;

        for (int i = 0; i < DecksAmount; i++)
        {
            if (rotation == 1)
            {
                newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find("Own" + Position.ToString()).transform.position.x, GameObject.Find("Own" + (Position + i).ToString()).transform.position.y), Quaternion.identity);
                ShipParts.Add(newShipPart);
            }
            else
            {
                newShipPart = Instantiate(ShipPartPrefab, new Vector2(GameObject.Find("Own" + (Position + i * 10).ToString()).transform.position.x, GameObject.Find("Own" + Position.ToString()).transform.position.y), Quaternion.identity);
                ShipParts.Add(newShipPart);
            }

        }
        if (decksAmount == 4)
        {
            if (rotation == 1)
            {
                newShipPart = (Instantiate(fourDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x, GameObject.Find("Own" + Position.ToString()).transform.position.y - 1f, -1f), Quaternion.Euler(0f, 0f, -180)));
            }
            else 
            {
                newShipPart = Instantiate(fourDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x + 1, GameObject.Find("Own" + Position.ToString()).transform.position.y, -1f), Quaternion.Euler(0f,0f,-90));
            }             
        }
        else if (decksAmount == 3) 
        {
            if (rotation == 1)
            {
                newShipPart = Instantiate(tripleDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x + 0.05f, GameObject.Find("Own" + Position.ToString()).transform.position.y-0.7f, -1f), Quaternion.Euler(0f, 0f, -180));
            }
            else
            {
                newShipPart = Instantiate(tripleDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x + 0.7f, GameObject.Find("Own" + Position.ToString()).transform.position.y + 0.09f, -1.1f), Quaternion.Euler(0f, 0f, -90)); ;
            }
        }
        else if (decksAmount == 2)
        {
            if (rotation == 1)
            {
                newShipPart = Instantiate(doubleDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x, GameObject.Find("Own" + Position.ToString()).transform.position.y - 0.4f, -1f), Quaternion.Euler(0f, 0f, -180));
            }
            else
            {
                newShipPart = Instantiate(doubleDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x + 0.4f, GameObject.Find("Own" + Position.ToString()).transform.position.y, -2f), Quaternion.Euler(0f, 0f, -90));
            }
        }
        else
        {
            newShipPart = Instantiate(singleDeckShip, new Vector3(GameObject.Find("Own" + Position.ToString()).transform.position.x, GameObject.Find("Own" + Position.ToString()).transform.position.y, -1f), Quaternion.Euler(0f, 0f, -180));
        }
      
        ShipTextures.Add(newShipPart);

    }

    private void ReDrawShip(int decksAmount)
    {
        ShipParts.GetRange(Math.Max(0, ShipParts.Count - decksAmount), Math.Min(decksAmount, ShipParts.Count)).ForEach(obj => Destroy(obj));
        ShipParts.RemoveRange(Math.Max(0, ShipParts.Count - decksAmount), Math.Min(decksAmount, ShipParts.Count));
        if (ShipTextures.Count > 0)
        {
            Destroy(ShipTextures[ShipTextures.Count - 1]);
            ShipTextures.RemoveAt(ShipTextures.Count - 1);
        }
       
     




        DrawShip(currentCell, decksAmount, rotation);
    }
    public Vector2 GetCellPosition(int CellNumber, int iterator)
    {
        if (CellNumber + iterator >= 0 && CellNumber + iterator <= 99)
        {
            return GameObject.Find("Own" + (CellNumber + iterator).ToString()).transform.position;
        }
        else
        {
            return GameObject.Find("Own" + (CellNumber).ToString()).transform.position;
        }
    }
    public bool IsCellPartOfShip(int cell)
    {
        foreach (GameObject part in ShipParts)
        {
            Vector2 PrtPos = part.transform.position;
            if (PrtPos == GetCellPosition(cell, 0)) 
            {
                return true;
            }
        }
        return false;

    }
    
    public void EraseShips()
    {
        PlaySound(sounds[2]);
        foreach (GameObject part in ShipParts)
        {
            Destroy(part);
        }
        ShipTextures.ForEach(shipTexture => Destroy(shipTexture));

        ShipTextures.Clear();
        ShipParts.Clear();
        currentCell = 55; oldCell = 55; rotation = 1; oldRotation = 1; decksAmount = 4; isPlacementDone = false;
       
    }

}
