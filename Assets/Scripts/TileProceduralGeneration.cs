using UnityEngine;
using UnityEngine.Tilemaps; // Importante para trabajar con Tilemaps

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width; // Anchura
    [SerializeField] int height; // Altura
    [SerializeField] Tile dirtTile; // Asigna tus Tiles aquí
    [SerializeField] Tile grassTile;
    [SerializeField] Tile stoneTile;
    [SerializeField] Tilemap tilemap; // Asigna tu Tilemap aquí
    [SerializeField] int minStoneDistance;
    [SerializeField] int maxStoneDistance;

    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            // Modificamos la altura de manera gradual
            int minHeight = height - 1;
            int maxHeight = height + 2;
            int currentHeight = Random.Range(minHeight, maxHeight); // Calculamos la altura

            // Distancia de la altura de la piedra
            int minStoneSpawnDistance = currentHeight - minStoneDistance;
            int maxStoneSpawnDistance = currentHeight - maxStoneDistance;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);
            for (int y = 0; y < currentHeight; y++)
            {
                if (y < totalStoneSpawnDistance)
                {
                    SpawnTile(stoneTile, x, y);
                }
                else
                {
                    SpawnTile(dirtTile, x, y);
                }
            }
            if (totalStoneSpawnDistance == currentHeight)
            {
                SpawnTile(stoneTile, x, currentHeight);
            }
            else
            {
                SpawnTile(grassTile, x, currentHeight); // Considerar ajuste de altura si es necesario
            }
        }
    }

    void SpawnTile(Tile tile, int x, int y)
    {
        Vector3Int cellPosition = new Vector3Int(x, y, 0); // Convertimos a posición de celda
        tilemap.SetTile(cellPosition, tile); // Colocamos la tile en la posición
    }
}
