using UnityEngine;
using System;

namespace PackagePersona
{
    [Serializable]
    public class Persona
    {
        [SerializeField]
        public string nameP;
        [SerializeField]
        public string mailP;
        [SerializeField]
        public string dirP;

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
