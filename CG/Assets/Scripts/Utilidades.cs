using System.IO;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using packagePersona;
using packageGeometria;

public static class Utilidades
{
    public static string GuardarEstudiantesJSON(List<Estudiante> lista, string nombreArchivo = "estudiantes.json")
    {
        var data = new EstudiantesData { items = lista };
        string json = JsonUtility.ToJson(data, true);

        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllText(ruta, json, Encoding.UTF8);

        Debug.Log($"[Utilidades] Estudiantes guardados en: {ruta}");
        return ruta;
    }

    public static string GuardarPuntosJSON(List<Punto2D> lista, string nombreArchivo = "puntos2D.json")
    {
        var data = new PuntosData { items = lista };
        string json = JsonUtility.ToJson(data, true);

        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllText(ruta, json, Encoding.UTF8);

        Debug.Log($"[Utilidades] Puntos2D guardados en: {ruta}");
        return ruta;
    }
}

