
using UnityEngine;
using UnityEngine.InputSystem;
public class inventario : MonoBehaviour
{
    public Item[] itens;
    public GameObject mouseItem;
    public void DragItem(GameObject button)
    {
       mouseItem = button;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - mouseItem.transform.position.z); // Distância da câmera ao objeto
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = mouseItem.transform.position.z; // Mantém o Z original do botão
        mouseItem.transform.position = worldPos;
    }
    public void DropItem(GameObject button)
    {
        if (mouseItem != null)
        {
            // Salva os pais
        Transform auxParent = mouseItem.transform.parent;
        // Salva as posições
        Vector3 auxPos = mouseItem.transform.position;

        // Troca os pais
        mouseItem.transform.SetParent(button.transform.parent);
        button.transform.SetParent(auxParent);

        // Troca as posições
        Vector3 buttonPos = button.transform.position;
        mouseItem.transform.position = buttonPos;
        button.transform.position = auxPos;
        }
        
    }
}
