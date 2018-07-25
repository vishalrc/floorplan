using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JLT.MySql.DataAccess
{
    enum ConnectionErrors
    {
        ConnectionError_BlankConnectionString = 10000001,
        ConnectionError_InvalidConnectionString = 10000002
    }
}