static class RestaurantMap
{
    public static string DisplayMap()
    {
        Console.WriteLine(
        @"Map van het restaurant:

+----+----+----+----+----+----+----+----+----+----+----+----+----+----+----+
|_________bar_____________|  |                                   |   wc    |
+  __ __ __ __ __ __ __ __ __|                                   |_________+
|                                                                          |
+          _____              ______               ____           ____     +
|     |   /     \   |     |  /      \  |       |  /    \ |    |  /    \  | |
+      |  | 17  |  |       | |  16  | |           | 15 |         | 13 |    +
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
}
