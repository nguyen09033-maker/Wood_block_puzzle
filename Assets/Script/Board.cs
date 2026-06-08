using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int Size = 8;
    [SerializeField] private Transform cellTransform;
    [SerializeField] private Cell cellPrefab;
    private score Score;
    private readonly Cell[,] cells = new Cell[Size,Size];
    private readonly int [,] data= new int [Size,Size]; // 0 = hide, 1= hover,2=normal
    private readonly List<Vector2Int> hoverPoints =new();
    private readonly List<int> fullLineColum=new();
    private readonly List<int> fullLineRow=new();
    private Sound soundSFX;
    void Awake()
    {
        soundSFX= GameObject.FindGameObjectWithTag("Audio").GetComponent<Sound>();
    }
    void Start()
    {
        for ( var  r =0;r<Size;r++ )
        {
            for ( var c=0;c<Size ; c++)
            {
                 cells[r,c]= Instantiate(cellPrefab,cellTransform);
                 cells[r,c].transform.position= new (c,r,0.0f);
                 cells[r,c].Hide();
            }
        }
    }
    public void Hover(Vector2Int currentDragPoint, int polyominoIndex)
    {
        var polyomino=Polyominos.Get(polyominoIndex);
        var polyominoRows=polyomino.GetLength(0);
        var polyominoColumns=polyomino.GetLength(1);
        Unhighlight();
        Unhover();
        HoverPoints(currentDragPoint,polyominoColumns,polyominoRows,polyomino);
        if(hoverPoints.Count>0)       
        {
            Hover();
            hightlight(currentDragPoint,polyominoColumns,polyominoRows);
        }
    }
    private bool IsValidcurrentDragPoint(Vector2Int currentDragPoint)
    {
        if(currentDragPoint.x<0||Size<=currentDragPoint.x) return false;
        if(currentDragPoint.y<0|| Size<=currentDragPoint.y)return false;
        if(data[currentDragPoint.y,currentDragPoint.x]>0)return false;
        return true;
    }
  // lưu vị trí ô mà block có thể đặt được  
    private void HoverPoints(Vector2Int currentDragPoint , int polyominoColumn,int polyominoRow,int[,] polyomino)
    {
        for (var r =0;r<polyominoRow;r++)
        {
            for (var c=0;c<polyominoColumn;c++)
            {
                if (polyomino[r,c]>0)
                {
                    var hovercurrentDragPoint= currentDragPoint +new Vector2Int(c,r);
                    if(IsValidcurrentDragPoint(hovercurrentDragPoint)== false)
                    {
                        hoverPoints.Clear();
                        return;
                    }
                    hoverPoints.Add(hovercurrentDragPoint);
                }
            }
        }
    }
    private void Hover()
    {
         foreach (var hoverPoints in hoverPoints)
        {
            data[hoverPoints.y,hoverPoints.x]=1;
            cells[hoverPoints.y,hoverPoints.x].Hover();
        }
    }
    private void Unhover()
    {
        foreach (var hoverPoints in hoverPoints)
        {
            data[hoverPoints.y,hoverPoints.x]=0;
            cells[hoverPoints.y,hoverPoints.x].Hide();
        }
        hoverPoints.Clear();
    }
        public bool Place(Vector2Int currentDragPoint, int polyominoIndex)
    {
        var polyomino=Polyominos.Get(polyominoIndex);
        var polyominoRows=polyomino.GetLength(0);
        var polyominoColumns=polyomino.GetLength(1);
        Unhover();
        HoverPoints(currentDragPoint,polyominoColumns,polyominoRows,polyomino);
        if(hoverPoints.Count>0)
        {
            Place(currentDragPoint,polyominoColumns,polyominoRows);
            soundSFX.play_sfx(soundSFX.placeBlock);
            return true;
        }
        return false;
    }
        private void Place(Vector2Int point ,int polyominoColums,int polyominoRows)
    {
         foreach (var hoverPoint in hoverPoints)
        {
            data[hoverPoint.y,hoverPoint.x]=2;
            cells[hoverPoint.y,hoverPoint.x].Normal();
        }
        ClearLine(point,polyominoColums,polyominoRows);
        hoverPoints.Clear();
    }

    private void  ClearLine( Vector2Int point,int polyominoColumns,int polyominoRows )
    {
        checkLineRow(point.y, point.y + polyominoRows);
        checkLineColum(point.x, point.x + polyominoColumns);
        if (fullLineColum.Count > 0 || fullLineRow.Count > 0)
        {
            soundSFX.play_sfx(soundSFX.lineclear);
        }
        ClearfullLine();
        
    }
    private void ClearfullLine()
    {
        foreach(var r in fullLineRow)
        {
            for(var c =0;c<Size;c++)
            {
                data[r,c]=0;
                cells[r,c].Hide();
            }
            if (score.instance != null) score.instance.UpdateScore();
        }
                foreach(var c in fullLineColum)
        {
            for(var r =0;r<Size;r++)
            {
                data[r,c]=0;
                cells[r,c].Hide();
            }
            if (score.instance != null) score.instance.UpdateScore();
        }
    } 

    private void checkLineRow(int fromRow,int toRow)
    {
        fullLineRow.Clear();
        var startRow=Math.Max(0,fromRow);
        var endRow=Math.Min(Size,toRow);
        for (var r=startRow;r<endRow ;r++)
        {
            var isFullLine = true;
            for (var c=0;c<Size;c++)
            {
                if(data[r,c]!=2)
                {
                    isFullLine=false;
                }
            }
            if(isFullLine== true)
            {
                fullLineRow.Add(r);
            }

        }

    }
        private void checkLineColum(int fromcolum,int tocolum)
    {
        fullLineColum.Clear();
        var startColum=Math.Max(0,fromcolum);
        var endColum=Math.Min(Size,tocolum);
        for (var c=startColum;c<endColum;c++)
        {
            var isFullLine = true;
            for (var r=0;r<Size;r++)
            {
                if(data[r,c]!=2)
                {
                    isFullLine=false;
                }
            }
            if(isFullLine== true)
            {
                fullLineColum.Add(c);
            }

        }
// Higlight
    }
        private void UnhighlightRows()
    {
        foreach(var r in fullLineRow)
        {
            for(var c =0;c<Size;c++)
            {
                if(data[r,c]==2)
                {
                    cells[r,c].Normal();
                }
            }
        }
    }
            private void UnhighlightColums()
    {
        foreach(var c in fullLineColum)
        {
            for(var r =0;r<Size;r++)
            {
                if(data[r,c]==2)
                {
                    cells[r,c].Normal();
                }
            }
        }
    }
    private void Unhighlight()
    {
        UnhighlightColums();
        UnhighlightRows();
    }
        private void predictLineRow(int fromRow,int toRow)
    {
        fullLineRow.Clear();
        var startRow=Math.Max(0,fromRow);
        var endRow=Math.Min(Size,toRow);
        for (var r=startRow;r<endRow ;r++)
        {
            var isFullLine = true;
            for (var c=0;c<Size;c++)
            {
                if(data[r,c]!=2&&data[r,c]!=1)
                {
                    isFullLine=false;
                }
            }
            if(isFullLine== true)
            {
                fullLineRow.Add(r);
            }

        }

    }
        private void predictLineColum(int fromcolum,int tocolum)
    {
        fullLineColum.Clear();
        var startColum=Math.Max(0,fromcolum);
        var endColum=Math.Min(Size,tocolum);
        for (var c=startColum;c<endColum ;c++)
        {
            var isFullLine = true;
            for (var r=0;r<Size;r++)
            {
                if(data[r,c]!=2&&data[r,c]!=1)
                {
                    isFullLine=false;
                }
            }
            if(isFullLine== true)
            {
                fullLineColum.Add(c);
            }

        }

    }
            private void highlightRows()
    {
        foreach(var r in fullLineRow)
        {
            for(var c =0;c<Size;c++)
            {
                if(data[r,c]==2)
                {
                    cells[r,c].Highlight();
                }
            }
        }
    }
            private void highlightColums()
    {
        foreach(var c in fullLineColum)
        {
            for(var r =0;r<Size;r++)
            {
                if(data[r,c]==2)
                {
                    cells[r,c].Highlight();
                }
            }
        }
    }
        private void  hightlight( Vector2Int point,int polyominoColumns,int polyominoRows )
    {
        predictLineRow(point.y,point.y+polyominoRows);
        predictLineColum(point.x,polyominoColumns+point.x);
        highlightColums();
        highlightRows();

    }
//tinh nang kiem tra thua
    public bool checkPlace(int polyominoIndex)
    {
        var polyomino=Polyominos.Get(polyominoIndex);
        var polyominoRows=polyomino.GetLength(0);
        var polyominoColumns=polyomino.GetLength(1);
        for (var r =0;r<=Size-polyominoRows;r++)
        {
            for (var c =0;c<=Size - polyominoColumns;c++)
            {
                if(Can_Place(r,c,polyominoColumns,polyominoRows,polyomino)==true)
                {
                    return true ;
                }
            }
        }
        return false;
    }
    private bool Can_Place(int row,int colum,int polyominoColumns,int polyominoRows, int[,]polyomino)
    {
        for (var r=0;r<polyominoRows;r++)
        {
            for (var c=0;c<polyominoColumns;c++)
            {
                if(polyomino[r,c]>0&& data[row+r,c+colum]==2)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
