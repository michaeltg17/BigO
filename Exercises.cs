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