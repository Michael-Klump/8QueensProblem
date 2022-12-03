using System;
using System.Collections;

namespace NQueensProblem
{

    public class NQueensProblem
    {

        private int _SizeOfField;
        private bool[,] _ChessField;
        private List<int> _ColumnsThatHaveQueensAlready;
        private List<bool[,]> _NQueenProblemSolutions;

        public int GetSizeOfField()
        {
            return _SizeOfField;
        }

        public void SetSizeOfField(int SizeOfField)
        {
            _SizeOfField = SizeOfField;
        }

        public bool[,] GetChessField()
        {
            return _ChessField;
        }

        public void SetChessField(bool[,] ChessField)
        {
            _ChessField = ChessField;
        }

        public List<int> GetColumnsThatHaveQueensAlready()
        {
            return _ColumnsThatHaveQueensAlready;
        }

        public void SetColumnsThatHaveQueensAlready(List<int> ColumnsThatHaveQueensAlready)
        {
            _ColumnsThatHaveQueensAlready = ColumnsThatHaveQueensAlready;
        }

        public List<bool[,]> GetNQueenProblemSolutions()
        {
            return _NQueenProblemSolutions;
        }

        public void SetNQueenProblemSolutions(List<bool[,]> NQueenProblemSolutions)
        {
            _NQueenProblemSolutions = NQueenProblemSolutions;
        }

        public void AddNQueenProblemSolution(bool[,] NQueenProblemSolutionToBeAdded)
        {
            GetNQueenProblemSolutions().Add(NQueenProblemSolutionToBeAdded);
        }

        public NQueensProblem()
        {
            _SizeOfField = 8;
            bool[,] DefaultChessField = new bool[GetSizeOfField(), GetSizeOfField()];
            _ChessField = DefaultChessField;
            _ColumnsThatHaveQueensAlready = new List<int>();
            _NQueenProblemSolutions = new List<bool[,]>();
        }

        public NQueensProblem(int NumberOfQueens)
        {
            _SizeOfField = NumberOfQueens;
            bool[,] DefaultChessField = new bool[GetSizeOfField(), GetSizeOfField()];
            _ChessField = DefaultChessField;
            _ColumnsThatHaveQueensAlready = new List<int>();
            _NQueenProblemSolutions = new List<bool[,]>();
            InitializeChessFieldForNumberOfQueens(NumberOfQueens);
        }

        public void InitializeChessFieldForNumberOfQueens(int NumberOfQueens)
        {
            PutQueenOnRandomPositionInFirstRow(MaximumPosition:NumberOfQueens);
            CanNextQueenBePlaced(CurrentRow: 1);
        }

        public void PutQueenOnRandomPositionInFirstRow(int MaximumPosition)
        {
            Random RandomizerForFirstQueenPosition = new Random();
            int FirstQueenColumnIndex = RandomizerForFirstQueenPosition.Next(0, MaximumPosition);
            PutQueenOnChessField(0, FirstQueenColumnIndex);
        }

        public void PutQueenOnChessField(int RowIndexOfQueenToBeAdded, int ColumnIndexOfQueenToBeAdded)
        {
            GetChessField()[RowIndexOfQueenToBeAdded, ColumnIndexOfQueenToBeAdded] = true;
            AddQueenColumnToColumnsThatHaveQueensAlready(QueenColumnToAdd: ColumnIndexOfQueenToBeAdded);
        }

        public void RemoveQueenFromChessField(int RowIndexOfQueenToBeRemoved, int ColumnIndexOfQueenToBeRemoved)
        {
            GetChessField()[RowIndexOfQueenToBeRemoved, ColumnIndexOfQueenToBeRemoved] = false;
            RemoveQueenColumnFromColumnsThatHaveQueensAlready(QueenColumnToRemove: ColumnIndexOfQueenToBeRemoved);
        }

        public void AddQueenColumnToColumnsThatHaveQueensAlready(int QueenColumnToAdd)
        {
            GetColumnsThatHaveQueensAlready().Add(QueenColumnToAdd);
        }

        public void RemoveQueenColumnFromColumnsThatHaveQueensAlready(int QueenColumnToRemove)
        {
            GetColumnsThatHaveQueensAlready().Remove(QueenColumnToRemove);
        }

        public bool CanNextQueenBePlaced(int CurrentRow)
        {
            for (int RowIndex = CurrentRow; RowIndex < GetSizeOfField(); RowIndex++)
            {
                for (int ColumnIndex = 0; ColumnIndex < GetSizeOfField(); ColumnIndex++)
                {
                    if (!GetColumnsThatHaveQueensAlready().Contains(ColumnIndex))
                    {
                        PutQueenOnChessField(RowIndexOfQueenToBeAdded: RowIndex, ColumnIndexOfQueenToBeAdded: ColumnIndex);
 
                        if (DoesChessFieldHaveQueenConflict())
                        {
                            RemoveQueenFromChessField(RowIndexOfQueenToBeRemoved: RowIndex, ColumnIndexOfQueenToBeRemoved: ColumnIndex);
                        }
                        else
                        {
                            if (!CanNextQueenBePlaced(CurrentRow + 1))
                            {
                                RemoveQueenFromChessField(RowIndexOfQueenToBeRemoved: RowIndex, ColumnIndexOfQueenToBeRemoved: ColumnIndex);
                            }
                        }
                    }
                }
                if (IsRowOfChessFieldEmpty(RowIndexToBeChecked: RowIndex))
                {
                    return false;
                } else if (RowIndex == GetSizeOfField() -1)
                {
                    bool[,] ChessFieldCopy = new bool[GetSizeOfField(), GetSizeOfField()];
                    Array.Copy(GetChessField(), ChessFieldCopy, GetSizeOfField()*GetSizeOfField());
                    AddNQueenProblemSolution(ChessFieldCopy);
                    return false;
                }
            }
            return true;
        }

        public bool IsRowOfChessFieldEmpty(int RowIndexToBeChecked)
        {
            bool RowOfChessFieldIsEmpty = true;
            for (int ColumnIndexToBeChecked = 0; RowOfChessFieldIsEmpty == true && ColumnIndexToBeChecked < GetSizeOfField(); ColumnIndexToBeChecked++)
            {
                if (GetChessField()[RowIndexToBeChecked, ColumnIndexToBeChecked] == true)
                {
                    RowOfChessFieldIsEmpty = false;
                }
            }
            return RowOfChessFieldIsEmpty;
        }

        public bool DoesChessFieldHaveQueenConflict()
        {
            bool ChessFieldHasQueenConflict = false;
            for (int RowIndex = 0; ChessFieldHasQueenConflict == false && RowIndex < GetSizeOfField(); RowIndex++)
            {
                for (int ColumnIndex = 0; ChessFieldHasQueenConflict == false && ColumnIndex < GetSizeOfField(); ColumnIndex++)
                {
                    if (DoesChessFieldSquareContainQueen(RowIndexToBeChecked: RowIndex, ColumnIndexToBeChecked: ColumnIndex))
                    {
                        int QueenConflictColumn = ColumnIndex;
                        for (int CheckConflictCounter = 1; ChessFieldHasQueenConflict == false && RowIndex + CheckConflictCounter < GetSizeOfField(); CheckConflictCounter++)
                        {
                            if (QueenConflictColumn - CheckConflictCounter >= 0 &&
                            DoesChessFieldSquareContainQueen(RowIndex + CheckConflictCounter, QueenConflictColumn - CheckConflictCounter))
                            {
                                ChessFieldHasQueenConflict = true;
                            } else if (DoesChessFieldSquareContainQueen(RowIndex + CheckConflictCounter, QueenConflictColumn))
                            {
                                ChessFieldHasQueenConflict = true;
                            } else if (QueenConflictColumn + CheckConflictCounter < GetSizeOfField() &&
                            DoesChessFieldSquareContainQueen(RowIndex + CheckConflictCounter, QueenConflictColumn + CheckConflictCounter))
                            {
                                ChessFieldHasQueenConflict = true;
                            }
                        }
                    }
                }
            }
            return ChessFieldHasQueenConflict;
        }

        public bool DoesChessFieldSquareContainQueen(int RowIndexToBeChecked, int ColumnIndexToBeChecked)
        {
            if (GetChessField()[RowIndexToBeChecked, ColumnIndexToBeChecked] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class QueensProblem
    {
        static void Main(string[] args)
        {
            int NumberOfQueens = 8;
            NQueensProblem DefaultSizedChessField = new NQueensProblem(NumberOfQueens);

            PrintChessFieldSolutions(DefaultSizedChessField);
        }

        public static void PrintChessFieldSolutions(NQueensProblem ChessFieldToBePrinted)
        {
            foreach (bool[,] CurrentNQueenProblemsSolution in ChessFieldToBePrinted.GetNQueenProblemSolutions())
            {
                for (int RowIndex = 0; RowIndex < ChessFieldToBePrinted.GetSizeOfField(); RowIndex++)
                {
                    string OutputLine = "";
                    for (int ColumnIndex = 0; ColumnIndex < ChessFieldToBePrinted.GetSizeOfField(); ColumnIndex++)
                    {

                        if (CurrentNQueenProblemsSolution[RowIndex, ColumnIndex])
                        {
                            OutputLine += "X";
                        }
                        else
                        {
                            OutputLine += "O";
                        }
                    }
                    System.Console.WriteLine(OutputLine);

                }
                System.Console.WriteLine();
                System.Console.WriteLine();
            }
        }
    }
}