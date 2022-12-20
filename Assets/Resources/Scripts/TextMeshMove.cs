using UnityEngine;

public class TextMeshMove : MonoBehaviour
{
    // The text mesh that displays the tile information
    public TextMesh infoText;
    public Camera cam;

    void Update()
    {
        // Set the position of the text mesh to the position of the mouse
        transform.position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 Offset = new Vector3(0, 0, 5);

        if (Physics.Raycast(ray, out hit))
        {
            // If the raycast hits a cube, display its information
            TileInformation info = hit.collider.gameObject.GetComponent<TileInformation>();
            if (info != null)
            {
                // Set the position of the info text to the position of the tile
                infoText.transform.position = hit.collider.gameObject.transform.position+Offset;

                // Set the text of the info text to the tile position
                infoText.text = "Tile Position: " + info.tilePosition;
            }
            else
            {
                infoText.text = "";
            }
        }
    }
}
