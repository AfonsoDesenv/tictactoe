using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

    private GameObject Player_Piece;
    
    [SerializeField] private BoardMinimax board;
    [SerializeField] private int this_position;

    protected virtual void Start()
    {
        Player_Piece = (GameObject)Resources.Load("Prefabs/Prefab_" + board.player_choice, typeof(GameObject));
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnMouseOver()
    {

    }

    protected virtual void OnMouseDown()
    {   
        if (!UI.GameIsPaused && !board.Is_GameOver(board.Get_Board_Map()))
        {
            if (board.Can_Put_Here(this_position, board.Get_Board_Map()))
            {
                Do_Move();
            }
        }
    }

    protected virtual void Do_Move()
    {
        GameObject new_player_piece = Instantiate(Player_Piece, this.transform.position, Quaternion.identity);
        new_player_piece.transform.parent = this.transform;
        board.Put_Into_Board(this_position, true);
        
        if (board.Make_Sequence())
            board.player_wins = true;

        board.player_moved = true;
    }
}
