using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject
{
    new public string name = "Nuevo Objeto";
    public Sprite icon = null;
    public bool isDefaultItem = false;
}
