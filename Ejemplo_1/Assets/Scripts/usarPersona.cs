using UnityEngine;
using PackagePersona;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class UsarPersona : MonoBehaviour
{
    
    List<Estudiante> listaE = new List<Estudiante>();
    List<Estudiante> listaIngenieros = new List<Estudiante>();
    List<Estudiante> listaOtrasCarreras = new List<Estudiante>();

    
    public TMP_InputField nameStudent;
    public TMP_InputField mailStudent;
    public TMP_InputField dirStudent;
    public TMP_InputField CodeStudent;
    public TMP_InputField carreraStudent;

    
    public TMP_InputField posicionInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        
        loadDataEstudiantes();

        
        SepararTodasLasListas();

        
        VerificarYCrearArchivosJSON();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    
    private void VerificarYCrearArchivosJSON()
    {
        Debug.Log("=== VERIFICANDO ARCHIVOS JSON ===");

        
        if (!Utilidades.FileExists("datosEstudiante.json"))
        {
            Debug.Log("Archivo datosEstudiante.json no existe, creando...");
            Utilidades.SaveDataStudent(listaE);
        }
        else
        {
            Debug.Log("Archivo datosEstudiante.json ya existe");
        }

        
        if (!Utilidades.FileExists("datosIngenieros.json"))
        {
            Debug.Log("Archivo datosIngenieros.json no existe, creando...");
            Utilidades.SaveDataIngenieros(listaIngenieros);
        }
        else
        {
            Debug.Log("Archivo datosIngenieros.json ya existe");
        }

        
        if (!Utilidades.FileExists("datosOtrasCarreras.json"))
        {
            Debug.Log("Archivo datosOtrasCarreras.json no existe, creando...");
            Utilidades.SaveDataOtrasCarreras(listaOtrasCarreras);
        }
        else
        {
            Debug.Log("Archivo datosOtrasCarreras.json ya existe");
        }

        Debug.Log("=== VERIFICACIÓN COMPLETADA ===");
    }

    
    public void loadDataEstudiantes()
    {
        string filePath = Path.Combine(GetStreamingAssetsPath(), "Estudiantes.txt");
        string fileContent = "";

        Debug.Log("Buscando archivo en: " + filePath);

        if (File.Exists(filePath))
        {
            try
            {
                fileContent = File.ReadAllText(filePath);
                Debug.Log("Contenido del archivo: " + fileContent);

                StringReader reader = new StringReader(fileContent);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineaEstudiante = line.Split(',');
                    Estudiante e = new Estudiante(lineaEstudiante[3], lineaEstudiante[4],
                        lineaEstudiante[0], lineaEstudiante[1], lineaEstudiante[2]);

                    Debug.Log("Persona leida " + e.NameP + " " + e.NameCarrera);
                    listaE.Add(e); 
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error al leer el archivo: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("El archivo no existe en: " + filePath);
        }
    }

   
    private string GetStreamingAssetsPath()
    {
        
#if UNITY_EDITOR
        return Path.Combine(Application.dataPath, "StreamingAssets");
#else
        // En build, usamos la ruta estándar
        return Application.streamingAssetsPath;
#endif
    }

    
    public void AddStudentList()
    {
        string nameStudent1 = nameStudent.text;
        string mailStudent1 = mailStudent.text;
        string dirStudent1 = dirStudent.text;
        string codeStudent1 = CodeStudent.text;
        string carreraS1 = carreraStudent.text;

        
        if (!Utilidades.ValidarDatosEstudiante(nameStudent1, mailStudent1, dirStudent1, codeStudent1, carreraS1))
        {
            Debug.LogError("Datos del estudiante no válidos");
            return;
        }

        Estudiante e1 = new Estudiante(codeStudent1, carreraS1, nameStudent1, mailStudent1, dirStudent1);

        listaE.Add(e1);

        
        SepararEstudianteEnListas(e1);

        
        GuardarListasAdicionales();
    }

   
    public void MostrarEstudiantesIngenieria()
    {
        Debug.Log("=== ESTUDIANTES DE INGENIERÍA ===");
        int contador = 0;
        for (int i = 0; i < listaE.Count; i++)
        {
            if (EsIngenieria(listaE[i].NameCarrera))
            {
                Debug.Log("Ingeniero: " + listaE[i].NameP + " - " + listaE[i].NameCarrera);
                contador++;
            }
        }
        Debug.Log($"Total ingenieros encontrados: {contador}");
    }

    
    public void AddStudentAtPosition()
    {
        if (string.IsNullOrEmpty(posicionInput.text))
        {
            Debug.LogError("Debe ingresar una posición");
            return;
        }

        if (!int.TryParse(posicionInput.text, out int posicion))
        {
            Debug.LogError("La posición debe ser un número");
            return;
        }

        string nameStudent1 = nameStudent.text;
        string mailStudent1 = mailStudent.text;
        string dirStudent1 = dirStudent.text;
        string codeStudent1 = CodeStudent.text;
        string carreraS1 = carreraStudent.text;

        
        if (!Utilidades.ValidarDatosEstudiante(nameStudent1, mailStudent1, dirStudent1, codeStudent1, carreraS1))
        {
            Debug.LogError("Datos del estudiante no válidos");
            return;
        }

        Estudiante e1 = new Estudiante(codeStudent1, carreraS1, nameStudent1, mailStudent1, dirStudent1);

       
        if (posicion < 0) posicion = 0;
        if (posicion > listaE.Count) posicion = listaE.Count;

        listaE.Insert(posicion, e1);
        Debug.Log($"Estudiante agregado en posición {posicion}: " + e1.NameP);

        
        SepararEstudianteEnListas(e1);

        
        GuardarListasAdicionales();
    }

   
    public void AddStudentAtBeginning()
    {
        string nameStudent1 = nameStudent.text;
        string mailStudent1 = mailStudent.text;
        string dirStudent1 = dirStudent.text;
        string codeStudent1 = CodeStudent.text;
        string carreraS1 = carreraStudent.text;

       
        if (!Utilidades.ValidarDatosEstudiante(nameStudent1, mailStudent1, dirStudent1, codeStudent1, carreraS1))
        {
            Debug.LogError("Datos del estudiante no válidos");
            return;
        }

        Estudiante e1 = new Estudiante(codeStudent1, carreraS1, nameStudent1, mailStudent1, dirStudent1);

        listaE.Insert(0, e1);
        Debug.Log("Estudiante agregado al inicio: " + e1.NameP);

       
        SepararEstudianteEnListas(e1);

      
        GuardarListasAdicionales();
    }

    
    private void SepararEstudianteEnListas(Estudiante estudiante)
    {
        if (EsIngenieria(estudiante.NameCarrera))
        {
            listaIngenieros.Add(estudiante);
            Debug.Log($"Agregado a lista de ingenieros: {estudiante.NameP}");
        }
        else
        {
            listaOtrasCarreras.Add(estudiante);
            Debug.Log($"Agregado a lista de otras carreras: {estudiante.NameP}");
        }
    }


    private bool EsIngenieria(string carrera)
    {
        return Utilidades.EsCarreraIngenieria(carrera);
    }


    public void SepararTodasLasListas()
    {
        listaIngenieros.Clear();
        listaOtrasCarreras.Clear();

        for (int i = 0; i < listaE.Count; i++)
        {
            if (EsIngenieria(listaE[i].NameCarrera))
            {
                listaIngenieros.Add(listaE[i]);
                Debug.Log($"Agregado a ingenieros: {listaE[i].NameP} - {listaE[i].NameCarrera}");
            }
            else
            {
                listaOtrasCarreras.Add(listaE[i]);
                Debug.Log($"Agregado a otras carreras: {listaE[i].NameP} - {listaE[i].NameCarrera}");
            }
        }

        Debug.Log($"SEPARACIÓN COMPLETADA - Ingenieros: {listaIngenieros.Count}, Otras: {listaOtrasCarreras.Count}");
    }


    public void ShowStudentList()
    {
        Debug.Log("=== LISTA COMPLETA ===");
        for (int i = 0; i < listaE.Count; i++)
        {
            Debug.Log($"{i}: {listaE[i].NameP} - {listaE[i].NameCarrera}");
        }

        Debug.Log("=== LISTA INGENIEROS ===");
        for (int i = 0; i < listaIngenieros.Count; i++)
        {
            Debug.Log($"{i}: {listaIngenieros[i].NameP} - {listaIngenieros[i].NameCarrera}");
        }

        Debug.Log("=== LISTA OTRAS CARRERAS ===");
        for (int i = 0; i < listaOtrasCarreras.Count; i++)
        {
            Debug.Log($"{i}: {listaOtrasCarreras[i].NameP} - {listaOtrasCarreras[i].NameCarrera}");
        }

       
        GuardarTodasLasListas();
    }

   
    public void GuardarTodasLasListas()
    {
        
        SepararTodasLasListas();

      
        bool salvar = Utilidades.SaveAllStudentLists(listaE, listaIngenieros, listaOtrasCarreras);

        if (salvar)
        {
            Debug.Log("Todas las listas se guardaron correctamente");
        }
        else
        {
            Debug.LogError("Error al guardar algunas listas");
        }
    }

 
    public void GuardarListasAdicionales()
    {
     
        SepararTodasLasListas();

        GuardarTodasLasListas();
    }

}