using System;
using System.Collections.Generic;
using System.Text;

namespace HashMap_Tutorial_CS
{
    class Program
    {
        static void Main(string[] args)
        {
	        HashMap<int, string> map = new HashMap<int, string>();
	        map.Place(1, "A");
	        map.Place(2, "B");
	        map.Place(3, "C");
	        map.Place(4, "D");
	        map.Place(5, "E");
	        map.Place(6, "F");

	        map[7 ] = "G";
	        map[8 ] = "H";
	        map[9 ] = "I";
	        map[10] = "J";
	        map[11] = "K";
	        map[12] = "L";
	        map[13] = "M";
	        map[14] = "N";
	        map[15] = "O";
	        map[16] = "P";
	        map[17] = "Q";
	        map[18] = "R";
	        map[19] = "S";

	        Console.WriteLine(map[1 ]);
	        Console.WriteLine(map[2 ]);
	        Console.WriteLine(map[3 ]);
	        Console.WriteLine(map[4 ]);
	        Console.WriteLine(map[5 ]);
	        Console.WriteLine(map[6 ]);
	        Console.WriteLine(map[7 ]);
	        Console.WriteLine(map[8 ]);
	        Console.WriteLine(map[9 ]);
	        Console.WriteLine(map[10]);
	        Console.WriteLine(map[11]);
	        Console.WriteLine(map[12]);
	        Console.WriteLine(map[13]);
	        Console.WriteLine(map[14]);
	        Console.WriteLine(map[15]);
	        Console.WriteLine(map[16]);
	        Console.WriteLine(map[17]);
	        Console.WriteLine(map[18]);
	        Console.WriteLine(map[19]);

	        map.Remove(1);
	        map.Remove(2);
	        map.Remove(3);
	        map.Remove(4);
	        map.Remove(5);
	        map.Remove(6);

	        Console.WriteLine(map[1 ]);
	        Console.WriteLine(map[2 ]);
	        Console.WriteLine(map[3 ]);
	        Console.WriteLine(map[4 ]);
	        Console.WriteLine(map[5 ]);
	        Console.WriteLine(map[6 ]);
	        Console.WriteLine(map[7 ]);
	        Console.WriteLine(map[8 ]);
	        Console.WriteLine(map[9 ]);
	        Console.WriteLine(map[10]);
	        Console.WriteLine(map[11]);
	        Console.WriteLine(map[12]);
	        Console.WriteLine(map[13]);
	        Console.WriteLine(map[14]);
	        Console.WriteLine(map[15]);
	        Console.WriteLine(map[16]);
	        Console.WriteLine(map[17]);
	        Console.WriteLine(map[18]);
	        Console.WriteLine(map[19]);

	        map.Clear();

	        Console.WriteLine(map[1 ]);
	        Console.WriteLine(map[2 ]);
	        Console.WriteLine(map[3 ]);
	        Console.WriteLine(map[4 ]);
	        Console.WriteLine(map[5 ]);
	        Console.WriteLine(map[6 ]);
	        Console.WriteLine(map[7 ]);
	        Console.WriteLine(map[8 ]);
	        Console.WriteLine(map[9 ]);
	        Console.WriteLine(map[10]);
	        Console.WriteLine(map[11]);
	        Console.WriteLine(map[12]);
	        Console.WriteLine(map[13]);
	        Console.WriteLine(map[14]);
	        Console.WriteLine(map[15]);
	        Console.WriteLine(map[16]);
	        Console.WriteLine(map[17]);
	        Console.WriteLine(map[18]);
	        Console.WriteLine(map[19]);
        }
    }
}
