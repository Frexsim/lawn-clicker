using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrassManager : MonoBehaviour
{
    [SerializeField] Vector2Int lawnSize = new Vector2Int(10, 10);
    [SerializeField] float panSensitivity = 0.1f;
    [SerializeField] private float zoomFactor = 0.05f;

    [SerializeField] GrassTileBase grassTilePrefab;

    GrassTileBase[,] grassTiles;
    SprinkleSpread sprinkleSpread;

    bool isPanning = false;

    public TMP_Text moneyYouHave;
    public float money;
    public float moneyGain;
    public float defaultGrassMoney;

    Vector3 zoomSprinkleSize;

    [Header("Upgrades")]
    public float rakeIncrease;
    public float slaveGain;
    public float cutterIncrease;
    public float rakeProviding = 1f;
    public float rakeProvidingIncrease = 1.6f;

    private void Awake()
    {
        grassTiles = new GrassTileBase[lawnSize.x, lawnSize.y];
    }

    private void Start()
    {
        sprinkleSpread = FindFirstObjectByType<SprinkleSpread>();
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
            Camera.main.transform.position += (Vector3) value.Get<Vector2>() / Camera.main.pixelHeight * Camera.main.orthographicSize;
        }
    }

    public void OnZoom(InputValue value)
    {
        
    }
}
