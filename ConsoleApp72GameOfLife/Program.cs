bool[,] cur = new bool[Console.LargestWindowHeight, Console.LargestWindowWidth];
bool[,] next = new bool[Console.LargestWindowHeight, Console.LargestWindowWidth];
int row = cur.GetLength(0);
int col = cur.GetLength(1);
//Blinker
//cur[0, 1] = true; cur[1, 1] = true; cur[2, 1] = true;
int x=2, y =0;
//Glider
cur[x+1, y+1] = true; cur[x+1, y+3] = true; cur[x+2, y+2] = true; 
cur[x+2, y+3] = true; cur[x+3, y+2] = true;

//Toad:
//cur[1, 2] = true; cur[1, 3] = true; cur[1, 4] = true;cur[2, 1] = true; cur[2,2]=true; cur[2,3]=true;

x = 20;y = 60;
//Lightweight SpaceShip:
cur[x+0, y] = true; cur[x, y+1] = true; cur[x, y+2] = true; cur[x, y+3] = true;
cur[x+1, y] = true; cur[x+1, y+4] = true;
cur[x+2, y] = true;
cur[x+3, y+1] = true; cur[x+3, y+4] = true;


Console.CursorVisible = false;
Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
//InitPaint();
while (true)
{
  PaintMat(); //paints current
  Calc();
  (cur, next) = (next, cur);
  Thread.Sleep(10);
}

void Calc()
{
  for (int i = 0; i < row; i++)
    for (int j = 0; j < col; j++)
    {
      next[i, j] = false; //איפוס המערך
      int neighs = Neighbors(i, j);
      if (neighs == 2 && Exists(i, j) || neighs == 3)
        next[i, j] = true;
    }
}

void InitPaint()
{
  for (int i = 0; i < row; i++)
    for (int j = 0; j < col; j++)
      Paint(cur[i, j], i, j);
}
void PaintMat()
{
  for (int i = 0; i < row; i++)
    for (int j = 0; j < col; j++)
      if (cur[i, j] != next[i, j])
        Paint(cur[i, j], i, j);
}

void Paint(bool v, int y, int x)
{
  if (v)
    Console.ForegroundColor = ConsoleColor.Yellow;
  else
    Console.ForegroundColor = ConsoleColor.Black;
  Console.SetCursorPosition(x, y);
  Console.Write("O");//█");
}
bool Exists(int i, int j)
{
  if (i < 0 || j < 0 || i >= row || j >= col)
    return false;
  return cur[i, j];
}

int Neighbors(int i, int j)
{
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