﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Input;

namespace DatabaseSandbox {
    class Program {
        static void Main(string[] args) {
            Program program = new Program();
            program.start();
        }

        void start() {
            StaffDAO staffDAO = new StaffDAO();

            staffDAO
                .getAll()
                .ForEach(Console.WriteLine);

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
