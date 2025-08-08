using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;

public class Position : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool MouseEncima = false;
    private List<Vector2> mousePositions = new List<Vector2>();
    private string filePath;

    private void Start()
    {
        
        filePath = Path.Combine(Application.streamingAssetsPath, "mousePositions.json");

        
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MousePositionData data = JsonUtility.FromJson<MousePositionData>(jsonData);
            mousePositions = data.positions;
        }
    }

    private void Update()
    {
        if (MouseEncima)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePositions.Add(mousePosition);
            print("Mouse sobre el panel: " + mousePosition);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEncima = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseEncima = false;
        GuardarJson();
    }

    private void GuardarJson()
    {
        
        MousePositionData data = new MousePositionData();
        data.positions = mousePositions;

        
        string jsonData = JsonUtility.ToJson(data, true);

        
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Datos guardados en: " + filePath);
    }

    
    [System.Serializable]
    private class MousePositionData
    {
        public List<Vector2> positions;
    }

   
}