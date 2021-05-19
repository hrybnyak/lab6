using lab6.Context;
using lab6.MigrationTests;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var mt = new MigrationTest();
            mt.MigrationTest1();
            mt.MigrationTest2();
        }
    }
}
