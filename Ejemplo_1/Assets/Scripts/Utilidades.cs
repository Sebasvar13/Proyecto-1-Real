using packagePersona;
using packagePuntos;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utilidades
{
    [Serializable]
    private class ContainEstudiante
    {
        public List<Estudiante> lista;
    }

    [Serializable]
    private class ContainPunto
    {
        public List<Punto2D> lista;
    }

    public static void GuardarEstudiantes(List<Estudiante> estudiantes)
    {
        string json = JsonUtility.ToJson(new ContainEstudiante { lista = estudiantes }, true);
        string ruta = Path.Combine(Application.streamingAssetsPath, "estudiantes.json");
        File.WriteAllText(ruta, json);
        Debug.Log("Estudiantes guardados en: " + ruta);
    }

    public static void GuardarPuntos(List<Punto2D> puntos)
    {
        string json = JsonUtility.ToJson(new ContainPunto { lista = puntos }, true);
        string ruta = Path.Combine(Application.streamingAssetsPath, "puntos.json");
        File.WriteAllText(ruta, json);
        Debug.Log("Puntos guardados en: " + ruta);
  
    }

}
