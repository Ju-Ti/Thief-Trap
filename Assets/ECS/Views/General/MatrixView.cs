using System;

namespace ECS.Views.General
{
    public class MatrixView : LinkableView
    {
        internal int[,] ActualMatrix;

        public void SaveAndPrintMatrix(int[,] matrix)
        {
            ActualMatrix = matrix;
            
            string arr = "";
            
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    arr += matrix[i,j] + ",";
                }
                arr += "\n";
            }
            print (arr);
        }
    }
}