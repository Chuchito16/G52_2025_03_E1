using UnityEngine;
using System;


namespace packagePersona
{
    [Serializable]
    public class Estudiante : Persona
    {
        private string codigoE;
        private string nameCarreraE;
    }
    public Estudiante()
        {
        }

        public Estudiante(string codigoE, string nameCarreraE, string nombre, string emailP, string direct)
            : base(nombre, emailP, direct)
        {
            this.codigoE = codigoE;
            this.nameCarreraE = nameCarreraE;
        }

        public string CodigoE { get => codigoE; set => codigoE = value; }
        public string NameCarreraE { get => nameCarreraE; set => nameCarreraE = value; }
    }