﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitConsumer.ConsumeMessages();
        }
    }
}
