using packagePersona;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace packagePuntos
{
    [System.Serializable]
    public class Punto2D : MonoBehaviour
    {
        [System.NonSerialized]
        List<Punto2D> listaE = new List<Punto2D>();
        
        
        public TMP_InputField ValorX;
        public TMP_InputField ValorY;

        [SerializeField] private double X;
        [SerializeField] private double Y;

        public Punto2D()
        {
        }

        public Punto2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X1 { get => X; set => X = value; }
        public double Y1 { get => Y; set => Y = value; }

        public void AddPointList()

        {
            double valorX1 = Convert.ToDouble(ValorX.text);
            double valorY1 = Convert.ToDouble(ValorY.text);
            
            Punto2D a1 = new Punto2D(valorX1, valorY1);

            listaE.Add(a1);

            Utilidades.GuardarPuntos(listaE);


            Debug.Log($"Puntos agregados: {valorX1} - {valorY1}");


        }



    }
}
