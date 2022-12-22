using UnityEngine;

public class TextMeshMove : MonoBehaviour
{
    public TextMesh infoText;
    public Camera cam;
    Vector3 offsetHeight = new Vector3(0, 0, 5);

    void Update()
    {
        // Set the position of the text mesh to the position of the mouse
        transform.position = Input.mousePosition;
        
        // Create a ray from the main camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the raycast hits a collider
        if (Physics.Raycast(ray, out hit))
        {
            // If the raycast hits a cube, display its information

            // Get the TileInformation component of the hit object
            TileInformation info = hit.collider.gameObject.GetComponent<TileInformation>();
            if (info != null)
            {
                // Set the position of the info text to the position of the tile with a Height Offset
                infoText.transform.position = hit.collider.gameObject.transform.position + offsetHeight;

                // Set the text of the info text to the tile position
                infoText.text = "Tile Position: " + info.tilePosition;
            }
            else
            {
                // Clear the text of the info text
                infoText.text = "";
            }
        }
    }
}
