using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Connect4_Application_with_AI
{
    public partial class Form1 : Form
    {
        PictureBox[,] map = new PictureBox[6, 7];
        Image player = global::Connect4_Application_with_AI.Properties.Resources.yellow;
        Image computer = global::Connect4_Application_with_AI.Properties.Resources.Red;
        Image _empty = global::Connect4_Application_with_AI.Properties.Resources.board;
        const int row = 6;
        const int col = 7;
        int empty = 0;
        int[,] Board = new int[row, col];
        int currentTurn = 1;
        const int playerTurn = 1;
        const int computerTurn = 2;
        const int none = -1;
        const int computerValue = -2;
        const int playerValue = -3;
        int depth = 6;

        public Form1()
        {
            InitializeComponent();

            wireMap();
            initializeBoard();
        }

        public void wireMap()
        {
            map[0, 0] = emptyBox0;
            map[0, 1] = emptyBox1;
            map[0, 2] = emptyBox2;
            map[0, 3] = emptyBox3;
            map[0, 4] = emptyBox4;
            map[0, 5] = emptyBox5;
            map[0, 6] = emptyBox6;
            map[1, 0] = emptyBox7;
            map[1, 1] = emptyBox8;
            map[1, 2] = emptyBox9;
            map[1, 3] = emptyBox10;
            map[1, 4] = emptyBox11;
            map[1, 5] = emptyBox12;
            map[1, 6] = emptyBox13;
            map[2, 0] = emptyBox14;
            map[2, 1] = emptyBox15;
            map[2, 2] = emptyBox16;
            map[2, 3] = emptyBox17;
            map[2, 4] = emptyBox18;
            map[2, 5] = emptyBox19;
            map[2, 6] = emptyBox20;
            map[3, 0] = emptyBox21;
            map[3, 1] = emptyBox22;
            map[3, 2] = emptyBox23;
            map[3, 3] = emptyBox24;
            map[3, 4] = emptyBox25;
            map[3, 5] = emptyBox26;
            map[3, 6] = emptyBox27;
            map[4, 0] = emptyBox28;
            map[4, 1] = emptyBox29;
            map[4, 2] = emptyBox30;
            map[4, 3] = emptyBox31;
            map[4, 4] = emptyBox32;
            map[4, 5] = emptyBox33;
            map[4, 6] = emptyBox34;
            map[5, 0] = emptyBox35;
            map[5, 1] = emptyBox36;
            map[5, 2] = emptyBox37;
            map[5, 3] = emptyBox38;
            map[5, 4] = emptyBox39;
            map[5, 5] = emptyBox40;
            map[5, 6] = emptyBox41;
        }


        private void column1_Click(object sender, EventArgs e)
        {
            setColor(0);
        }

        private void column2_Click(object sender, EventArgs e)
        {
            setColor(1);
        }

        private void column3_Click(object sender, EventArgs e)
        {
            setColor(2);
        }

        private void column4_Click(object sender, EventArgs e)
        {
            setColor(3);
        }

        private void column5_Click(object sender, EventArgs e)
        {
            setColor(4);
        }

        private void column6_Click(object sender, EventArgs e)
        {
            setColor(5);
        }

        private void column7_Click(object sender, EventArgs e)
        {
            setColor(6);
        }

        public void initializeBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Board[i, j] = empty;
                }
            }
        }

        public void setColor(int column)
        {
            int r = freeRow(Board, column);

            if (r >= 0)
            {
                if (currentTurn == 1)
                {

                    map[r, column].Image = player;
                    addCircle(Board, r, column, playerTurn);
                    win(playerTurn);
                }
                else
                {
                    map[r, column].Image = computer;
                    addCircle(Board, r, column, computerTurn);
                    win(computerTurn);
                }
            }
        }

        private void comp(int[,] board)
        {

            if (currentTurn == 2)
            {
                int colIndex = miniMax(board, this.depth, int.MinValue, int.MaxValue, true)[0];
                setColor(colIndex);
            }

        }

        public void NextTurn()
        {
            if (currentTurn == 1)
            {
                currentTurn = 2;
                comp(Board);
            }
            else
            {
                currentTurn = 1;
            }
        }

        private bool validLocation(int[,] board, int column)
        {
            return board[0, column] == 0;
        }

        private int[] getValidLocations(int[,] board)
        {
            List<int> validLocations = new List<int>();
            for (int i = 0; i < col; i++)
            {
                if (validLocation(board, i))
                {
                    validLocations.Add(i);
                }
            }
            return validLocations.ToArray();
        }

        private int freeRow(int[,] board, int column)
        {
            for (int r = row - 1; r >= 0; r--)
            {

                if (board[r, column] == 0)
                {
                    return r;
                }

            }
            return -1;
        }

        private int count(int[] array, int p)
        {
            int c = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == p)
                {
                    c++;
                }
            }
            return c;
        }

        private int evaluate(int[] array, int turn)
        {
            int score = 0;
            int oppTurn = playerTurn;
            if (turn == playerTurn)
                oppTurn = computerTurn;

            if (count(array, turn) == 4)
            {
                score += 100;
            }
            else
                if (count(array, turn) == 3 && count(array, empty) == 1)
            {
                score += 5;
            }
            else if (count(array, turn) == 2 && count(array, empty) == 2)
            {
                score += 2;
            }

            if (count(array, oppTurn) == 3 && count(array, empty) == 1)
            {
                score -= 4;
            }
            return score;
        }

        public int[] secondArray(int[] array, int start, int end)
        {
            int j = 0;
            int[] secondArray = new int[10];
            for (int i = start; i < end; i++)
            {
                secondArray[j] = array[i];
                j++;
            }
            return secondArray;
        }
        private int Score(int[,] board, int piece)
        {
            int score = 0;
            List<int> centerArr = new List<int>();
            List<int> rowArr = new List<int>();
            List<int> colArr = new List<int>();
            List<int> window = new List<int>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    centerArr.Add(board[i, j]);
                }
            }

            score += count(centerArr.ToArray(), piece) * 3;


            //Horizontal 
            for (int r = 0; r < row; r++)
            {

                for (int j = 0; j < col; j++)
                {

                    rowArr.Add(board[r, j]);

                }

                for (int c = 0; c < col - 3; c++)
                {


                    score += evaluate(secondArray(rowArr.ToArray(), c, c + 4), piece);

                }
            }

            //vertical
            for (int c = 0; c < col; c++)
            {

                for (int j = 0; j < row; j++)
                {

                    colArr.Add(board[j, c]);

                }

                for (int r = 0; r < row - 3; r++)
                {


                    score += evaluate(secondArray(colArr.ToArray(), c, c + 4), piece);

                }
            }

            //positive diag
            for (int r = 0; r < row - 3; r++)
            {

                for (int j = 0; j < col - 3; j++)
                {

                    for (int i = 0; i < 4; i++)

                        window.Add(board[r + i, j + i]);
                    score += evaluate(window.ToArray(), piece);

                }
            }

            //negative diag
            for (int r = 0; r < row - 3; r++)
            {

                for (int j = 0; j < col - 3; j++)
                {

                    for (int i = 0; i < 4; i++)

                        window.Add(board[r + 3 - i, j + i]);
                    score += evaluate(window.ToArray(), piece);

                }
            }

            return score;
        }

        private bool winningMove(int[,] board, int turn)
        {
            //horizontal
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (board[r, c] == turn && board[r, c + 1] == turn && board[r, c + 2] == turn && board[r, c + 3] == turn)
                    {
                        return true;
                    }
                }
            }

            //vertical
            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row - 3; r++)
                {
                    if (board[r, c] == turn && board[r + 1, c] == turn && board[r + 2, c] == turn && board[r + 3, c] == turn)
                    {
                        return true;
                    }
                }
            }


            //positive diagonals
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 0; r < row - 3; r++)
                {
                    if (board[r, c] == turn && board[r + 1, c + 1] == turn && board[r + 2, c + 2] == turn && board[r + 3, c + 3] == turn)
                    {
                        return true;
                    }
                }
            }

            //negative diagonals
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 3; r < row; r++)
                {
                    if (board[r, c] == turn && board[r - 1, c + 1] == turn && board[r - 2, c + 2] == turn && board[r - 3, c + 3] == turn)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        private bool terminalNode(int[,] board)
        {
            return winningMove(board, playerTurn) || winningMove(board, computerTurn) || getValidLocations(board).Length == 0;
        }

        private void addCircle(int[,] board, int row, int column, int turn)
        {

            board[row, column] = turn;

        }


        private int[,] copyBoard(int[,] board)
        {
            int[,] temp = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    temp[i, j] = board[i, j];
                }
            }
            return temp;
        }

        private int[] miniMax(int[,] board, int depth, int alpha, int beta, bool maximizingPlayer)
        {

            int[] validLocations = getValidLocations(board);
            bool terminal = terminalNode(board);
            List<int> values = new List<int>();

            if (depth == 0 || terminal)
            {
                if (terminal)
                {
                    if (winningMove(board, computerTurn))
                    {
                        values.Add(none);
                        values.Add(computerValue);
                        return values.ToArray();
                    }
                    else
                    {

                        if (winningMove(board, playerTurn))
                        {
                            values.Add(none);
                            values.Add(playerValue);
                            return values.ToArray();
                        }
                        else
                        {
                            values.Add(none);
                            values.Add(0);
                            return values.ToArray();
                        }
                    }

                }
                else
                {
                    values.Add(none);
                    values.Add(Score(board, computerTurn));
                    return values.ToArray();
                }

            }

            if (maximizingPlayer)
            {
                int value = int.MinValue;
                Random rand = new Random();
                int index = rand.Next(validLocations.Length);
                int column = validLocations[index];
                int[,] temp = new int[row, col];
                for (int c = 0; c < validLocations.Length; c++)
                {
                    int _row = freeRow(board, validLocations[c]);

                    temp = copyBoard(board);

                    addCircle(temp, _row, validLocations[c], computerTurn);
                    int new_score = miniMax(temp, depth - 1, alpha, beta, false)[1];
                    if (new_score > value)
                    {
                        value = new_score;
                        column = validLocations[c];
                    }
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }

                }
                values.Add(column);
                values.Add(value);
                return values.ToArray();
            }
            else
            {
                int value = int.MaxValue;
                Random rand = new Random();
                int index = rand.Next(validLocations.Length);
                int column = validLocations[index];
                int[,] temp = new int[row, col];
                for (int c = 0; c < validLocations.Length; c++)
                {
                    int _row = freeRow(board, validLocations[c]);

                    temp = copyBoard(board);
                    addCircle(temp, _row, validLocations[c], playerTurn);
                    int new_score = miniMax(temp, depth - 1, alpha, beta, true)[1];
                    if (new_score < value)
                    {
                        value = new_score;
                        column = validLocations[c];
                    }
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }

                }
                values.Add(column);
                values.Add(value);
                return values.ToArray();
            }


        }

        public void win(int piece)
        {


            if (winningMove(Board, piece))
            {
                if (currentTurn == 1)
                {
                    MessageBox.Show("You won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    MessageBox.Show("computer won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
            }
            else
                NextTurn();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
