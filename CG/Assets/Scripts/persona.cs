using System;
using UnityEngine;

namespace packagePersona
{
    [Serializable]
    public class Persona
    {
        [SerializeField] private string nombre;
        [SerializeField] private string EmailP;
        [SerializeField] private string Direct;

        public Persona() { }

        public Persona(string nombre, string emailP, string direct)
        {
            this.nombre = nombre;
            EmailP = emailP;
            Direct = direct;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string EmailP1 { get => EmailP; set => EmailP = value; }
        public string Direct1 { get => Direct; set => Direct = value; }
    }
}
