using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrassManager : MonoBehaviour
{
    [SerializeField] Vector2Int lawnSize = new Vector2Int(10, 10);
    [SerializeField] float panSensitivity = 0.1f;
    [SerializeField] private float zoomFactor = 0.05f;

    [SerializeField] GrassTileBase grassTilePrefab;
    [SerializeField] GameObject sprinklerSlot;
    [SerializeField] GameObject sprinkler;

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
    public float rakeProvidingIncrease = 1.5f;

    private void Awake()
    {
        grassTiles = new GrassTileBase[lawnSize.x, lawnSize.y];
    }

    private void Start()
    {
        zoomSprinkleSize = sprinkler.transform.localScale;
        sprinkleSpread = FindFirstObjectByType<SprinkleSpread>();
        GenerateTiles();
    }

    private void Update()
    {
        if (sprinkleSpread.isChilded)
        {
            sprinkler.transform.localScale = zoomSprinkleSize;
        }
        else
        {
            sprinkler.transform.localScale = sprinkleSpread.originalSprinkleSize;
        }
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
        Camera.main.orthographicSize -= value.Get<float>() * zoomFactor;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1, 10);

        // Screen edge position
        float cameraRightEdge = Camera.main.aspect * Camera.main.orthographicSize;
        float cameraTopEdge = Camera.main.orthographicSize;

        // original sprinkler slot scale
        sprinklerSlot.transform.localScale = Vector3.one; // Dont care about this until the sprinkler item slot is done

        // Sprinkler slot position at the upper right corner
        Vector3 slotHalfSize = sprinklerSlot.GetComponent<Renderer>().bounds.extents;
        sprinklerSlot.transform.position = Camera.main.transform.position + new Vector3(cameraRightEdge - slotHalfSize.x, cameraTopEdge - slotHalfSize.y, 10f);

        // If childed, apply the zoom scaling size
        if (sprinkleSpread.isChilded)
        {
            float scaleFactor = Camera.main.orthographicSize / 10f; // Scale based on camera zoom
            sprinkler.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
        else
        {
            sprinkler.transform.localScale = sprinkleSpread.originalSprinkleSize;
        }
    }
}
