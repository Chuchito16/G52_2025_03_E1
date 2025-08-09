using UnityEngine;
using System;
using packagePersona;
using packageGeometria;   // <-- importa el paquete de Punto2D
using System.Collections.Generic;

public class UsaEstudiante : MonoBehaviour
{
    List<Estudiante> listaE = new List<Estudiante>();
    List<Punto2D> listaP = new List<Punto2D>();

    void Start()
    {
        // Estudiantes
        Estudiante e1 = new Estudiante("2025_03", "Ing multimedia", "David Castro", "dacastro@uao.edu.co", "carrera 34");
        Estudiante e2 = new Estudiante("2023_03", "Ing Ambiental", "Maria Perez", "merez@uao.edu.co", "calle 14");
        listaE.Add(e1);
        listaE.Add(e2);

        for (int i = 0; i < listaE.Count; i++)
        {
            Debug.Log("name " + listaE[i].Nombre + " Carrera " + listaE[i].NameCarreraE);
        }

        // Puntos 2D de ejemplo
        listaP.Add(new Punto2D(10, 5));
        listaP.Add(new Punto2D(-2.5, 3.75));

        // Guardar JSON
        Utilidades.GuardarEstudiantesJSON(listaE);  
        Utilidades.GuardarPuntosJSON(listaP);       
    }
}
