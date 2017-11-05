using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racer
{
    class Race_status
    {
        int checkpoint = 0;

        public int Checkpoint
        {
            get
            {
                return checkpoint;
            }
        }

        public void OnStartFinishReached(object source, EventArgs e)
        {
            checkpoint++;
        }

        public void OnCheckpointReached(object source, EventArgs e)
        {
            checkpoint++;
        }

    }
}
