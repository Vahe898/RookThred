using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace RookThred
{
    public class TwoPoint
    {
        public Cord First { get; set; }
        public Cord Second { get; set; }
        public List<Cord> CanVisit { get; set; }

        public int Count { get; set; }

        public TwoPoint(Cord first, Cord second)
        {
            First = first;
            Second = second;

        } 
        
        public TwoPoint(Cord first, Cord second,List<Cord> canVisit,int count)
        {
            First = first;
            Second = second;
            CanVisit = canVisit;
            Count = count;
        }
        

    }
    internal class Program
    {
        static object locker = new object();/*irar mej chmtnelu hamara*/
        public static void ThredMethod(TwoPoint Points )
        {
                List<Cord> CanVisit = new List<Cord>();
                int a = 0;
                int count = Reaching(Points.First, Points.Second, ref CanVisit,  a);
                while (Points.Second != Points.First)
                {

                    Console.WriteLine(Points.Second);
                    Cord prefiusMove = GettingPreviousMove(CanVisit, Rook.Moves(Points.Second));
                    if (Rook.Reached(Rook.Moves(prefiusMove), Points.First))
                    {
                        Console.WriteLine(prefiusMove);
                        break;
                    }
                    CanVisit.RemoveAll(item => Rook.Moves(Points.Second).Contains(item));
                    Points.Second = prefiusMove;
                }
                Console.WriteLine(Points.First);
        }
        public static int Reaching(Cord first, Cord last, ref List<Cord> CanVisit,  int count)/*en listna tali qayleri minchev vor en qaylne lini vor nshel enq*/
        {
            //object locker = new();
            lock (locker)
            {
                List<Cord> tests = new List<Cord>();
                if (count == 0)
                {
                    CanVisit = Rook.Moves(first);
                    count++;
                }
                foreach (Cord c in CanVisit)
                {
                    if (Rook.Reached(CanVisit, last))
                    {
                        return count;
                    }
                    else
                    {
                        tests.AddRange(Rook.Moves(c));
                    }
                }
                CanVisit.AddRange(tests);
                count++;
                return Reaching(first, last, ref CanVisit,  count);
            }
        }
            
        

        public static Cord GettingPreviousMove(List<Cord> InList,List<Cord> FromList)/*mi qayl hetna tali te urduca eke*/
        {
            foreach (Cord item in InList)
            {               
                if (FromList.Contains(item))
                {                   
                    return item;
                }
            }
            return new Cord(0,0);

        }
        static void Main(string[] args)
        {
            Cord k = new Cord(2, 1);
            Cord l = new Cord(3, 6);
            (int, Cord) tuple;
            List<(int, Cord)> ListOfMinMoves = new List<(int, Cord)>();
            foreach (Cord item in Rook.Moves(k))
            {
                int count = 0;
                List<Cord> a = new List<Cord>();
                Thread newThread = new Thread(() => {
                    int result = Reaching(item, l,ref a, count);
                    ListOfMinMoves.Add((result, item));
                    Console.WriteLine("Thread result: " + result);
                });
                newThread.Start();
            }
            var res = ListOfMinMoves.OrderBy(item => item.Item1);
            TwoPoint kl = new TwoPoint(res.First().Item2, l);           
            ThredMethod(kl);
        }
    }
}
