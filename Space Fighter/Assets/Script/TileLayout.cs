using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//[ExecuteAlways]
public class TileLayout : MonoBehaviour
{

    [SerializeField] private GameObject tile;
    [Range(0,20)]
    [SerializeField] private int tilesRow, tilesCol;



    private void Start()
    {
        

        for(int j = 0; j < tilesCol; j++)
        {
            for(int i = 0; i < tilesRow; i++)
            {
                GameObject newTile = Instantiate(tile, transform.position + new Vector3(i,j), transform.rotation);
            }
        }
    }
}
