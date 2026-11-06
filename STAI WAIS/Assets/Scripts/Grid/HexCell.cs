using UnityEngine;

public class HexCell : MonoBehaviour //Representa cada hex�gono inividual que aparece en el tablero
{
    public HexCoordinates Coordinates { get; private set; } //Guarda las coordenadas de la casilla

    private HexGridManager grid;//Administrador del tablero
    private SpriteRenderer spriteRenderer;

    [Header("Colores de la casilla")]//el ultimo digito es para la componente opacidad
    public Color normalColor = new Color(1f, 1f, 1f, 0.15f);
    public Color hoverColor = new Color(0f, 0f, 0f, 0.25f);
    public Color selectedColor = new Color(0f, 0f, 0f, 0.40f);
    private bool isSelected = false;
    public void Initialize(HexGridManager gridManager, HexCoordinates coordinates)
    {
        grid = gridManager;
        Coordinates = coordinates;
        name = $"Hex_{coordinates.Q}_{coordinates.R}"; //Nombre del objeto de Unity

        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateVisual();
    }

    private void OnMouseEnter()//cuadno el raton entra en la casilla
    {
        if (TurnsManager.EstaEnTransicion) return;
        if (!isSelected) 
        {
            spriteRenderer.color = hoverColor;
        }
    }
    private void OnMouseExit()//cuando el at�n sale de la casilla
    {
        if (TurnsManager.EstaEnTransicion) return;
        UpdateVisual();
    }
    private void OnMouseDown()//Detectar los clics del rat�n sobre las celdas
    {
        if (TurnsManager.EstaEnTransicion) return;
        if (grid != null)
        {
            grid.SelectCell(this);
        }
    }
    public void SetSelected(bool selected) 
    {
        isSelected = selected;
        UpdateVisual();
    }
    private void UpdateVisual() 
    {
        if (isSelected) { spriteRenderer.color = selectedColor; }
        else { spriteRenderer.color = normalColor; }
    }
}
