using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class HexGridManager : MonoBehaviour //Administra todo el tablero
{
    [Header("Configuracion del tablero")]
    public GameObject hexPrefab; //prefab del hexagono
    public int mapRadius = 6; //Radio del tablero N = 1 + 3R(R+1) donde N = numero de casillas y R el radio
    public float hexSize = 1f; //tama�o del hexagono y separacion entre los centros
    
    private Dictionary<HexCoordinates, HexCell> cells = new Dictionary<HexCoordinates, HexCell>(); //almacena las casillas del tablero
    private HexCell selectedCell;

    private void Start()
    {
        GenerateGrid();
    }
    //Generar el tablero
    public void GenerateGrid() 
    {
        for (int q = -mapRadius; q <= mapRadius; q++) //recorrer coordenada Q. Reccore desde -mapRadius hasta +mapRadius -> Ej: -5,-4,-3...3,4,5
        {
            //Calcular los l�mites de R para crear tablero hexagonal en lugar de cuadrado
            int MinRadius = Mathf.Max(-mapRadius, -q - mapRadius);
            int MaxRadius = Mathf.Min(mapRadius, -q + mapRadius);

            for (int r = MinRadius; r <= MaxRadius; r++) 
            {
                HexCoordinates coordinates = new HexCoordinates(q, r);
                Vector3 position = AxialToWorld(coordinates); //convertir las coordenadas a posicion de Unity


                //Crear una copia del prefab
                GameObject hexObject = Instantiate(hexPrefab, position, Quaternion.identity, transform);

                //Busca el componente HexCell dentro del objeto que acabamos de crear.
                //As� podemos acceder a sus m�todos y variables.
                HexCell cell = hexObject.GetComponent<HexCell>();

                cell.Initialize(this, coordinates);

                cells.Add(coordinates, cell); //A�adimos al diccionario
            }
        }
    }
    public Vector3 AxialToWorld(HexCoordinates hex) //convierte coordenadas axiales a coordenadas de Unity
    {
        float x = hexSize * Mathf.Sqrt(3f) * (hex.Q + hex.R / 2f);
        float y = hexSize * 1.5f * hex.R;
        return transform.position + new Vector3(x, y, 0);
    }
    public HexCell GetCell(HexCoordinates coordinates) 
    {
        cells.TryGetValue(coordinates, out HexCell cell);
        return cell;
    }
    public void SelectCell(HexCell cell) 
    {
        //Quitar la seleccion anterior
        if (selectedCell != null) { selectedCell.SetSelected(false); }
        
        //Guardar la nueva seleccion
        selectedCell = cell;

        //activar visualmente la nueva seleccion 
        selectedCell.SetSelected(true);
        Debug.Log("Casilla seleccionada: " + cell.Coordinates);
    }
}
