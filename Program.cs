Console.WriteLine("BIG O: how fast does the work grow when n gets bigger?");
Console.WriteLine("Ordered from lower growth to higher");


var title1 = "O(1) CONSTANT — get the mid number";
// ============================================================
// 1. O(1) CONSTANT — get the mid number
//
// N: 10, 100, 1000, doesn't matter, same work, same growth, constant
// ============================================================
long O1(int n)
{
    var mid = n / 2;
    return 1;
}
Show(title1, O1, [10, 100, 1000, 10000]);


var title2 = "O(log log n) DOUBLE LOGARITHMIC — how many logs fit in n";
// ============================================================
// 2. O(log log n) DOUBLE LOGARITHMIC — how many logs fit in n
//
// Take the log of n (halve until one is left = one log), then
// take the log of THAT, and so on until one is left. Each "log"
// is one unit of work. n shrinks to log n, then log log n, then
// ~1, so only ~log log n logs fit. n going 1000x bigger adds
// barely a step — the flattest curve after O(1).
// ============================================================
long OLogLogN(int n)
{
    long work = 0;
    long x = n;
    while (x > 1)
    {
        long y = x;
        long halvings = 0;
        while (y > 1) { y /= 2; halvings++; }   // one log: halve until one is left
        x = halvings;                            // next log starts from log(x)
        work++;                                  // one "log" done
    }
    return work;
}
Show(title2, OLogLogN, [2, 16, 256, 100000, 10000000]);


var title3 = "O(log n) LOGARITHMIC — BINARY SEARCH";
// ============================================================
// 3. O(log n) LOGARITHMIC — BINARY SEARCH
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
Show(title3, OLogN, [10, 100, 1000, 10000]);


var title4 = "O(sqrt n) SQUARE ROOT — is n prime? (trial division)";
// ============================================================
// 4. O(sqrt n) SQUARE ROOT — IS N PRIME? (trial division)
//
// To check if n is prime, try dividing by 2, 3, 4, ... but only
// while i*i <= n. Why stop at sqrt n? If n = a * b, one of a or
// b must be <= sqrt(n) — they can't BOTH be bigger, because
// bigger * bigger is bigger than n. So if nothing up to sqrt(n)
// divides n, nothing ever will. i tops out at sqrt n: n going
// 100x bigger only makes work go 10x bigger.
//
// We test real primes (7, 97, 997, ...) so the loop always runs
// all the way to sqrt n — the worst case. 999983 is the largest
// prime under one million.
// ============================================================
long OIsPrime(int n)
{
    long work = 0;
    for (int i = 2; i * i <= n; i++)
    {
        work++;
        if (n % i == 0) return work;   // n = i * (n/i): composite, stop early
    }
    return work;                        // prime: checked everything up to sqrt n
}
Show(title4, OIsPrime, [7, 97, 997, 9973, 999983]);


var title5 = "O(n) LINEAR — classic foreach";
// ============================================================
// 5. O(n) LINEAR — classic foreach
// ============================================================
long OLinear(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        work++;
    return work;
}
Show(title5, OLinear, [10, 100, 1000, 10000]);


var title6 = "O(n log n) linearithmic — classic foreach + binary search";
// ============================================================
// 6. O(n log n) LINEARITHMIC — classic foreach + halving counter
// ============================================================
long ONLogN(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        for (int j = n; j > 1; j /= 2) // halving counter so log n
            work++;
    return work;
}
Show(title6, ONLogN, [10, 100, 1000, 10000]);


var title7 = "O(n^2) QUADRATIC — every item with every item";
// ============================================================
// 7. O(n^2) QUADRATIC — every item with every item
//
// n^2 is a polynomial (degree 2). n^3 would be CUBIC, n^4 QUARTIC —
// all the same family (polynomial), just different degrees.
// ============================================================
long OQuadratic(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            work++;
    return work;
}
Show(title7, OQuadratic, [10, 100, 1000, 5000]);


var title8 = "O(n^2 log n) QUADRATIC LOGARITHMIC — every item with every item, plus a halving counter at each pair";
// ============================================================
// 8. O(n^2 log n) QUADRATIC LOGARITHMIC — the quadratic grid, plus a halving counter at each pair
//
// Every item with every item (that's n^2), and at every pair we
// run the halving counter (that's log n). The log factor means it
// is NOT a pure polynomial (those are clean n^k like n^2 or n^3),
// but it IS polynomial-time: it grows slower than n^3, so it stays
// in the polynomial family. No one-word name exists for it, so we
// just describe the two factors: quadratic + logarithmic.
// ============================================================
long ON2LogN(int n)
{
    long work = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            for (int k = n; k > 1; k /= 2) // halving counter so log n
                work++;
    return work;
}
Show(title8, ON2LogN, [10, 100, 1000, 3000]);


var title9 = "O(2^n) EXPONENTIAL — coin flipping";
// ============================================================
// 9. O(2^n) EXPONENTIAL — coin flipping
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
Show(title9, OExponential, [1, 2, 3, 5, 10]);


var title10 = "O(n!) FACTORIAL — every possible order of items";
// ============================================================
// 10. O(n!) FACTORIAL — every possible order of items
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
Show(title10, OFactorial, [1, 2, 3, 5, 10]);


var title11 = "O(2^2^n) DOUBLE EXPONENTIAL — groups of coin combos";
// ============================================================
// 11. O(2^2^n) DOUBLE EXPONENTIAL — groups of coin combos
//
// 2^n was: every combo of n coins. Now go one level up and count
// every possible GROUP of those combos: each combo gets a yes/no
// (in the group or out), and the yes/no's multiply: 2^(2^n) groups.
//
// n = 2: 4 combos (HH, HT, TH, TT) -> 2x2x2x2 = 16 groups:
//   (none)
//   (HH)    (HT)    (TH)    (TT)
//   (HH,HT) (HH,TH) (HH,TT) (HT,TH) (HT,TT) (TH,TT)
//   (HH,HT,TH) (HH,HT,TT) (HH,TH,TT) (HT,TH,TT)
//   (HH,HT,TH,TT)
// 1 + 4 + 6 + 4 + 1 = 16. Order inside a group doesn't matter —
// (HH,HT) and (HT,HH) are the same group.
//
// n = 4 is already 65536. n = 5 would be 4 billion. n = 10 is a
// number with over 300 digits. We literally cannot count further.
// ============================================================
long ODoubleExponential(int n)
{
    long work = 0;
    long limit = 1L << (1 << n);   // 2^(2^n)
    for (long i = 0; i < limit; i++)
        work++;
    return work;
}
Show(title11, ODoubleExponential, [1, 2, 3, 4]);


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
