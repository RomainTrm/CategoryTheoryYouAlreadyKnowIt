using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SocratesCSharp
{
    public class UnitTest1
    {
        int ToInt(decimal x) => (int)x;
        bool ToBool(int x) => x % 2 == 0;

        bool DecimalToBool(decimal x) => ToBool(ToInt(x));

        bool IsEven(int x) => x % 2 == 0;


        List<bool> ToListEven(List<int> x) => x.Select(IsEven).ToList();
        
    }
}