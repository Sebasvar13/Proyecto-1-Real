using UnityEngine;
using System;
using System.Collections.Generic;
using PackagePersona;
using System.Runtime.Serialization;
using System.IO;
using System.Linq;

public class Utilidades
{
    
    private static string GetStreamingAssetsPath()
    {
       

        return Path.Combine(Application.dataPath, "StreamingAssets");

        
        

    }

    
    private static bool EnsureStreamingAssetsDirectory()
    {
        try
        {
            string folderPath = GetStreamingAssetsPath();

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Carpeta StreamingAssets creada en: " + folderPath);


                
                UnityEditor.AssetDatabase.Refresh();

            }
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al crear carpeta StreamingAssets: " + ex.Message);
            return false;
        }
    }

    
    public static bool SaveDataStudent(List<Estudiante> listaE)
    {
        return SaveEstudiantesToJson(listaE, "datosEstudiante.json", "Lista completa de estudiantes");
    }

    
    public static bool SaveDataIngenieros(List<Estudiante> listaIngenieros)
    {
        return SaveEstudiantesToJson(listaIngenieros, "datosIngenieros.json", "Lista de ingenieros");
    }

    
    public static bool SaveDataOtrasCarreras(List<Estudiante> listaOtrasCarreras)
    {
        return SaveEstudiantesToJson(listaOtrasCarreras, "datosOtrasCarreras.json", "Lista de otras carreras");
    }

    
    private static bool SaveEstudiantesToJson(List<Estudiante> lista, string nombreArchivo, string descripcion)
    {
        try
        {
            
            if (!EnsureStreamingAssetsDirectory())
            {
                Debug.LogError($"No se pudo crear la carpeta StreamingAssets para {descripcion}");
                return false;
            }

            
            string jsonString = JsonUtility.ToJson(new EstudianteListWrapper { estudiantes = lista }, true);
            string folderPath = GetStreamingAssetsPath();
            string filePath = Path.Combine(folderPath, nombreArchivo);

            
            File.WriteAllText(filePath, jsonString);

            Debug.Log($"{descripcion} guardada correctamente en: {filePath} ({lista.Count} estudiantes)");


            
            UnityEditor.AssetDatabase.Refresh();


            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al guardar {descripcion}: {ex.Message}");
            return false;
        }
    }

    
    public static bool FileExists(string nombreArchivo)
    {
        string folderPath = GetStreamingAssetsPath();
        string filePath = Path.Combine(folderPath, nombreArchivo);
        return File.Exists(filePath);
    }

    
    public static List<Estudiante> LoadEstudiantesFromJson(string nombreArchivo)
    {
        try
        {
            string folderPath = GetStreamingAssetsPath();
            string filePath = Path.Combine(folderPath, nombreArchivo);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Archivo {nombreArchivo} no existe en: {filePath}");
                return new List<Estudiante>();
            }

            string jsonString = File.ReadAllText(filePath);
            EstudianteListWrapper wrapper = JsonUtility.FromJson<EstudianteListWrapper>(jsonString);

            if (wrapper != null && wrapper.estudiantes != null)
            {
                Debug.Log($"Cargados {wrapper.estudiantes.Count} estudiantes desde {nombreArchivo}");
                return wrapper.estudiantes;
            }
            else
            {
                Debug.LogWarning($"El archivo {nombreArchivo} no contiene datos válidos");
                return new List<Estudiante>();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al cargar estudiantes desde {nombreArchivo}: {ex.Message}");
            return new List<Estudiante>();
        }
    }

    
    public static bool SaveAllStudentLists(List<Estudiante> listaCompleta,
                                         List<Estudiante> listaIngenieros,
                                         List<Estudiante> listaOtrasCarreras)
    {
        
        if (!EnsureStreamingAssetsDirectory())
        {
            Debug.LogError("No se pudo crear la carpeta StreamingAssets");
            return false;
        }

        bool exito1 = SaveDataStudent(listaCompleta);
        bool exito2 = SaveDataIngenieros(listaIngenieros);
        bool exito3 = SaveDataOtrasCarreras(listaOtrasCarreras);

        bool exitoTotal = exito1 && exito2 && exito3;

        if (exitoTotal)
        {
            Debug.Log("=== TODAS LAS LISTAS GUARDADAS EXITOSAMENTE ===");
        }
        else
        {
            Debug.LogError("=== ERROR AL GUARDAR ALGUNAS LISTAS ===");
        }

        return exitoTotal;
    }

    
    public static List<Estudiante> FiltrarEstudiantesIngenieria(List<Estudiante> listaCompleta)
    {
        List<Estudiante> ingenieros = new List<Estudiante>();

        foreach (Estudiante estudiante in listaCompleta)
        {
            if (EsCarreraIngenieria(estudiante.NameCarrera))
            {
                ingenieros.Add(estudiante);
            }
        }

        Debug.Log($"Filtrados {ingenieros.Count} estudiantes de ingeniería de un total de {listaCompleta.Count}");
        return ingenieros;
    }

    
    public static List<Estudiante> FiltrarEstudiantesOtrasCarreras(List<Estudiante> listaCompleta)
    {
        List<Estudiante> otrasCarreras = new List<Estudiante>();

        foreach (Estudiante estudiante in listaCompleta)
        {
            if (!EsCarreraIngenieria(estudiante.NameCarrera))
            {
                otrasCarreras.Add(estudiante);
            }
        }

        Debug.Log($"Filtrados {otrasCarreras.Count} estudiantes de otras carreras de un total de {listaCompleta.Count}");
        return otrasCarreras;
    }

    
    public static void SepararListasPorCarrera(List<Estudiante> listaCompleta,
                                             out List<Estudiante> listaIngenieros,
                                             out List<Estudiante> listaOtrasCarreras)
    {
        listaIngenieros = new List<Estudiante>();
        listaOtrasCarreras = new List<Estudiante>();

        foreach (Estudiante estudiante in listaCompleta)
        {
            if (EsCarreraIngenieria(estudiante.NameCarrera))
            {
                listaIngenieros.Add(estudiante);
            }
            else
            {
                listaOtrasCarreras.Add(estudiante);
            }
        }

        Debug.Log($"Separación automática completada:");
        Debug.Log($"- Ingenieros: {listaIngenieros.Count}");
        Debug.Log($"- Otras carreras: {listaOtrasCarreras.Count}");
    }

    
    public static bool EsCarreraIngenieria(string carrera)
    {
        if (string.IsNullOrEmpty(carrera)) return false;

        string carreraLower = carrera.ToLower();
        return carreraLower.Contains("ingenieria") ||
               carreraLower.Contains("ingeniería") ||
               carreraLower.Contains("engineer");
    }

    
    public static bool ValidarDatosEstudiante(string nombre, string email, string direccion, string codigo, string carrera)
    {
        if (string.IsNullOrEmpty(nombre))
        {
            Debug.LogError("El nombre del estudiante no puede estar vacío");
            return false;
        }

        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("El email del estudiante no puede estar vacío");
            return false;
        }

        if (string.IsNullOrEmpty(direccion))
        {
            Debug.LogError("La dirección del estudiante no puede estar vacía");
            return false;
        }

        if (string.IsNullOrEmpty(codigo))
        {
            Debug.LogError("El código del estudiante no puede estar vacío");
            return false;
        }

        if (string.IsNullOrEmpty(carrera))
        {
            Debug.LogError("La carrera del estudiante no puede estar vacía");
            return false;
        }

        return true;
    }

    
    public static void MostrarListaEstudiantes(List<Estudiante> lista, string tituloLista)
    {
        Debug.Log($"=== {tituloLista.ToUpper()} ({lista.Count} estudiantes) ===");

        for (int i = 0; i < lista.Count; i++)
        {
            Debug.Log($"{i + 1}. {lista[i].NameP} | {lista[i].NameCarrera} | Código: {lista[i].CodeE}");
        }

        if (lista.Count == 0)
        {
            Debug.Log("(Lista vacía)");
        }
    }

    
    public static bool AgregarEstudianteEnPosicion(List<Estudiante> lista, Estudiante estudiante, int posicion)
    {
        try
        {
            if (posicion < 0)
            {
                Debug.LogWarning($"Posición {posicion} no válida, agregando al inicio");
                posicion = 0;
            }
            else if (posicion > lista.Count)
            {
                Debug.LogWarning($"Posición {posicion} mayor al tamaño de la lista, agregando al final");
                posicion = lista.Count;
            }

            lista.Insert(posicion, estudiante);
            Debug.Log($"Estudiante {estudiante.NameP} agregado en posición {posicion}");
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al agregar estudiante en posición {posicion}: {ex.Message}");
            return false;
        }
    }

    
    public static bool SaveDataPuntos(List<Punto2D> listaP)
    {
        try
        {
            
            if (!EnsureStreamingAssetsDirectory())
            {
                Debug.LogError("No se pudo crear la carpeta StreamingAssets para puntos");
                return false;
            }

            string jsonString = JsonUtility.ToJson(new PuntosListWrapper { puntos = listaP }, true);
            string folderPath = GetStreamingAssetsPath();
            string filePath = Path.Combine(folderPath, "datosPuntos.json");

            
            File.WriteAllText(filePath, jsonString);

            Debug.Log("Archivo JSON de puntos guardado correctamente en: " + filePath);


            
            UnityEditor.AssetDatabase.Refresh();


            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al guardar archivo JSON de puntos: " + ex.Message);
            return false;
        }
    }
}


[Serializable]
public class EstudianteListWrapper
{
    public List<Estudiante> estudiantes;
}

[Serializable]
public class PuntosListWrapper
{
    public List<Punto2D> puntos;
}