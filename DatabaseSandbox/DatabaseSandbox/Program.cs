﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.SQLInterface;

namespace DatabaseSandbox {
    class Program {
        static void Main(string[] args) {
            Program program = new Program();
            program.start();
        }

        void start() {
            SQLInterface sqlInterface = new SQLInterface(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);


        }
    }
}
