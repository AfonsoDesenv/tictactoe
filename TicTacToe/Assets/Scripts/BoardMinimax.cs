    using UnityEngine;
using System.Collections;
using System;

public class BoardMinimax : MonoBehaviour {

    private GameObject Enemy_Piece;
    [SerializeField] private int [] board_map = new int [9]; // 0 = empty, 1 = player, 2 = enemy
    [SerializeField] public float tree_deph = 1f;
    [SerializeField] public string player_choice = "X";
    public bool player_moved = false;
    public bool player_wins = false;
    public bool enemy_wins = false;

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
    void Awake()
    {
        LoadSettings();
    }
    
    protected virtual void Start()
    {
        Enemy_Piece = (GameObject)Resources.Load("Prefabs/Prefab_" + (player_choice == "X" ? "O" : "X") , typeof(GameObject));
    }
    protected virtual void FixedUpdate()
    {
        if(!Is_GameOver(board_map))
        {
            if(player_moved)
            {
                player_moved = false;
                Execute_Minimax();
            }
        } else {
            UI.Is_GameOver = true;
        }
    }

    public void Change_TreeLevel (float newLevel) 
    {
        tree_deph = newLevel;
    }

    public void ChoosePiece (string piece){
        this.player_choice = piece;
    }

    public void Execute_Minimax()
    {

        int position = Minimax(Clone(this.board_map));
        Move(position);
        
        if (Make_Sequence(this.board_map, false))
            enemy_wins = true;

    }

    private int Minimax(int[] board_map)
    {
        int position = -1;
        int value_returned;
        int maxValue = -1000;
        int alpha = -1000;
        int beta = 1000;

        for (int i = 0; i < 9; i++)
        {
            value_returned = Play_Min(i, Clone(board_map), 1, alpha, beta);
            
            if (value_returned >= alpha)
                alpha = value_returned;

            if (value_returned > maxValue)
            {
                maxValue = value_returned;
                position = i;
            }
        }
        
        return position;
    }

    private int Play_Min(int position, int[] board_map, int tree_deph, int alpha, int beta)
    {
        if (Can_Put_Here(position, board_map))
        {
            board_map[position] = 2;

            if (Is_Leaf(tree_deph) || Is_GameOver(board_map))
            {
                return Value_The_Move(board_map);
            }
            else
            {
                int value_returned;
                int minValue = 1000;
                for (int i = 0; i < 9; i++)
                {
                    value_returned = Play_Max(i, Clone(board_map), tree_deph + 1, alpha, beta);

                    if (value_returned < beta)
                        beta = value_returned;

                    if (value_returned < minValue)
                        minValue = value_returned;

                    if (alpha >= beta)
                        return minValue;

                }
                return minValue;
            }
        }
        return -1000;
    }

    private int Play_Max(int position, int[] board_map, int tree_deph, int alpha, int beta)
    {
        if (Can_Put_Here(position, board_map))
        {
            board_map[position] = 1;

            if (Is_Leaf(tree_deph) || Is_GameOver(board_map))
            {
                return Value_The_Move(board_map);
            }
            else
            {
                int value_returned;
                int maxValue = -1000;
                for (int i = 0; i < 9; i++)
                {

                    value_returned = Play_Min(i, Clone(board_map), tree_deph + 1, alpha, beta);

                    if (value_returned > alpha)
                        alpha = value_returned;

                    if (value_returned > maxValue)
                        maxValue = value_returned;

                    if (alpha >= beta)
                        return maxValue;

                }
                return maxValue;
            }
        }
        return 1000;
    }

    protected int[] Clone(int [] board_map)
    {
        int[] new_board_map = new int [9];
        for(int i = 0; i < new_board_map.GetLength(0); i++)
        {
            new_board_map[i] = board_map[i];
        }
        return new_board_map;
    }

    private int Value_The_Move(int[] board_map)
    {

        int value = 0;
        value = Make_Sequence(board_map) ? 10 : 0;
        value += Number_Of_Sequences_Stopped(board_map) * 2;
        value += board_map[04] == 2 ? 2 : 0;

        return value;

    }


    public bool Make_Sequence()
    {
        return Make_Sequence(this.board_map, true);
    }

    protected bool Make_Sequence(int[] board_map, bool is_player = false)
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

    protected int Number_Of_Sequences_Stopped(int[] board_map, bool is_player = false)
    {
        int number_of_sequences_stopped = 0;
        for (int i = 0; i < this.possibleSequences.GetLength(0); i++)
        {
            if ((board_map[this.possibleSequences[i, 0]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 1]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 2]] == (is_player ? 1 : 2)) ||

                (board_map[this.possibleSequences[i, 0]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 1]] == (is_player ? 1 : 2) &&
                 board_map[this.possibleSequences[i, 2]] == (is_player ? 2 : 1)) ||

                (board_map[this.possibleSequences[i, 0]] == (is_player ? 1 : 2) &&
                 board_map[this.possibleSequences[i, 1]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 2]] == (is_player ? 2 : 1)))
            {
                number_of_sequences_stopped += 1;
            }
        }
        return number_of_sequences_stopped;
    }

    protected bool Is_Almost_Mills(int position)
    {
        for (int i = 0; i < this.possibleSequences.GetLength(0); i++)
        {
            if ((possibleSequences[i, 0] == position ||
                 possibleSequences[i, 1] == position ||
                 possibleSequences[i, 2] == position) &&

               ((board_map[this.possibleSequences[i, 0]] == 1 &&
                 board_map[this.possibleSequences[i, 1]] == 1 &&
                 board_map[this.possibleSequences[i, 2]] == 0) ||

                (board_map[this.possibleSequences[i, 0]] == 1 &&
                 board_map[this.possibleSequences[i, 1]] == 0 &&
                 board_map[this.possibleSequences[i, 2]] == 1) ||

                (board_map[this.possibleSequences[i, 0]] == 0 &&
                 board_map[this.possibleSequences[i, 1]] == 1 &&
                 board_map[this.possibleSequences[i, 2]] == 1)))
            {
                return true;
            }
        }
        return false;
    }

    public bool Can_Put_Here(int position, int[] board_map)
    {   
        return (board_map[position] == 0);
    }

    protected bool Is_Leaf(int tree_deph)
    {
        return tree_deph == this.tree_deph;
    }

    protected void Move(int position)
    {
        GameObject position_01 = GameObject.Find("position_" + position.ToString("00"));
        GameObject new_enemy_piece = Instantiate(Enemy_Piece, position_01.transform.position, Quaternion.identity) as GameObject;

        new_enemy_piece.transform.parent = position_01.transform;
        Put_Into_Board(position, false);
    }

    public void Put_Into_Board(int position, bool is_player)
    {
        this.board_map[position] = is_player ? 1 : 2;
    }
    
    public void Clean_The_Board(int position) {
        board_map[position] = 0;
    }
    
    public void Put_Player_Piece_Into_The_Board(int position) {
        board_map[position] = 1;
    }

    public void Put_Machine_Piece_Into_The_Board(int position) {
        board_map[position] = 2;
    }
    
    protected bool Part_Of_A_Mill(int position, int[] board_map, bool is_player = false)
    {
        for (int i = 0; i < this.possibleSequences.GetLength(0); i++)
        {
            if ((this.possibleSequences[i, 0] == position ||
                 this.possibleSequences[i, 1] == position ||
                 this.possibleSequences[i, 2] == position) &&

                (board_map[this.possibleSequences[i, 0]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 1]] == (is_player ? 2 : 1) &&
                 board_map[this.possibleSequences[i, 2]] == (is_player ? 2 : 1)))
                   return true;
        }
        return false;
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
    
	public void LoadSettings() 
	{
		Settings settings =	SaveSystem.LoadSettings();
		if (settings != null)
		{
			this.player_choice = settings.player_choice;
			this.tree_deph = settings.tree_deph;
		}
	}
}
