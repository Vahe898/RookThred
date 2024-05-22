using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookThred
{
    internal class Rook
    {
        public int NumberOfRook { get; set; }
        public int LetterOfRook { get; set; }


        public Rook(int num,int let)
        { 
            NumberOfRook = num;
            LetterOfRook = let;
        }
        public static List<Cord> Moves(Cord cord)/*qaylerna tali taxtaki mej*/
        {
            List<Cord> ListOfValidMoves = new List<Cord>();
            int[] rowMoves = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] colMoves = { -1, 1, -2, 2, -2, 2, -1, 1 };
            //cord.Number = NumberOfRook;
            //cord.Letter = LetterOfRook;
            
            for(int i = 0; i < 8; i++)
            {
                Cord CanMoove = new Cord(cord.Number + rowMoves[i], cord.Letter + colMoves[i]);
                if (Cord.IsValid(CanMoove))
                {
                    ListOfValidMoves.Add(CanMoove);
                }              
            }
            return ListOfValidMoves;

        }

        
        public static bool Reached(List<Cord> MovingCords, Cord destination)/*lista tali nayuma sax qayleri mej hasaci kordinaty ka te che*/
        {
            
            foreach(Cord c in MovingCords)
            {
                if (c == destination)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
