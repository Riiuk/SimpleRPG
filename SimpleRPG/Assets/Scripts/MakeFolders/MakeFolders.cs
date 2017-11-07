using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;


/// Colocar este script en el directorio raiz /Assets, arrastrandolo al inspector de Unity.
/// Crea un directorio nuevo con el nombre establecido abajo.
/// Modificar como se necesite.
/// Las carpetas son creadas en el raiz de Assets
/// No comprobado en Mac.


public class MakeFolders : ScriptableObject {

    //[MenuItem("Assets/Crear carpetas de proyecto")]
    [MenuItem("Edit/Project Settings/Crear carpetas de proyecto")]
    static void MenuMakeFolders() {
        CreateFolders();
    }

    static void CreateFolders() {
        string f = Application.dataPath + "/";

        Directory.CreateDirectory(f + "Models");
        Directory.CreateDirectory(f + "Fonts");
        Directory.CreateDirectory(f + "Plugins");
        Directory.CreateDirectory(f + "Textures");
        Directory.CreateDirectory(f + "Materials");
        Directory.CreateDirectory(f + "Resources");
        Directory.CreateDirectory(f + "Scenes");
        Directory.CreateDirectory(f + "Sounds");
        Directory.CreateDirectory(f + "Scripts");
        Directory.CreateDirectory(f + "Scripts/MakeFolders");
        Directory.CreateDirectory(f + "Shaders");
        Directory.CreateDirectory(f + "Sounds");
        Directory.CreateDirectory(f + "Prefabs");
        Directory.CreateDirectory(f + "Animations");

        Debug.Log("Directorios creados");
    }
}