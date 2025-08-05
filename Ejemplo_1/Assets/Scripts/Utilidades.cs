using System.Collections.Generic;
using System.IO;
using UnityEngine;
using packagePersona;
using packagePuntos;

public static class Utilidades
{
    public static void GuardarEstudiantes(List<Estudiante> estudiantes)
    {
        string json = JsonUtility.ToJson(new WrapperEstudiante { lista = estudiantes }, true);
        string ruta = Path.Combine(Application.persistentDataPath, "estudiantes.json");
        File.WriteAllText(ruta, json);
        Debug.Log("Estudiantes guardados en: " + ruta);
    }

    public static void GuardarPuntos(List<Punto2D> puntos)
    {
        string json = JsonUtility.ToJson(new WrapperPunto { lista = puntos }, true);
        string ruta = Path.Combine(Application.persistentDataPath, "puntos.json");
        File.WriteAllText(ruta, json);
        Debug.Log("Puntos guardados en: " + ruta);
  
    }

    // Wrappers para que Unity serialice listas
    [System.Serializable]
    private class WrapperEstudiante
    {
        public List<Estudiante> lista;
    }

    [System.Serializable]
    private class WrapperPunto
    {
        public List<Punto2D> lista;
    }
}
