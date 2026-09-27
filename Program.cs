Console.WriteLine("BIG O: how fast does the work grow when n gets bigger?");

// ============================================================
// 1. O(1) CONSTANT — get the mid number
//
// N: 10, 100, 1000, doesn't matter, same cost no growth, cost is 1
// ============================================================
long O1(int n)
{
    return n / 2;
}
Show("O(1) CONSTANT — get the mid number", O1, [10, 100, 1000, 10000]);

// ============================================================
// 2. O(log n) LOGARITHMIC — BINARY SEARCH
//
// We have a SORTED list of numbers from 0 up to n (n is the HIGHEST number).
// Because it's sorted, checking the MIDDLE number tells us which half
// the answer is in:
//   - if the middle is too LOW  -> the answer is in the TOP half
//     -> we throw away the ENTIRE bottom half, all at once.
//   - if the middle is too HIGH -> the answer is in the BOTTOM half
//     -> we throw away the entire top half.
//
// n numbers, then n/2, then n/4, then n/8, ... until one is left.
// Cutting in half again and again = "log"
//
// Example, n = 100, numberToSearch = 99 (range 0..100, sorted):
//   check 50 -> too low  -> only 51..100 is alive
//   check 75 -> too low  -> only 76..100 is alive
//   check 88 -> too low  -> only 89..100
//   check 94 -> too low  -> only 95..100
//   check 97 -> too low  -> only 98..100
//   check 99 -> FOUND! stop. 6 work instead of up to 99.
// ============================================================
long OLogN(int n)
{
    int numberToSearch = n - 1;   // the number we are looking for (a high number = worst case)
    int low = 0;
    int high = n;
    long work = 0;
    while (low < high)
    {
        work++;                                    // one check
        int mid = low + (high - low) / 2;           // always check the middle
        if (mid == numberToSearch) return work;    // FOUND IT! the search ends here
        if (mid < numberToSearch) low = mid + 1;    // too low -> keep top half
        else high = mid;                            // too high -> keep bottom half
    }
    return work;
}
Show("O(log n) LOGARITHMIC — BINARY SEARCH", OLogN, [10, 100, 1000, 10000]);

// ============================================================
// 3. O(n) LINEAR — classic foreach
// ============================================================
long OLinear(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        work++;
    return work;
}
Show("O(n) LINEAR — classic foreach", OLinear, [10, 100, 1000, 10000]);

// ============================================================
// 4. O(n log n) LINEARITHMIC — classic foreach + halving counter
// ============================================================
long ONLogN(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        for (int j = n; j > 1; j /= 2) // halving counter so log n
            work++;
    return work;
}
Show("O(n log n) linearithmic — classic foreach + binary search", ONLogN, [10, 100, 1000, 10000]);

// ============================================================
// 5. O(n^2) QUADRATIC — every item with every item
// ============================================================
long OQuadratic(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            work++;
    return work;
}
Show("O(n^2) QUADRATIC — every item with every item", OQuadratic, [10, 100, 1000, 5000]);

// ============================================================
// 6. O(2^n) EXPONENTIAL — coin flipping
//
// n is the quantity of coins
// Each coin is a bit: 0 = heads, 1 = tails. The numbers from
// 0 up to 2^n - 1 ARE the combos, one combo per number.
// We count every combo: 2^n of them.
// ============================================================
long OExponential(int n)
{
    long work = 0;
    for (int mask = 0; mask < (1 << n); mask++)   // 0 .. 2^n-1 = every combo
    {
        work++;
    }
    return work;
}
Show("O(2^n) EXPONENTIAL — coin flipping", OExponential, [1, 2, 3, 5, 10]);

// ============================================================
// 7. O(n!) FACTORIAL — every possible order of items
//
// In how many orders can n items be placed?
// 1st spot: n options. 2nd spot: n-1 options left. 3rd: n-2 ...
// Total = n * (n-1) * (n-2) * ... * 1  which is written "n!" (n factorial).
// Even a tiny n is way, way bigger than 2^n. This is why we never do this for big n.
// ============================================================
long OFactorial(int n)
{
    long work = 0;
    var used = new bool[n];
    void Try(int spot)
    {
        if (spot == n) { work++; return; } // no spots, one order found
        for (int i = 0; i < n; i++)
            if (!used[i]) // item i not placed yet
            {
                used[i] = true;
                Try(spot + 1);
                used[i] = false;
            }
    }
    Try(0);
    return work;
}
Show("O(n!) FACTORIAL — every possible order of items", OFactorial, [1, 2, 3, 5, 10]);

void Show(string title, Func<int, long> bigO, int[] sizes)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine(title);
    Console.WriteLine();
    Console.WriteLine($"  {"n",8}  {"work",10}  {"n-growth",10}  {"work-growth",14}");
    long? baseWork = null;
    int baseN = 0;
    foreach (int n in sizes)
    {
        long work = bigO(n);
        if (baseWork is long b)
        {
            string nGrowth = "x" + ((double)n / baseN).ToString("0.##");
            string workGrowth = "x" + ((double)work / b).ToString("0.##");
            Console.WriteLine($"  {n,8}  {work,10}  {nGrowth,10}  {workGrowth,14}");
        }
        else
        {
            Console.WriteLine($"  {n,8}  {work,10}");
            baseWork = work;
            baseN = n;
        }
    }
}