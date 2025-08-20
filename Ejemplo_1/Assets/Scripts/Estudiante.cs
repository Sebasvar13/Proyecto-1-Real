using UnityEngine;
using System;

namespace PackagePersona
{
    [Serializable]
    public class Estudiante : Persona
    {
        [SerializeField]
        public string codeE;
        [SerializeField]
        public string nameCarrera;

        public Estudiante()
        {
        }

        public Estudiante(string codeE, string nameCarrera, string nameP, string mailP, string dirP)
        : base( nameP,  mailP,  dirP)
        {
            
            this.codeE = codeE;
            this.nameCarrera = nameCarrera;
        }
        
        public string CodeE { get => codeE; set => codeE = value; }
        public string NameCarrera { get => nameCarrera; set => nameCarrera = value; }
    }
}
