using System.Collections;

namespace ProgrammeringsOppgave
{
    class QueensProblem
    {
        static void Main(string[] args)
        {

            //initializing the empty chess field

            string[,] chessField = new string[,] {  {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"} };

            //randomly determine the first queen's position
            Random rndFirstQueen = new Random();
            int intFirstQueen = rndFirstQueen.Next(0,7);
            chessField[0, intFirstQueen] = "Q";

            //columns cannot be used twice, therefore used columns will be blocked for checking
            System.Collections.ArrayList blockedColumns = new System.Collections.ArrayList();
            blockedColumns.Add(intFirstQueen);

            //call recursive function to add next Queen for 2nd row
            getNextQueen(ref chessField, 1, 0, ref blockedColumns);

            //print the output to console
            printChessField(ref chessField);
        }

        public static bool getNextQueen(ref string[,] chessField, int currentRow, int currentColumn, ref ArrayList blockedColumns) {
            
            for (int row = currentRow; row <=7; row++) {

                for (int column = currentColumn; column <=7; column++) {

                    //to increase performance already used columns will not be tried out
                    if (!blockedColumns.Contains(column)) {
                        //if the columns are valid, the queen will be added
                        chessField[row, column] = "Q";
                        //check for conlicts and in case of conflict, revert last added queen
                        if (checkQueenConflict(ref chessField)) {
                            chessField[row, column] = "O";
                            continue;
                        } else {
                            //in case of no conflict, the new queen's column will be blocked for further queens
                            blockedColumns.Add(column);
                            if (!getNextQueen(ref chessField, currentRow+1, 0, ref blockedColumns)) {
                                /*recursive fallback - if the following queen did not work out
                                the current queen does not work either and will be reverted*/
                                chessField[row, column] = "O";
                                //blocked column can be used again
                                blockedColumns.Remove(column);
                            }
                        }
                    }
                }

                if (isRowEmpty(ref chessField, row)) {
                    //if a row is empty, reversion is needed
                    return false;
                }
            }
            return true;
        }

        public static bool isRowEmpty(ref string[,] chessField, int row) {

            if (chessField[row,0]=="O" && chessField[row,1]=="O"
                && chessField[row,2]=="O" && chessField[row,3]=="O"
                && chessField[row,4]=="O" && chessField[row,5]=="O"
                && chessField[row,6]=="O" && chessField[row,7]=="O") {
                return true;
            }
            else {
                return false;
            }

        }

        public static bool checkQueenConflict(ref string[,] chessField) {

            bool queenConflict = false;

            for (int row = 0; row <=7; row++) {

                for (int column = 0; column <=7; column++) {

                    if (chessField[row, column] == "Q") {
                        int queenConflictColumn = column;
                        for (int checkConflictCounter = 1; row + checkConflictCounter <=7; checkConflictCounter++) {
                            
                            //check diagonal conflict left-down
                            if (queenConflictColumn-checkConflictCounter >= 0 &&
                            chessField[row + checkConflictCounter, queenConflictColumn-checkConflictCounter]=="Q") {
                                queenConflict = true;
                                break;
                            }

                            //check column conflict
                            if (chessField[row + checkConflictCounter, queenConflictColumn]=="Q") {
                                queenConflict = true;
                                break;
                            }

                            //check diagonal conflict right-down
                            if (queenConflictColumn+checkConflictCounter <= 7 &&
                            chessField[row + checkConflictCounter, queenConflictColumn+checkConflictCounter]=="Q") {
                                queenConflict = true;
                                break;
                            }

                        }

                    }

                }

            }

            return queenConflict;

        }

        public static void printChessField(ref string[,] chessField) {

            for (int row = 0; row <=7; row++) {

                string outputline = "";

                for (int column = 0; column <=7; column++) {
                    
                    outputline += chessField[row, column].ToString();

                }

                System.Console.WriteLine(outputline);

            }

        }

    }

}
