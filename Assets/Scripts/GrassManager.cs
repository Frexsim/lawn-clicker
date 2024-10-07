using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;



    void OnCutGrass()
    {
        Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Tile tile = tilemap.GetTile<Tile>(tilemapPos);

        Debug.Log(tilemapPos);
    }
}
