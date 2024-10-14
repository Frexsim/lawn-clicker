using UnityEngine;
using UnityEngine.InputSystem;

public class GrassManager : MonoBehaviour
{
    [SerializeField] Vector2Int lawnSize = new Vector2Int(10, 10);
    [SerializeField] float panSensitivity = 0.1f;

    [SerializeField] GrassTileBase grassTilePrefab;

    GrassTileBase[,] grassTiles;

    bool isPanning = false;

    private void Awake()
    {
        grassTiles = new GrassTileBase[lawnSize.x, lawnSize.y];
    }

    private void Start()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        for (int x = 0; x < lawnSize.x; x++)
        {
            for (int y = 0; y < lawnSize.y; y++)
            {
                grassTiles[x, y] = Instantiate(grassTilePrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    public void OnCutGrass()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f))
        {
            GrassTileBase grassTile = raycastHit.transform.GetComponent<GrassTileBase>();
            if (grassTile != null)
            {
                grassTile.Cut();
            }
        }
    }

    public void OnPan()
    {
        isPanning = InputSystem.actions.FindAction("Pan").WasPressedThisFrame();
    }

    public void OnPanMove(InputValue value)
    {
        if (isPanning)
        {
            Camera.main.transform.position += Camera.main.ScreenToViewportPoint((Vector3) value.Get<Vector2>()) * panSensitivity;
        }
    }
}
