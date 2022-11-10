using UnityEngine;
using System.Collections;

public class PositionPVP : MonoBehaviour {

    private GameObject Piece_X;
    private GameObject Piece_O;

    [SerializeField] private BoardPVP board;
    [SerializeField] private int this_position;

    protected virtual void Start()
    {
        Piece_X = (GameObject)Resources.Load("Prefabs/Prefab_X", typeof(GameObject));
        Piece_O = (GameObject)Resources.Load("Prefabs/Prefab_O", typeof(GameObject));
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
        GameObject new_player_piece = Instantiate((board.is_player_one ? Piece_X : Piece_O), this.transform.position, Quaternion.identity);
        new_player_piece.transform.parent = this.transform;
        board.Put_Into_Board(this_position, board.is_player_one);
        
        if (board.Make_Sequence(board.Get_Board_Map(), board.is_player_one))
            if (board.is_player_one)
                board.player_one_wins = true;
            else
                board.player_two_wins = true;

        board.is_player_one = !board.is_player_one;
    }
}
