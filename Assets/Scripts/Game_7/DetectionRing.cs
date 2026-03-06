using UnityEngine;

// Automatikusan hozzáadja a LineRenderer komponenst, ha még nincs az objektumon
[RequireComponent(typeof(LineRenderer))]
public class DetectionRing : MonoBehaviour
{
    private EnemyAI _enemyAI;   // Referencia az ellenség MI-jére a hatótáv lekéréséhez
    private LineRenderer _line; // A kört ténylegesen kirajzoló komponens
    public int segments = 50;   // A kör részletessége (hány egyenes szakaszból álljon)

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _enemyAI = GetComponent<EnemyAI>();

        // A vonalbeállítások konfigurálása a kód alapú rajzoláshoz
        _line.useWorldSpace = false;      // Az objektumhoz képest (lokálisan) rajzolunk
        _line.positionCount = segments + 1;
        _line.startWidth = 0.1f;
        _line.endWidth = 0.1f;
        _line.loop = true;                // Összeköti az elejét a végével

        // Alapértelmezett anyag beállítása, ha a szerkesztõben maradt volna
        if (_line.material == null)
        {
            _line.material = new Material(Shader.Find("Sprites/Default"));
        }
    }

    void Update()
    {
        DrawCircle(); // Minden képkockánál újrarajzoljuk a kört
    }

    // A kör matematikai kiszámítása és megjelenítése
    void DrawCircle()
    {
        float radius = _enemyAI.detectionRange; // Lekérjük az MI aktuális látótávolságát
        float angle = 0f;

        // Színváltás az ellenség állapota alapján: üldözésnél piros, õrjáratnál sárga
        Color ringColor = (_enemyAI.currentState == EnemyAI.AIState.Chase) ? Color.red : Color.yellow;
        _line.startColor = ringColor;
        _line.endColor = ringColor;

        // A kör pontjainak kiszámítása trigonometriával (Sin, Cos)
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            // A pontok elhelyezése (y: -0.98f a padló feletti közvetlen megjelenítéshez)
            _line.SetPosition(i, new Vector3(x, -0.98f, z));

            angle += (360f / segments);
        }
    }
}