using UnityEngine;
using System.Collections.Generic;
using System.IO;
using packagePersona;
using packageGeometria;

public class Utilidades : MonoBehaviour
{
    
    [SerializeField] UIEstudiantePunto uiCtrl;  

    
    public void GuardarJSON()
    {
        GuardarEstudiantesJSON(uiCtrl.listaE);
        GuardarPuntosJSON(uiCtrl.listaP);
        Debug.Log("Guardado OK (Estudiantes y Puntos).");
    }

    public void GuardarSoloEstudiantes() => GuardarEstudiantesJSON(uiCtrl.listaE);
    public void GuardarSoloPuntos() => GuardarPuntosJSON(uiCtrl.listaP);

    
    [System.Serializable] class EstudiantesWrap { public List<Estudiante> items; }
    [System.Serializable] class PuntosWrap { public List<Punto2D> items; }

    static string PathE => Path.Combine(Application.persistentDataPath, "estudiantes.json");
    static string PathP => Path.Combine(Application.persistentDataPath, "puntos.json");

    public static void GuardarEstudiantesJSON(List<Estudiante> lista)
    {
        var wrap = new EstudiantesWrap { items = lista };
        File.WriteAllText(PathE, JsonUtility.ToJson(wrap, true));
        Debug.Log($"Estudiantes -> {PathE}");
    }

    public static List<Estudiante> LeerEstudiantesJSON()
    {
        if (!File.Exists(PathE)) return new List<Estudiante>();
        var wrap = JsonUtility.FromJson<EstudiantesWrap>(File.ReadAllText(PathE));
        return wrap?.items ?? new List<Estudiante>();
    }

    public static void GuardarPuntosJSON(List<Punto2D> lista)
    {
        var wrap = new PuntosWrap { items = lista };
        File.WriteAllText(PathP, JsonUtility.ToJson(wrap, true));
        Debug.Log($"Puntos -> {PathP}");
    }

    public static List<Punto2D> LeerPuntosJSON()
    {
        if (!File.Exists(PathP)) return new List<Punto2D>();
        var wrap = JsonUtility.FromJson<PuntosWrap>(File.ReadAllText(PathP));
        return wrap?.items ?? new List<Punto2D>();
    }
}

