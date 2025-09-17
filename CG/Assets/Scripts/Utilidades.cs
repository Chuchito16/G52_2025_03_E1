using UnityEngine;
using UnityEngine.Networking;          // Para leer StreamingAssets en Android
using System.Collections;
using System.Collections.Generic;
using System.IO;
using packagePersona;
using packageGeometria;

public static class Utilidades
{
    // Nombres de archivo
    private const string EstudiantesFile = "estudiantes.json";
    private const string PuntosFile = "puntos.json";

    // Rutas semilla (solo lectura)
    private static string SeedE => Path.Combine(Application.streamingAssetsPath, EstudiantesFile);
    private static string SeedP => Path.Combine(Application.streamingAssetsPath, PuntosFile);

    // Rutas de trabajo (lectura/escritura)
    public static string PathE => Path.Combine(Application.persistentDataPath, EstudiantesFile);
    public static string PathP => Path.Combine(Application.persistentDataPath, PuntosFile);

    [System.Serializable] class EstudiantesWrap { public List<Estudiante> items; }
    [System.Serializable] class PuntosWrap { public List<Punto2D> items; }

    /// <summary>
    /// Copia los JSON de StreamingAssets a persistentDataPath si NO existen.
    /// Llama con StartCoroutine(Utilidades.EnsureSeedFiles()) antes de leer.
    /// </summary>
    public static IEnumerator EnsureSeedFiles()
    {
        // Asegura carpeta destino
        Directory.CreateDirectory(Application.persistentDataPath);

        if (!File.Exists(PathE))
            yield return CopySeedToPersistent(SeedE, PathE);

        if (!File.Exists(PathP))
            yield return CopySeedToPersistent(SeedP, PathP);
    }

    /// <summary>
    /// Fuerza reimportar desde StreamingAssets (sobrescribe).
    /// Útil si editaste a mano los JSON semilla y quieres “resetear”.
    /// </summary>
    public static IEnumerator ResetFromStreamingAssets()
    {
        yield return CopySeedToPersistent(SeedE, PathE, overwrite: true);
        yield return CopySeedToPersistent(SeedP, PathP, overwrite: true);
    }

    // Copia compatible con Android/Editor/PC
    private static IEnumerator CopySeedToPersistent(string src, string dst, bool overwrite = false)
    {
        if (File.Exists(dst) && !overwrite) yield break;

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var req = UnityWebRequest.Get(src))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"No se pudo leer {src} desde StreamingAssets: {req.error}");
                yield break;
            }
            File.WriteAllBytes(dst, req.downloadHandler.data);
        }
#else
        // En Editor/PC/Mac/Linux puedes leer directamente el archivo.
        try
        {
            File.Copy(src, dst, overwrite);
        }
        catch (IOException io)
        {
            // Si falló por overwrite=false y el archivo ya existe, ignoramos
            if (!File.Exists(dst)) Debug.LogError(io);
        }
        yield break;
#endif
    }

    // ------------ Guardar / Leer (siempre en persistentDataPath) ------------
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

    // Abre la carpeta de datos para que el usuario edite a mano si quiere
    public static void AbrirCarpetaDatos()
    {
        Application.OpenURL("file://" + Application.persistentDataPath);
    }
}

