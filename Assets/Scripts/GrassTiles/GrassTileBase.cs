using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class GrassTileBase : TileBase
{
    [SerializeField] Sprite[] sprites;

    private void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprites[0];

        tileData.transform = this.transform;
        tileData.color = this.color;
        tileData.gameObject = this.gameObject;
        tileData.flags = this.flags;

        tileData.colliderType = this.colliderType;
    }
}
