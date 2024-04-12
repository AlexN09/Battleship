//using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightController : Sounds
{
    ShipSpawner copy;
    ManualPlacement copyPlayer;
    public List<GameObject> sings = new List<GameObject>();
    public List<int> BeatenCellsOwn = new List<int>(); public List<Vector2> BeatenCells = new List<Vector2>();
    public GameObject CheckMark, Cross, missilePrefab, InstantiatedMissile, Explosion, WaterSpalsh, panel,winText,loseText;
    public int currentTurn = 1,lastIterator = 0,SuccessfullIterator = 0,successfullyBeatenShipsOwn = 0 , successfullyBeatenShips = 0; // 1 = player turn; 2 = pc turn
    public bool wasBeaten = false, isMissileActive, isCellPartOfShip, isPlayerTurn = true;
    public static bool isFightActive = false;

    Vector2 TargetPos = Vector2.zero;
    void Start()
    {
        
        copy = GameObject.Find("Main Camera").GetComponent<ShipSpawner>();
        copyPlayer = GameObject.Find("Main Camera").GetComponent<ManualPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        

      
        if (isMissileActive && isFightActive)
        {
            if (isPlayerTurn)
            {
                if (isCellPartOfShip && InstantiatedMissile.transform.position.x >= TargetPos.x)
                {
                    sings.Add(Instantiate(CheckMark, TargetPos, Quaternion.identity));
                    PlaySound(sounds[0]);
                    Instantiate(Explosion, TargetPos, Quaternion.identity);
                    Destroy(InstantiatedMissile);
                    isMissileActive = false;
                    successfullyBeatenShips++;
                }
                else if (!isCellPartOfShip && InstantiatedMissile.transform.position.x >= TargetPos.x)
                {
                    sings.Add(Instantiate(Cross, TargetPos, Quaternion.identity));
                    PlaySound(sounds[1]);
                    Instantiate(WaterSpalsh, TargetPos, Quaternion.identity);
                    Destroy(InstantiatedMissile);
                    currentTurn = 2; isMissileActive = false;
                }
            }
            else
            {
                if (InstantiatedMissile != null)
                {
                    if (isCellPartOfShip && InstantiatedMissile.transform.position.x <= TargetPos.x)
                    {
                        sings.Add(Instantiate(Cross, TargetPos, Quaternion.identity));
                        PlaySound(sounds[0]);
                        Instantiate(Explosion, TargetPos, Quaternion.identity);
                        Destroy(InstantiatedMissile);
                        wasBeaten = true;
                        successfullyBeatenShipsOwn++;
                    }
                    else if (!isCellPartOfShip && InstantiatedMissile.transform.position.x <= TargetPos.x)
                    {
                        PlaySound(sounds[1]);
                        sings.Add(Instantiate(Cross, TargetPos, Quaternion.identity));
                        Instantiate(WaterSpalsh, TargetPos, Quaternion.identity);
                        Destroy(InstantiatedMissile);
                        currentTurn = 1;
                    }
                }
                else
                {
                    isMissileActive = false;
                }

            }
        }
        if (!isMissileActive && isFightActive)
        {
            if (currentTurn == 1)
            {
                if (copy.isMouseOnShip() && !BeatenCells.Contains(copy.GetClickedObjPos()))
                {
                    TargetPos = copy.GetClickedObjPos();
                    InstantiatedMissile = Instantiate(missilePrefab, new Vector2(-0.8f, TargetPos.y), Quaternion.Euler(0, 0, -90));
                    isMissileActive = true; isCellPartOfShip = true; isPlayerTurn = true;
                    BeatenCells.Add(copy.GetClickedObjPos());
                    PlaySound(sounds[2]);
                }
                else if (copy.GetClickedObjPos() != Vector2.zero && !BeatenCells.Contains(copy.GetClickedObjPos()))
                {
                    TargetPos = copy.GetClickedObjPos();
                    InstantiatedMissile = Instantiate(missilePrefab, new Vector2(-0.8f, TargetPos.y), Quaternion.Euler(0, 0, -90));
                    isMissileActive = true; isCellPartOfShip = false; isPlayerTurn = true;
                    BeatenCells.Add(copy.GetClickedObjPos());
                    PlaySound(sounds[2]);
                }
            }
            else if (currentTurn == 2)
            {
                int randomCell;
                if (!wasBeaten)
                {
                    randomCell = GenerateRndCell();
                    Debug.Log("True");
                }
                else
                {
                    randomCell = 0;
                    if (!BeatenCellsOwn.Contains(randomCell) && BeatenCellsOwn.Count > 0)
                    {

                        while (true)
                        {
                            int lastEl = BeatenCellsOwn[BeatenCellsOwn.Count - 1];
                            if (SuccessfullIterator == 0)
                            {
                                randomCell = new int[] { -10, -1, 1, 10 }[Random.Range(0, 4)];
                                lastIterator = randomCell;
                            }
                            else
                            {
                                randomCell = SuccessfullIterator;
                                if (BeatenCellsOwn.Contains(randomCell + lastEl))
                                {
                                    randomCell = GenerateRndCell();
                                }
                            }
                          

                            
                            if (randomCell + lastEl >= 0 && randomCell + lastEl <= 99)
                            {
                                break;
                            }
                            else if (lastEl % 10 == 9 && randomCell != 1 || lastEl % 10 == 0 && randomCell != -1)
                            {
                                break;
                            }
                        }


                        randomCell += BeatenCellsOwn[BeatenCellsOwn.Count - 1];
                    }
                    else
                    {
                        randomCell = GenerateRndCell();

                    }
                    wasBeaten = false;
                }

                if (copyPlayer.IsCellPartOfShip(randomCell))
                {
                    SuccessfullIterator = lastIterator;
                    TargetPos = copyPlayer.GetCellPosition(randomCell, 0);
                    InstantiatedMissile = Instantiate(missilePrefab, new Vector2(1.4f, TargetPos.y), Quaternion.Euler(0, 0, 90));
                    isCellPartOfShip = true; BeatenCellsOwn.Add(randomCell); isMissileActive = true; isPlayerTurn = false;
                   
                }
                else
                {
                    TargetPos = copyPlayer.GetCellPosition(randomCell, 0);
                    InstantiatedMissile = Instantiate(missilePrefab, new Vector2(1.4f, TargetPos.y), Quaternion.Euler(0, 0, 90));
                    isCellPartOfShip = false; BeatenCellsOwn.Add(randomCell); isMissileActive = true; isPlayerTurn = false;
                    lastIterator = 0;
                    SuccessfullIterator = 0;
                }
                PlaySound(sounds[2]);
            }


            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    sings.ForEach(obj => { Destroy(obj); });
            //    sings.Clear();
            //    BeatenCells.Clear();
            //    BeatenCellsOwn.Clear();

            //}

        }
        if (successfullyBeatenShips == 20)
        {
            PlaySound(sounds[3]);
            panel.SetActive(true);
            winText.SetActive(true);
            loseText.SetActive(false);
            enabled = false;
        }
        else if (successfullyBeatenShipsOwn == 20)
        {
            PlaySound(sounds[4]);
            panel.SetActive(true);
            winText.SetActive(false);
            loseText.SetActive(true);
            isMissileActive = false;
            enabled = false;
        }
        Debug.Log(successfullyBeatenShipsOwn);
    }
    private int GenerateRndCell()
    {
        int randomCell;
        while (true)
        {
            randomCell = Random.Range(0, 100);
            if (BeatenCellsOwn.Contains(randomCell))
            {
                continue;
            }
            else
            {
                break;
            }
        }
        return randomCell;
    }
}