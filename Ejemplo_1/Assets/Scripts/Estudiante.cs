using UnityEngine;
using System;

namespace packagePersona
{

    [Serializable]
    public class Estudiante : Persona
    {
        [SerializeField] private string codeE;
        [SerializeField] private string nameCarreraE;

        public Estudiante() 
        { 
        }

        public Estudiante(string codeE, string nameCarreraE, string nameP, string mailP, string dirP)
            : base(nameP, mailP, dirP)
        {
            this.codeE = codeE;
            this.nameCarreraE = nameCarreraE;
        }

        public string CodeE { get => codeE; set => codeE = value; }
        public string NameCarreraE { get => nameCarreraE; set => nameCarreraE = value; }
    }




}