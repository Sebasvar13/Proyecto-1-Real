using UnityEngine;
using System;


namespace packagePersona
{
    [Serializable]
    public class Persona
    {

        [SerializeField] private string nameP;
        [SerializeField] private string mailP;
        [SerializeField] private string dirP;

        public Persona()
        {
        }

        public Persona(string nameP, string mailP, string dirP)
        {
            this.nameP = nameP;
            this.mailP = mailP;
            this.dirP = dirP;
        }

        public string NameP { get => nameP; set => nameP = value; }
        public string MailP { get => mailP; set => mailP = value; }
        public string DirP { get => dirP; set => dirP = value; }
    }

    
         



   
}