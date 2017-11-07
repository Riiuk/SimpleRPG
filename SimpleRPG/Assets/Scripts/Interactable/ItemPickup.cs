using UnityEngine;

public class ItemPickup : Interactable
{
    [Tooltip("Variable publica tipo Item que contiene la información de cada objeto en el juego")]
    public Item item;

    /// <summary>
    /// Método público que sobre escribe la función Interact del objeto Interactivo
    /// </summary>
    public override void Interact()
    {
        base.Interact();
        // Llamamos al metodo que realiza la acción de recoger el objeto
        PickUp();
    }

    /// <summary>
    /// Método encargado de recoger el objeto
    /// </summary>
    void PickUp()
    {
        // Mostramos por consola un aviso de que estamos recogiendo un item
        Debug.Log("Cogiendo el item: " + item.name);
        // Si hemos conseguido llamar a la función de añadir al inventario,
        // destruimos el objeto
        if (Inventory.instance.Add(item))
            Destroy(gameObject);
    }
        
        
}
