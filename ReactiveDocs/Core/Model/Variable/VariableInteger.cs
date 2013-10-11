﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDocs.Core.Model.Variable
{
    public class VariableInteger : VariableBase
    {
        public override VariableType Type
        {
            get
            {
                return VariableType.Integer;
            }
        }
    }
}