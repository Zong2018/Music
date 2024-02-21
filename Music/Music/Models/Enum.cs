using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public enum PlayMode
    {
        Order = 0,
        Loop = 1,
        SingleLoop = 2,
        Random = 3
    }

    public enum PlayState
    {
        Play = 0,
        Stop = 1
    }
}
