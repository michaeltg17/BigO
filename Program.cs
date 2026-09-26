// BIG O = a way to describe how fast the WORK grows when the input (n) gets bigger.
//
// Imagine n = kids standing in a line. "steps" = how many tiny things the computer does.
// Every example below starts from the same simple for-loop, then wraps it in something
// a bit bigger. Watch the steps column: that's the whole story.

void Show(string title, Func<int, long> howManySteps, int[] sizes)
{
    Console.WriteLine();
    Console.WriteLine(title);
    foreach (int n in sizes)
        Console.WriteLine($"   n = {n,6}    steps = {howManySteps(n),10}");
}

Console.WriteLine("BIG O: how fast does the work grow when n gets bigger?");
Console.WriteLine("(n = number of kids in a line, steps = work the computer does)");

// ============================================================
// 1. O(1) CONSTANT — "open the middle locker"
//
// A list is a row of lockers numbered 0, 1, 2, 3, ...
// If you want the middle locker, you just say "locker # n/2" and open it.
// No counting, no loop. 10 kids? 1 step. 1 million kids? still 1 step.
// (this is why the old code had arr[n / 2] — jump straight to one locker by number)
// ============================================================
long O1(int n)
{
    var lockers = new int[n];
    lockers[n / 2] = 1;   // open the middle locker: 1 step
    return 1;
}
Show("O(1) constant — open the middle locker", O1, new[] { 10, 100, 1000, 10000 });

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
// So we don't check every number (0, 1, 2, 3, ... would be the O(n) example below).
// We always check the MIDDLE, which throws away half of all remaining numbers
// every single step:
// n numbers, then n/2, then n/4, then n/8, ... until one is left.
// Cutting in half again and again = "log"
//
// In THIS code, numberToSearch is the ONE number we are looking for.
// We pick numberToSearch = n - 1 (a high number) = the WORST case.
// The worst case takes ~log n steps, so the WHOLE algorithm is O(log n)
// — even a random numberToSearch can never do worse than this.
// low and high are the "still alive" range, and mid is its middle.
// The search STOPS the moment mid equals numberToSearch.
//
// Example, n = 100, numberToSearch = 99 (range 0..100, sorted):
//   check 50 -> too low  -> only 51..100 is alive
//   check 75 -> too low  -> only 76..100 is alive   (NOT 25! it's dead)
//   check 88 -> too low  -> only 89..100
//   check 94 -> too low  -> only 95..100
//   check 97 -> too low  -> only 98..100
//   check 99 -> FOUND! stop. 6 checks instead of up to 99.
// ============================================================
long OLogN(int n)
{
    int numberToSearch = n - 1;   // the number we are looking for (a high number = worst case)
    int low = 0;
    int high = n;
    long steps = 0;
    while (low < high)
    {
        steps++;                                    // one check
        int mid = low + (high - low) / 2;           // always check the middle
        if (mid == numberToSearch) return steps;    // FOUND IT! the search ends here
        if (mid < numberToSearch) low = mid + 1;    // too low -> keep top half
        else high = mid;                            // too high -> keep bottom half
    }
    return steps;
}
Show("O(log n) logarithmic — binary search (cut in half each time)", OLogN, new[] { 10, 100, 1000, 10000 });

// ============================================================
// 3. O(n) LINEAR — "count all the kids"
//
// The base example: one simple for-loop. Visit every kid once.
// 10 kids = 10 steps. 1000 kids = 1000 steps. Work grows straight up with n.
// ============================================================
long OLinear(int n)
{
    long steps = 0;
    for (int i = 0; i < n; i++)  // visit every kid once
        steps++;
    return steps;
}
Show("O(n) linear — one simple for-loop, count every kid", OLinear, new[] { 10, 100, 1000, 10000 });

// ============================================================
// 4. O(n log n) LINEARITHMIC — "every kid plays the guessing game"
//
// The simple for-loop (n kids) — and INSIDE it, the cut-in-half game (log n steps).
// So the work is n x log n. This is how fast the best sorting algorithms go.
// ============================================================
long ONLogN(int n)
{
    long steps = 0;
    for (int i = 0; i < n; i++)          // for every kid...
        for (int j = n; j > 1; j /= 2)   // ...play the cut-in-half game (log n steps)
            steps++;
    return steps;
}
Show("O(n log n) linearithmic — for-loop with the halving game inside", ONLogN, new[] { 10, 100, 1000, 10000 });

// ============================================================
// 5. O(n^2) QUADRATIC — "every kid shakes hands with every kid"
//
// The simple for-loop, with ANOTHER simple for-loop inside it.
// Kid 1 shakes hands with n kids, kid 2 with n kids, ... n times n = n^2.
// ============================================================
long OQuadratic(int n)
{
    long steps = 0;
    for (int i = 0; i < n; i++)       // every kid...
        for (int j = 0; j < n; j++)   // ...shakes hands with every kid
            steps++;
    return steps;
}
Show("O(n^2) quadratic — for-loop inside for-loop (handshakes)", OQuadratic, new[] { 10, 100, 1000, 5000 });

// ============================================================
// 6. O(n^3) CUBIC — "every handshake gets n high-fives"
//
// Same idea, one more for-loop inside.
// n kids x n kids x n high-fives = n^3
// ============================================================
long OCubic(int n)
{
    long steps = 0;
    for (int i = 0; i < n; i++)       // every kid...
        for (int j = 0; j < n; j++)   // ...with every kid...
            for (int k = 0; k < n; k++)  // ...does n high-fives
                steps++;
    return steps;
}
Show("O(n^3) cubic — three for-loops stacked", OCubic, new[] { 10, 100, 500 });

// ============================================================
// 7. O(2^n) EXPONENTIAL — "every possible photo"
//
// Take a photo of EVERY group of kids. Each kid has 2 choices:
// IN the photo or OUT. So the total is 2 x 2 x 2 ... (n times) = 2^n.
// This is a function that calls itself twice per kid (recursion).
// Doubles every single step — it explodes.
// ============================================================
long OExponential(int n)
{
    long steps = 0;
    void Try(int kid)
    {
        if (kid == n) { steps++; return; }  // all kids decided = one photo taken
        Try(kid + 1);   // kid is OUT of the photo
        Try(kid + 1);   // kid is IN the photo
    }
    Try(0);
    return steps;
}
Show("O(2^n) exponential — every possible photo (2 choices per kid)", OExponential, new[] { 1, 5, 10, 15, 20 });

// ============================================================
// 8. O(n!) FACTORIAL — "every possible lineup"
//
// In how many orders can n kids stand in a line?
// 1st spot: n options. 2nd spot: n-1 options left. 3rd: n-2 ...
// Total = n * (n-1) * (n-2) * ... * 1  which is written "n!" (n factorial).
// Even a tiny n is way, way bigger than 2^n. This is why we never do this for big n.
// ============================================================
long OFactorial(int n)
{
    long steps = 0;
    var used = new bool[n];
    void Try(int spot)
    {
        if (spot == n) { steps++; return; }  // line is full = one lineup found
        for (int i = 0; i < n; i++)
            if (!used[i])                    // kid i isn't in the line yet
            {
                used[i] = true;
                Try(spot + 1);
                used[i] = false;
            }
    }
    Try(0);
    return steps;
}
Show("O(n!) factorial — every possible lineup of kids", OFactorial, new[] { 1, 3, 5, 7, 8 });
