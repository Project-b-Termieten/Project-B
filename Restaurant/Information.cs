static class Information
{
    public static void DisplayMap()
    {
        Console.WriteLine(
        @"Map van het restaurant:

+----+----+----+----+----+----+----+----+----+----+----+----+----+----+----+
|_________bar_____________|  |                                   |   wc    |
+  __ __ __ __ __ __ __ __ __|                                   |_________+
|                                                                          |
+          _____              ______               ____           ____     +
|     |   /     \   |     |  /      \  |       |  /    \ |    |  /    \  | |
+      |  | 16  |  |       | |  15  | |           | 14 |         | 13 |    +
|     |   \_____/   |     |  \______/  |       |  \____/ |    |  \____/  | |
+           ____                    ____                    ____           +
|       |  /    \  |            |  /    \  |            |  /    \  |       |
+          |  9 |                  | 10 |                  | 12 |          +
|       |  \____/  |            |  \____/  |            |  \____/  |       |
+                                   ____                                   +
|          | [4] |              |  /    \  |              | [8] |          |
+                                  | 11 |                                  +
|   | [1] |       | [3] |       |  \____/  |       | [5] |      | [7] |    |
+                                                                          +
|          | [2] |                                        | [6] |          |
+----+----+----+----+----+----+----/    \----+----+----+----+----+----+----+
                                  [Entree]
");
    }

    public static void ContactInfo()
    {
        Console.WriteLine("+----------------------------------------------+");
        Console.WriteLine("| Jake’s restaurant information:               |");
        Console.WriteLine("| Location: Wijnhaven 107, 3011 WN in Rotterdam|");
        Console.WriteLine("| Phone: (123) 456-7890                        |");
        Console.WriteLine("| Email: jakes@example.com                     |");
        Console.WriteLine("+----------------------------------------------+");
    }
}
