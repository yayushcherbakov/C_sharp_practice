using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Warehouse
{
    class Warehouse
    {
        private static Warehouse instance;

        /// <summary>
        /// Получает сущность-одиночку.
        /// </summary>
        /// <param name="maxContainerCount">Максимальное число контейнеров.</param>
        /// <param name="containerStorageFee">Стоимость содержания контейнера.</param>
        /// <returns></returns>
        public static Warehouse GetInstance(uint maxContainerCount, decimal containerStorageFee)
        {
            if (instance == null)
            {
                instance = new Warehouse(maxContainerCount, containerStorageFee);
            }

            return instance;
        }

        private List<Container> _containers = new List<Container>();
        private readonly uint _maxContainerCount;
        private readonly decimal _containerStorageFee;
        private decimal _earnings = 0.0m;


        /// <summary>
        /// Конструктор для склада.
        /// </summary>
        /// <param name="maxContainerCount">Максимальное число контейнеров.</param>
        /// <param name="containerStorageFee">Стоимость содержания контейнера.</param>
        private Warehouse(uint maxContainerCount, decimal containerStorageFee)
        {
            _maxContainerCount = maxContainerCount;
            _containerStorageFee = containerStorageFee;
        }

        /// <summary>
        /// Проверяет контейнер на совпадение по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        /// <returns></returns>
        public bool IsContainerExistsById(string id)
        {
            for (int i = 0; i < _containers.Count; i++)
            {
                if (_containers[i].Id == id)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Оценивает степень повреждения контейнера.
        /// </summary>
        /// <param name="container">Контейнер.</param>
        private void AssessContainerDamage(Container container)
        {
            var rand = new Random();
            container.DegreeOfDamage = rand.NextDouble() / 2;
        }

        /// <summary>
        /// Свойство для получения максимального числа контейнеров на складе.
        /// </summary>
        public uint MaxContainerCount
        {
            get
            {
                return _maxContainerCount;
            }
        }

        /// <summary>
        /// Попытка добавить контейнер на склад.
        /// </summary>
        /// <param name="container">Контейнер.</param>
        public void AddContainer(Container container)
        {
            AssessContainerDamage(container);
            if (container.TotalCost <= _containerStorageFee)
            {
                Console.WriteLine("Хранение контейнера нерентабельно.");
                Console.WriteLine($"Cуммарная стоимость содержимого контейнера({decimal.Round(container.TotalCost,2)}) не превосходит стоимость хранения({_containerStorageFee.ToString(CultureInfo.InvariantCulture)}).");
                Console.WriteLine("Контейнер не был добавлен.");
                return;
            }

            _earnings += _containerStorageFee;

            if (_containers.Count < _maxContainerCount)
            {
                _containers.Add(container);
                Console.WriteLine("Контейнер добавлен на склад.");
                return;
            }

            Console.WriteLine("Склад был переполнен.");
            Console.WriteLine("Был удален контейнер, который был добавлен не позднее всех остальных:\n");
            Console.WriteLine(_containers[0]);
            Console.WriteLine("На его место помещен добавляемый контейнер");
            _containers.RemoveAt(0);
            _containers.Add(container);

        }

        /// <summary>
        /// Удаление контейнера по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        public void RemoveContainerById(string id)
        {
            int index = -1;

            for (int i = 0; i < _containers.Count; i++)
            {
                if (_containers[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("Контейнер с данным идентификатором не найден на складе.");
                return;
            }

            _containers.RemoveAt(index);
            Console.WriteLine($"Контейнер с данным идентификатором \"{id}\" удален со слада.");
        }

        /// <summary>
        /// Переопределяет метод ToString() для склада.
        /// </summary>
        /// <returns>Переопределённый метод ToString() для склада.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append($"Склад: \n  Вместительность склада - {_maxContainerCount}. \n  Стоимость содержания одного контейнера на складе - {_containerStorageFee}. \n  Текущая ценность склада - {_earnings}.\n");
            
            foreach (var container in _containers)
            {
                sb.Append($"{container.ToString()}\n");
            }
            return sb.ToString();
        }
    }
}
