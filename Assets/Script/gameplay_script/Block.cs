using UnityEngine;
using UnityEngine.Rendering;

public class Block : MonoBehaviour
{
    public const int Size= 5;
    public static bool GameOver=false;
    private readonly Vector3 inputoffset = new Vector3(0.0f,2.0f,0.0f); // khoang cach giua chuot va block
    [SerializeField] private Cell cellPrefab;
    private SortingGroup sortingGroup;
private Vector3 initialPosition;
    private readonly Cell [,] cells=new Cell[Size,Size]; 
    private Vector3 position;
    private Vector3 scale;
    private Vector2 inputPoint;
    private Camera mainCamera;
    private Vector2 center;
    private Vector2Int currentDragPoint;
    [SerializeField]  Board board;
    [SerializeField] private Blocks blocks;
    private int polyominoIndex;
    private void Awake(){
        mainCamera=Camera.main;
        sortingGroup=gameObject.GetComponent<SortingGroup>();
    }
    public void Initializes()
    {
        for (var r =0; r< Size;r++)
        {
             for (var c =0;c<Size;c++)
            {
                cells[r,c]=Instantiate(cellPrefab,transform);
            }
        }
        position= transform.position;
        scale=transform.localScale;
    }
    public void Show (int polyominoIndex)
    {
        this.polyominoIndex=polyominoIndex;
        Hide();
        var polyomino= Polyominos.Get(polyominoIndex);
        var polyominoRow=polyomino.GetLength(0); // dem so cum ngoac nhon
        var polyominoColumn= polyomino.GetLength(1);// dem so phan tu trong cum
        center= new Vector2 (polyominoColumn*0.5f,polyominoRow*0.5f);// lay trung diem
        for ( var r=0;r<polyominoRow;r++)
        {
            for(var c=0;c < polyominoColumn;c++)
            {
                if(polyomino[r,c]>0)
                {
                    cells[r,c].transform.localPosition=new(c-center.x+0.5f,r-center.y+0.5f,0.0f);
                    cells[r,c].Normal();
                }
            }
        }
        
    }
    public void Hide(){
        for (var r =0; r< Size;r++)
            {
                for (var c =0;c<Size;c++)
                    {
                        if (cells[r,c]!=null)
                            cells[r,c].Hide();
                    }
            }

    }


    private void OnMouseDown()
    {
        
        if (GameOver == true) return;
        inputPoint=mainCamera.ScreenToWorldPoint(Input.mousePosition);// vi tri nhan chuot
        blocks.Restsorting();
        Setsortingoder(1);
        Debug.Log("OnMouseDown");
        board.Hover(currentDragPoint,polyominoIndex);
        transform.position=position+new Vector3(0.0f,2.0f,0.0f);        
        transform.localScale=Vector3.one;
    }

    private void OnMouseDrag()
    {
        if (GameOver == true) return;
        Debug.Log("OnMouseDrag");
        var inputDelta= (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition)-inputPoint;// khoan cach keo chuot va vi tri nhan ban dau
        transform.position=position+inputoffset+(Vector3)inputDelta;
        currentDragPoint=Vector2Int.RoundToInt((Vector2)transform.position-center);
        board.Hover(currentDragPoint,polyominoIndex);
        Debug.Log($"vi tri drag{currentDragPoint}");

    }
    private void OnMouseUp()
    {
        if (GameOver == true) return;
        currentDragPoint=Vector2Int.RoundToInt((Vector2)transform.position-center);
        if(board.Place(currentDragPoint,polyominoIndex)==true)
        {
            gameObject.SetActive(false);
            blocks.Remove();
        }
        transform.position=position;      
        transform.localScale=scale;

        Debug.Log("OnMouseUp");
    }
    public void Setsortingoder(int sorting_oder)
    {
        sortingGroup.sortingOrder=sorting_oder;
    }

}

