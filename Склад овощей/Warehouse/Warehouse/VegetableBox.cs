using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class VegetableBox
    {
        private readonly uint _weight;

        /// <summary>
        /// Cвойство для получения веса ящика овощей.
        /// </summary>
        public uint Weight
        {
            get
            {
                return _weight;
            }
        }

        private decimal _pricePerKilogram;

        /// <summary>
        /// Cвойство для получения и перезаписи стоимости за килограмм ящика овощей.
        /// </summary>
        public decimal PricePerKilogram
        {
            set
            {
                _pricePerKilogram = value;
            }
            get
            {
                return _pricePerKilogram;
            }
        }

        /// <summary>
        /// Конструктор ящика овощей.
        /// </summary>
        /// <param name="weight">Вес ящика овощей.</param>
        /// <param name="pricePerKilogram">Стоимость за киллограм ящика овощей.</param>
        public VegetableBox(uint weight, decimal pricePerKilogram)
        {
            _weight = weight;
            _pricePerKilogram = pricePerKilogram;
        }

        /// <summary>
        /// Переопределяет метод ToString() для ящика овощей.
        /// </summary>
        /// <returns>Переопределённый метод ToString() для ящика овощей.</returns>
        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return $"  Ящик овощей: \n    Вес ящика овощей - {_weight}, \n    Стоимость за килограмм ящика овощей - {_pricePerKilogram}";
            Console.ResetColor();
        }
    }
}
