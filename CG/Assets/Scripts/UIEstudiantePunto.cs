using UnityEngine;
using UnityEngine.UI;          // Botones
using TMPro;                   // TMP_InputField, TMP_Text
using System.Collections.Generic;
using System.Globalization;
using packagePersona;
using packageGeometria;

public class UIEstudiantePunto : MonoBehaviour
{
    // ====== Inputs Estudiante (TMP) ======
    [Header("Estudiante - Inputs (TMP)")]
    [SerializeField] TMP_InputField nombreIF;
    [SerializeField] TMP_InputField emailIF;
    [SerializeField] TMP_InputField direccionIF;
    [SerializeField] TMP_InputField codigoIF;
    [SerializeField] TMP_InputField carreraIF;

    // ====== Inputs Punto2D (TMP) ======
    [Header("Puntos - Inputs (TMP)")]
    [SerializeField] TMP_InputField puntoXIF;
    [SerializeField] TMP_InputField puntoYIF;

    // ====== Textos de salida (TMP) ======
    [Header("Salidas (TMP Text)")]
    [SerializeField] TMP_Text listaEstudiantesTXT;
    [SerializeField] TMP_Text listaPuntosTXT;

    // ====== Botones ======
    [Header("Botones")]
    [SerializeField] Button agregarEstudianteBtn;
    [SerializeField] Button eliminarEstudianteBtn;
    [SerializeField] Button guardarJsonBtn;
    [SerializeField] Button agregarPuntoBtn;
    [SerializeField] Button eliminarPuntoBtn;

    // ====== Datos en memoria ======
    public List<Estudiante> listaE = new();
    public List<Punto2D> listaP = new();

    void Awake()
    {
        // Conectar botones por código (sin OnClick del Inspector)
        if (agregarEstudianteBtn) agregarEstudianteBtn.onClick.AddListener(AgregarEstudiante);
        if (eliminarEstudianteBtn) eliminarEstudianteBtn.onClick.AddListener(EliminarUltimoEstudiante);
        if (guardarJsonBtn) guardarJsonBtn.onClick.AddListener(GuardarJSON);
        if (agregarPuntoBtn) agregarPuntoBtn.onClick.AddListener(AgregarPunto);
        if (eliminarPuntoBtn) eliminarPuntoBtn.onClick.AddListener(EliminarUltimoPunto);

        // Atajos con ENTER (TMP)
        if (carreraIF) carreraIF.onSubmit.AddListener(_ => AgregarEstudiante());
        if (puntoYIF) puntoYIF.onSubmit.AddListener(_ => AgregarPunto());
    }

    void Start()
    {
        // Cargar desde JSON si existe
        var e = Utilidades.LeerEstudiantesJSON();
        if (e != null && e.Count > 0) listaE = e;

        var p = Utilidades.LeerPuntosJSON();
        if (p != null && p.Count > 0) listaP = p;

        RefrescarUI();
    }

    void OnDestroy()
    {
        // Limpieza (buena práctica)
        if (agregarEstudianteBtn) agregarEstudianteBtn.onClick.RemoveListener(AgregarEstudiante);
        if (eliminarEstudianteBtn) eliminarEstudianteBtn.onClick.AddListener(EliminarUltimoEstudiante);
        if (eliminarEstudianteBtn) eliminarEstudianteBtn.onClick.RemoveListener(EliminarUltimoEstudiante);
        if (guardarJsonBtn) guardarJsonBtn.onClick.RemoveListener(GuardarJSON);
        if (agregarPuntoBtn) agregarPuntoBtn.onClick.RemoveListener(AgregarPunto);
        if (eliminarPuntoBtn) eliminarPuntoBtn.onClick.RemoveListener(EliminarUltimoPunto);

        if (carreraIF) carreraIF.onSubmit.RemoveAllListeners();
        if (puntoYIF) puntoYIF.onSubmit.RemoveAllListeners();
    }

    // ====== Lógica: Estudiantes ======
    void AgregarEstudiante()
    {
        if (string.IsNullOrWhiteSpace(nombreIF?.text) || string.IsNullOrWhiteSpace(codigoIF?.text))
            return;

        var est = new Estudiante(
            codigoIF.text.Trim(),
            carreraIF ? carreraIF.text.Trim() : "",
            nombreIF.text.Trim(),
            emailIF ? emailIF.text.Trim() : "",
            direccionIF ? direccionIF.text.Trim() : ""
        );

        listaE.Add(est);
        LimpiarCamposEstudiante();
        RefrescarUI();
    }

    void EliminarUltimoEstudiante()
    {
        if (listaE.Count > 0)
        {
            listaE.RemoveAt(listaE.Count - 1);
            RefrescarUI();
        }
    }


    // ====== Lógica: Puntos ======
    void AgregarPunto()
    {
        if (TryGetDouble(puntoXIF?.text, out var x) && TryGetDouble(puntoYIF?.text, out var y))
        {
            listaP.Add(new Punto2D(x, y));
            if (puntoXIF) puntoXIF.text = "";
            if (puntoYIF) puntoYIF.text = "";
            RefrescarUI();
        }
    }

    void EliminarUltimoPunto()
    {
        if (listaP.Count > 0)
        {
            listaP.RemoveAt(listaP.Count - 1);
            RefrescarUI();
        }
    }

    // ====== Guardar ======
    void GuardarJSON()
    {
        Utilidades.GuardarEstudiantesJSON(listaE);
        Utilidades.GuardarPuntosJSON(listaP);
        Debug.Log("Guardado OK (Estudiantes y Puntos).");
    }

    // ====== Helpers ======
    void RefrescarUI()
    {
        if (listaEstudiantesTXT)
        {
            listaEstudiantesTXT.text = "";
            for (int i = 0; i < listaE.Count; i++)
            {
                var e = listaE[i];
                listaEstudiantesTXT.text += $"{i + 1}. {e.CodigoE} - {e.Nombre} - {e.NameCarreraE}\n";
            }
        }

        if (listaPuntosTXT)
        {
            listaPuntosTXT.text = "";
            for (int i = 0; i < listaP.Count; i++)
            {
                var p = listaP[i];
                listaPuntosTXT.text += $"{i + 1}. ({p.X}, {p.Y})\n";
            }
        }
    }
 
    void LimpiarCamposEstudiante()
    {
        if (nombreIF) nombreIF.text = "";
        if (emailIF) emailIF.text = "";
        if (direccionIF) direccionIF.text = "";
        if (codigoIF) codigoIF.text = "";
        if (carreraIF) carreraIF.text = "";
    }

    bool TryGetDouble(string s, out double value)
    {
        s = (s ?? "").Replace(",", "."); // acepta coma o punto
        return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
    }
}


