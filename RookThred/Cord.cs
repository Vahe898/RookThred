using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RookThred
{
    public struct Cord
    {
        public int Number { get; set; }
        public int Letter { get; set; }
        public Cord(int num,int letter)
        { 
            Number = num;
            Letter = letter;
        }
       
        public static bool IsValid(Cord cord)
        {
            if(cord.Number<8 && cord.Number>=0 && cord.Letter>=0 && cord.Letter < 8)
            {
                return true;
            }
            return false;
        }

        public static bool operator ==(Cord cord1, Cord cord2)
        {
            //if (cord1.Letter == cord2.Letter && cord2.Number == cord1.Number)
            //{
            //    return true;
            //}
            //return false;
            return cord1.Letter == cord2.Letter && cord2.Number == cord1.Number;
        }
        public static bool operator !=(Cord cord1, Cord cord2)
        {
            return !(cord1 == cord2);
        }

        public override string ToString()
        {
            return ($"Number is:{Number} and Letter is:{Letter} ");
        }
    }
}
