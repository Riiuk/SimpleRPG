using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("Variable pública que almacena el espacio máximo de almacenaje")]
    public int space = 20;
    [Tooltip("Lista pública que almacena los items que porta el jugador")]
    public List<Item> items = new List<Item>();

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }
    #endregion

    public delegate void OnItemChanged();       // Magia negra
    public OnItemChanged onItemChangedCallback; // Magia negra

    /// <summary>
    /// Método público tipo bool que requiere de un objeto tipo Item al ser llamado
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Add(Item item)
    {
        // Si el objeto que se va a añadir no es el default, continuamos
        if (!item.isDefaultItem)
        {
            // Si el contador de items recogido es mayor o igual al espacio máximo
            if (items.Count >= space)
            {
                // Mostramos un aviso
                Debug.Log("No hay suficiente espacio - TODO: mostrar en pantalla");
                // Y obligamos a salir de la función devolviendo false
                return false;
            }
            // Si por el contrario tenemos espacio, añadimos el item a la lista de items
            items.Add(item);

            // Mágia negra
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        // Si hemos añadido bien el item al inventario y hemos llegado a este punto,
        // devolvemos como true la función
        return true;
    }

    /// <summary>
    /// Método que llamamos para quitar un item del inventario,
    /// asignando el item que qureremos quitar desde donde lo llamamos
    /// </summary>
    /// <param name="item"></param>
    public void Remove(Item item)
    {
        // Quitamos el item de la lista de inventario
        items.Remove(item);

        // Mágia negra
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }


}
