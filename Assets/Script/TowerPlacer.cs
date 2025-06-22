using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour {
    public GameObject towerPrefab;
    public int towerCost = 50;
    public Tilemap restrictedTilemap;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            Vector3Int cellPos = restrictedTilemap.WorldToCell(mouseWorldPos);

            // Cek apakah tile di posisi ini adalah tile jalur (path)
            if (restrictedTilemap.HasTile(cellPos)) {
                Debug.Log("Tidak boleh menaruh tower di jalur!");
                return;
            }

            Vector3 spawnPos = SnapToGrid(mouseWorldPos);
            if (TowerExistsAtPosition(spawnPos)) {
                Debug.Log("Sudah ada tower di sini!");
                return;
            }

            if (GameManager.SpendGold(towerCost)) {
                Instantiate(towerPrefab, SnapToGrid(mouseWorldPos), Quaternion.identity);
            }
            else
            {
                Debug.Log("Gold tidak cukup untuk membeli tower.");
            }
        }
    }

    Vector3 SnapToGrid(Vector3 pos) {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0f);
    }

    bool TowerExistsAtPosition(Vector3 pos) {
        Collider2D hit = Physics2D.OverlapCircle(pos, 0.1f, LayerMask.GetMask("Tower"));
        return hit != null;
    }
}