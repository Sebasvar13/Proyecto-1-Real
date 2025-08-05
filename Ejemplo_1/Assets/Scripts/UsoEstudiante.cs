using UnityEngine;
using System;
using packagePersona;
using packagePuntos;
using System.Collections.Generic;
public class UsoEstudiante : MonoBehaviour
{
    List <Estudiante> listaE=new List <Estudiante>();
    List<Punto2D> listaPuntos = new List<Punto2D>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Estudiante e1 = new Estudiante("2025_03", "Ing multimedia", "David Castro", "dacastro@gmail.com",
            "carrera 34");
        Estudiante e2 = new Estudiante("2025_03", "Ing informatica", "Luis Perez", "luisprz@gmail.com",
            "calle 27");

        listaE.Add(e1);
        listaE.Add(e2);

        for (int i = 0; i < listaE.Count; i++)
        {
            Debug.Log(" " + listaE[i].NameP + "Carrera " + listaE[i].NameCarreraE);
        }

        Utilidades.GuardarEstudiantes(listaE);

        Punto2D p1 = new Punto2D(1.5, 2.3);
        Punto2D p2 = new Punto2D(4.2, 5.7);

        listaPuntos.Add(p1);
        listaPuntos.Add(p2);

        Utilidades.GuardarPuntos(listaPuntos);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
