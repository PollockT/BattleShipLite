using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteshipLiteLib.Models
{
    // 0 = empty, 1 = ship, 2 = miss, 3 = hit, 4 = sunk
    public enum GridSpotStatus
    {
        Empty, //default
        Ship,
        Miss,
        Hit,
        Sunk
    }
}
