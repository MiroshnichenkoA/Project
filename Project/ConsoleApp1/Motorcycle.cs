using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Motorcycle
    {
        public string Name { get; set; }
        public string Model { get; set; }
        private int _vinNumber = 111;
        int _odometr;
        public int Odometr { get => _odometr; set => _odometr = value; }
        public Motorcycle(string name)
        {
            Name = name;
        }

        public int GetVinNumer ()
        {
            return _vinNumber;
        }
        public override string ToString()
        {
            return $"Motorcycle: {Name}, Model: {Model}, Odometr: {Odometr}; VinNumber: {GetVinNumer()}";
        }
    }
}
