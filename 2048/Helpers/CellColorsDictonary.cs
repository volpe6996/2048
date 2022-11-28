﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048.Helpers
{
    public class CellColorsDictonary
    {
        public Dictionary<int, string> Colors { get; set; }

        public CellColorsDictonary()
        {
            Colors = new Dictionary<int, string>()
            {
                {0, "#CDC1B4"},
                {2, "#EEE4DA"},
                {4, "#EDE0C8"},
                {8, "#F2B179"},
                {16, "#F59563"},
                {32, "#F67C5F"},
                {64, "#F65E3B"},
                {128, "#EDCF72"},
                {256, "#EDCC61"},
                {512, "#EDC850"},
                {1024, "#EDC53F"},
                {2048, "#EDC22E"},
                {4096, "#000000"},
            };
        }
    }
}
