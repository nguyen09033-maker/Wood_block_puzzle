using System.Runtime.CompilerServices;
using UnityEngine;
public class Blocks : MonoBehaviour
{    
     [SerializeField]private Board board;
     private int [] polyominoIndex;
     [SerializeField] private Block[] blocks;
     private int blockcount=0;
     public GameObject GameoverPanel;
    void Start()
    {
     var blockWidth= (float) Board.Size/blocks.Length;
     var cellSize= (float) Board.Size/(Block.Size*blocks.Length+ blocks.Length+1);
     for (var i =0 ;i<blocks.Length;i++)
        {
             blocks[i].transform.position=new(blockWidth * (i+0.5f),-0.25f-cellSize*4.0f,0.0f);
             blocks[i].transform.localScale=new ( cellSize, cellSize, cellSize);
             blocks[i].Initializes();
        }
        polyominoIndex=new int[blocks.Length];
          Generate();

    }
    private void Generate(){
     for (var i=0;i< blocks.Length;i++){
          polyominoIndex[i]=Random.Range(0,Polyominos.Length); 
          blocks[i].gameObject.SetActive(true);
          blocks[i].Show(polyominoIndex[i]);
          blockcount++;
     }
    }
    public void Remove()
     {
          blockcount--;
          if(blockcount<=0)
          {
               blockcount=0;
               Generate();
          }
          var lose =true;
          for ( var i=0;i<blocks.Length;i++)
          {
               if( blocks[i].gameObject.activeSelf==true&& board.checkPlace(polyominoIndex[i]) == true)
               {
                    lose=false;
                    break;
               }
               
          }
          if (lose==true)
          {
               Debug.Log("lose");  
               losegame();
               
          }
     }
         public void Restsorting()
    {
        for (var i=0;i<blocks.Length;i++)
        {
            blocks[i].Setsortingoder(0);
        }
    }
     public void losegame()
     {
          GameoverPanel.SetActive(true);
          Block.GameOver=true;
     }
}
 