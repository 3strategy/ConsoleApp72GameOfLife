const int maxX = 170;// Console.LargestWindowWidth returns different values in debug/exe runs...
// Since the size and positioning affects life, I reverted to predefined numbers;
const int maxY = 40;// Console.LargestWindowHeight - 7;
bool[,] cur = new bool[maxY, maxX];
bool[,] next = new bool[maxY, maxX];

//Provision the life (create various shapes)
//Blinker(70, 140); //Toad(5, 80);
Glider(2, 60); Glider(69, 80);
LWSS(225, 422); LWSS(345, 422);
Console.CursorVisible = false; Console.SetWindowSize(maxX, maxY);

while (true) //Game loop.
{
  PaintMat(); //paints current
  Calc();
  (cur, next) = (next, cur); // swap current and next frames.
  if (args.Length > 0)
    Thread.Sleep(int.Parse(args[0])); //sleep if necessary.
}

void Calc()
{
  for (int row = 0; row < maxY; row++)
    for (int x = 0; x < maxX; x++)
    {
      int neighs = Neighbors(row, x);
      // The below is logically equivalent to the rules,
      next[row, x] = false; //איפוס המערך
      if (neighs == 2 && Exists(row, x) || neighs == 3)
        next[row, x] = true;
    }
}

void PaintMat() //differentially paints the matrix 
{
  for (int row = 0; row < maxY; row++)
    for (int x = 0; x < maxX; x++)
      if (cur[row, x] != next[row, x]) //only paints cells that changed between frames.
        Paint(cur[row, x], row, x);
}

void Paint(bool v, int y, int x) //paint an individual cell
{
  if (v)
    Console.ForegroundColor = ConsoleColor.Yellow;
  else
    Console.ForegroundColor = ConsoleColor.Black;
  Console.SetCursorPosition(x, y);
  Console.Write("O");//█");
}

//returns true if the position is alive. using toroidal coordinates. 
bool Exists(int i, int j) => cur[(i + maxY) % maxY, (j + maxX) % maxX];

int Neighbors(int i, int j)
{ //counts the number of neigbors
  int count = 0;
  if (Exists(i - 1, j - 1)) count++;
  if (Exists(i - 1, j)) count++;
  if (Exists(i - 1, j + 1)) count++;
  if (Exists(i, j - 1)) count++;
  if (Exists(i, j + 1)) count++;
  if (Exists(i + 1, j - 1)) count++;
  if (Exists(i + 1, j)) count++;
  if (Exists(i + 1, j + 1)) count++;
  return count;
}

#region Shape generators
(int, int) Clean(int y, int x) => (y % maxY, x % maxX);
void Blinker(int y, int x)   //Blinker
{
  (y, x) = Clean(y, x);
  cur[y, x + 1] = true; cur[y + 1, x + 1] = true; cur[y + 2, x + 1] = true;
}
void Glider(int y, int x)
{
  (y, x) = Clean(y, x);
  cur[y + 1, x + 1] = true; cur[y + 1, x + 3] = true; cur[y + 2, x + 2] = true;
  cur[y + 2, x + 3] = true; cur[y + 3, x + 2] = true;
}

void Toad(int y, int x)
{
  (y, x) = Clean(y, x);
  cur[y + 1, x + 2] = true; cur[y + 1, x + 3] = true; cur[y + 1, x + 4] = true;
  cur[y + 2, x + 1] = true; cur[y + 2, x + 2] = true; cur[y + 2, x + 3] = true;
}

void LWSS(int y, int x) //Lightweight SpaceShip:
{
  (y, x) = Clean(y, x);
  cur[y + 0, x] = true; cur[y, x + 1] = true; cur[y, x + 2] = true; cur[y, x + 3] = true;
  cur[y + 1, x] = true; cur[y + 1, x + 4] = true;
  cur[y + 2, x] = true;
  cur[y + 3, x + 1] = true; cur[y + 3, x + 4] = true;
}
#endregion