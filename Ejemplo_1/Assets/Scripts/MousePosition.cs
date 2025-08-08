using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PanelMouseTracker : MonoBehaviour
{
    private RectTransform rectTransform;
    private MousePositionData positionData = new MousePositionData();

    [System.Serializable]
    public class MousePositionData
    {
        public List<Vector2> positions = new List<Vector2>();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (IsMouseOverPanel())
        {
            Vector2 localPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Input.mousePosition,
                null,
                out localPos))
            {
                // Agregar la posición actual a la lista
                positionData.positions.Add(localPos);
                Debug.Log($"Position on panel: {localPos}");
            }
        }
    }

    private bool IsMouseOverPanel()
    {
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }

    public void SaveDataManually()
    {
        string json = JsonUtility.ToJson(positionData, true);
        string path = Path.Combine(Application.streamingAssetsPath, "mouse_positions.json");

        // Asegurar que existe el directorio
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        File.WriteAllText(path, json);
        Debug.Log($"Datos guardados en: {path}");
    }
}