using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FieldCells : MonoBehaviour
{
    public List<GameObject> EnemyFieldCells = new List<GameObject>();
    public List<GameObject> OwnFieldCells = new List<GameObject>();
    public GameObject CellPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject NewCell = null;
            for (int j = 0; j < 10; j++)
            {
               
                EnemyFieldCells.Add(NewCell = Instantiate(CellPrefab, new Vector3(1.465f + 0.635f * i, 3.097f - 0.655f * j, 0), Quaternion.identity));
                OwnFieldCells.Add(NewCell = Instantiate(CellPrefab, new Vector3(-6.55f + 0.635f * i, 3.095f - 0.655f * j, 0), Quaternion.identity));

            }
           
            
        }
        for (int i = 0;i < EnemyFieldCells.Count;i++) 
        {
            EnemyFieldCells[i].name = i.ToString();
            OwnFieldCells[i].name = "Own" + i.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
