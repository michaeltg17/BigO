// O(n) - outer shrinks inner shrinks = linear
long Work(int n)
{
    long work = 0;
    for (int i = 1; i <= n; i *= 2)
        for (int j = 0; j < n / i; j++)
            work++;
    return work;
}

i       n       n / i (work)
1       100     +100          
2       100     +50          
4       100     +25          
8       100     +12       

// O(n) - outer shrinks inner shrinks = linear
long Work(int n)
{
    long work = 0;
    for (int i = n; i > 1; i /= 2)
    {
        for (int j = 0; j < i; j++)
            work++;
    }
    return work;
}

i           n       i (work)
100         100     +100          
50          100     +50          
25          100     +25          
12          100     +12 

// O(n log n) - outer shrinks inner flat = log n (shrinks) * n (flat)
long Work(int n)
{
    long work = 0;
    for (int k = 1; k < n; k *= 2)
        for (int m = 0; m < n; m++)
            work++;
    return work;
}

k           n       (work)
1           100     +100          
2           100     +100          
4           100     +100        
8           100     +100

// O(n log n) - outer flat + inner shrinks
long Work(int n)
{
    long work = 0;
    for (int i = 1; i <= n; i++)
    {
        for (int j = i; j <= n; j += i)
            work++;
    }
    return work;
}

i           n       j       (work)
1           100     1       +1
                    2       +1
                            total +100 
2           100     2       +1
            100     4       +1
                            total +50         
3           100     3       +33        
4           100     4       +25

//O(n log n) - outer flat mid shrink
long Work(int n)
{
    long work = 0;
    for (int i = 1; i <= n; i++)
        for (int j = i; j <= n; j += i)
            if (n % j == 0)
                work++;
    return work;
}

i           n       j       (work)
1           100     1       +1
                    2       +1
                            total +100 
2           100     2       +1
            100     4       +1
                            total +50         
3           100     3       +33        
4           100     4       +25

// O(log n) - shrinks
long Work(int n)
{
    long work = 0;
    int x = n;
    while (x > 0)
    {
        x &= (x - 1);
        work++;
    }
    return work;
}

x           n       (work)
100         100     1
96          100     2
64          100

// O(n log log n) - outer linear + inner shrinks and shrinks again because of the composite array (each time there are more items as true)
long Work(int n)
{
    var isComposite = new bool[n + 1];
    long work = 0;
    for (int i = 2; i <= n; i++)
    {
        if (!isComposite[i])               // i is prime
            for (int j = i * i; j <= n; j += i)
            {
                isComposite[j] = true;
                work++;
            }
    }
    return work;
}

i           n       j        (work)
2           100     4       1
                    6       1
3           100             2
4           100

// O(n) - outer linear + inside linear
long Work(int n)
{
    long work = 0;
    int cap = 1;
    for (int i = 0; i < n; i++)
    {
        if (i == cap)            // full -> grow
        {
            work += cap;         // copying cap elements costs cap
            cap *= 2;
        }
        work++;                  // the append itself
    }
    return work;
}

i           n       cap       work
0           100     1         1
1           100     1         3 
2           100     2         6
3           100     4         7
4           100     4         12
5           100     8         13
7           100     8         14
8           100     8         23
9           100     16        24

//O(n log n) - outer linear inner shrinks
Q7 (to close the round):
long Work(int n)
{
    long work = 0;
    for (int i = 1; i <= n; i++) //O(n)
        for (int j = 1; j <= n; j += i) //O(log n)
            work++;
    return work;
}

i           n       j       work
1           100     1       1
            100     2       2
            100     3       3
2           100     1       
            100     3

//O(2!)
function mystery(n) {
  if (n <= 1) return 1;
  return mystery(n - 1) + mystery(n - 2);
}

n       caller1     caller2     calls1      calls2      total
2       1          0            0           0           2
3       2          1            2           0           1
4       3          2            
        2          1