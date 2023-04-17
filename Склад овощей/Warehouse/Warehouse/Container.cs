using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class Container
    {
        public readonly uint MaxSumWeight;
        public readonly string Id;
        public double DegreeOfDamage = 0.0;
        private List<VegetableBox> _vegetableBoxes = new List<VegetableBox>();

        /// <summary>
        /// Конструктор контейнера.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        public Container(string id)
        {
            Id = id;
            var rand = new Random();
            MaxSumWeight = (uint)rand.Next(50, 1001);
        }


        /// <summary>
        /// Свойство для получения текущего веса контейнера.
        /// </summary>
        public uint CurrentWeigh
        {
            get
            {
                uint currentWeigh = 0;
                for (int i = 0; i < _vegetableBoxes.Count; i++)
                {
                    currentWeigh += _vegetableBoxes[i].Weight;
                }
                return currentWeigh;
            }
        }


        /// <summary>
        /// Свойство для получения итоговой стоимости контейнера.
        /// </summary>
        public decimal TotalCost
        {
            get
            {
                decimal totalCost = 0.0m;

                for (int i = 0; i < _vegetableBoxes.Count; i++)
                {
                    totalCost += _vegetableBoxes[i].Weight * _vegetableBoxes[i].PricePerKilogram;
                }

                return totalCost * (1.0m - (decimal)DegreeOfDamage);
            }
        }


        /// <summary>
        /// Пробует добавить ящик с овощами в контейнер.
        /// </summary>
        /// <param name="box">Ящик с овощами.</param>
        public void AddBox(VegetableBox box)
        {
            if (CurrentWeigh + box.Weight <= MaxSumWeight)
            {
                _vegetableBoxes.Add(box);
                Console.WriteLine("Ящик добавлен.");
                return;
            }
            Console.WriteLine("Превышен максимальный вес контейнера. Ящик не был добавлен.");

        }

        /// <summary>
        /// Переопределяет метод ToString() для контейнера.
        /// </summary>
        /// <returns>Переопределенный метод ToString() для контейнера.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($" Контейнер: \n   Идентификатор контейнера - {Id}. \n   Максимальный вес контейнера - {MaxSumWeight}. \n   Степень повреждения контейнера - {Math.Round(DegreeOfDamage,2)}.\n");
            foreach(var box in _vegetableBoxes)
            {
                sb.Append($"{box.ToString()}\n");
            }
            return sb.ToString();
        }
    }
}
