namespace ProgrammeringsOppgave
{
    class QueensProblem
    {
        static void Main(string[] args)
        {
            
            string[,] chessField = new string[,] {  {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"},
                                                    {"O", "O", "O", "O", "O", "O", "O", "O"} };

            Random rndFirstQueen = new Random();
            int intFirstQueen = rndFirstQueen.Next(0,7);
            chessField[0, 4] = "Q";

            System.Collections.ArrayList blockedColumns = new System.Collections.ArrayList();
            blockedColumns.Add(4);

            getNextQueen(ref chessField, 1, 0);

            printChessField(ref chessField);
        }

        public static bool getNextQueen(ref string[,] chessField, int currentRow, int currentColumn) {

            for (int row = currentRow; row <=7; row++) {

                for (int column = currentColumn; column <=7; column++) {

                    chessField[row, column] = "Q";
                    if (checkQueenConflict(ref chessField)) {
                        chessField[row, column] = "O";
                        continue;
                    } else {
                        if (!getNextQueen(ref chessField, currentRow+1, 0)) {
                            chessField[row, column] = "O";
                        }
                    }
                }

                if (isRowEmpty(ref chessField, row)) {
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

        public static bool getNextQueen(ref string[,] chessField, int currentRow, System.Collections.ArrayList blockedColumns) {
            
            for (int row = currentRow; row <=7; row++) {

                for (int column = 0; column <=7; column++) {
                    if (!blockedColumns.Contains(column)) {
                        chessField[row, column] = "Q";
                        if (checkQueenConflict(ref chessField)) {
                            chessField[row, column] = "O";
                            continue;
                        } else {
                            blockedColumns.Add(column);
                            if (!getNextQueen(ref chessField, currentRow+1, blockedColumns)) {
                                chessField[row, column] = "O";
                                blockedColumns.Remove(column);
                                if (column == 7) {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
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
