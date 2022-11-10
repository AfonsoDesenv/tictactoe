    using UnityEngine;
using System.Collections;
using System;

public class BoardPVP : MonoBehaviour {

    private GameObject Enemy_Piece;
    [SerializeField] private int [] board_map = new int [9]; // 0 = empty, 1 = player, 2 = enemy
    [SerializeField] public float tree_deph = 1f;
    [SerializeField] public string player_choice = "X";    
    public bool is_player_one = true;
    public bool player_one_wins = false;
    public bool player_two_wins = false;
    private int [,] possibleSequences = { 
                                            {00, 01, 02},
                                            {03, 04, 05},
                                            {06, 07, 08},
                                            {00, 03, 06},
                                            {01, 04, 07},
                                            {02, 05, 08},
                                            {00, 04, 08},
                                            {02, 04, 06}
                                        };

    protected virtual void FixedUpdate()
    {
        UIPVP.Is_GameOver = Is_GameOver(board_map);
    }


    public bool Make_Sequence()
    {
        return Make_Sequence(this.board_map, true);
    }

    public bool Make_Sequence(int[] board_map, bool is_player = false)
    {
        for (int i = 0; i < this.possibleSequences.GetLength(0); i++)
        {
            if (board_map[this.possibleSequences[i, 0]] == (is_player ? 1 : 2) &&
                board_map[this.possibleSequences[i, 1]] == (is_player ? 1 : 2) &&
                board_map[this.possibleSequences[i, 2]] == (is_player ? 1 : 2))
                return true;
        }
        return false;
    }

    public bool Can_Put_Here(int position, int[] board_map)
    {   
        return (board_map[position] == 0);
    }

    public void Put_Into_Board(int position, bool is_player)
    {
        this.board_map[position] = is_player ? 1 : 2;
    }
    
    public int [] Get_Board_Map()
    {
        return this.board_map;
    }

    protected bool BoardIsFull (int[] board_map) {
        for (int i = 0; i < 9; i++)
            if (board_map[i] == 0)
                return false;
        return true;
    }

    public bool Is_GameOver (int[] board_map) {
        return Make_Sequence(board_map, true) ||
               Make_Sequence(board_map, false) ||
               BoardIsFull(board_map);
    }
}
