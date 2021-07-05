using System;

namespace TwoLinkedList
{

    public class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }

    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList
    {
        int GetCount(); // возвращает количество элементов в списке
        void AddNode(int value);  // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        Node FindNode(int searchValue); // ищет элемент по его значению
    }

    class TLL : ILinkedList
    {
        Node head; // головной/первый элемент
        Node tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        /// <summary>
        /// метод добавляет элемент value в конеч списка
        /// </summary>
        /// <param name="value"></param>
        public void AddNode(int value)
        {
            Node tll = new Node(value);
            if (head == null)
                head = tll;
            else
            {
                tail.NextNode = tll;
                tll.PrevNode = tail;
            }
            tail = tll;
            count++;
        }

        /// <summary>
        /// метод добавляет элемент value после элемента node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public void AddNodeAfter(Node node, int value)
        {
            Node tll = new Node(value);
            tll.PrevNode = node;
            tll.NextNode = node.NextNode;
            if (node.NextNode != null)
                node.NextNode.PrevNode = tll;
            node.NextNode = tll;
            count++;
        }

        /// <summary>
        /// метод поиска элемента списка на позиции searchPos
        /// </summary>
        /// <param name="searchPos"></param>
        /// <returns></returns>
        public Node ItemPos(int searchPos)
        {
            if (searchPos < 0 | searchPos > count)
            {
                Console.WriteLine($"Аргумент {searchPos} находится за пределами диапазона 0 - {count}");
                return null;
            }

            Node current = head;

            int i = 0;
            // поиск узла
            while (current.NextNode != null)
            {
                if (i.Equals(searchPos))
                    break;
                current = current.NextNode;
                i++;
            }
            return current;
            throw new NotImplementedException();
        }
        /// <summary>
        /// метод ищет элемент списка по значению
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Node FindNode(int searchValue)
        {
            Node current = head;

            // поиск узла
            while (current != null)
            {
                if (current.Value.Equals(searchValue))
                    break;
                current = current.NextNode;
            }
            return current;
        }

        /// <summary>
        /// метод возвращает общее количество элементов в списке
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return count;
        }

        /// <summary>
        /// удаление элемента с индексом index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveNode(int index)
        {
            Node current = ItemPos(index);
            if (current != null)
            {
                // если узел не последний
                if (current.NextNode != null)
                {
                    current.NextNode.PrevNode = current.PrevNode;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.PrevNode;
                }

                // если узел не первый
                if (current.PrevNode != null)
                {
                    current.PrevNode.NextNode = current.NextNode;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.NextNode;
                }
                count--;
                return;
            }
            throw new NotImplementedException();

        }

        /// <summary>
        /// удаление элемента node
        /// </summary>
        /// <param name="node"></param>
        public void RemoveNode(Node node)
        {
            Node current = node;
            if (current != null)
            {
                // если узел не последний
                if (current.NextNode != null)
                {
                    current.NextNode.PrevNode = current.PrevNode;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.PrevNode;
                }

                // если узел не первый
                if (current.PrevNode != null)
                {
                    current.PrevNode.NextNode = current.NextNode;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.NextNode;
                }
                count--;
                return;
            }
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            TLL linkedList = new TLL();
            // добавление элементов
            linkedList.AddNode(1);
            linkedList.AddNode(3);
            linkedList.AddNode(4);
            linkedList.AddNode(2);
            linkedList.AddNode(6);
            linkedList.AddNode(5);
            // контрольный вывод
            int cnt = linkedList.GetCount();
            Console.WriteLine($"Количество элементов списка = {cnt}" );
            for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine($"Элемент списка № {i+1} = {linkedList.ItemPos(i).Value}");
            }

            // добавление элемента item в конец списка
            int item = 7;
            Console.WriteLine($"Добавим элемент <{item}>");
            linkedList.AddNode(item);
            // контрольный вывод
            cnt = linkedList.GetCount();
            Console.WriteLine($"Количество элементов списка = {cnt}");
            for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine($"Элемент списка № {i + 1} = {linkedList.ItemPos(i).Value}");
            }

            // удаление элемента, значение которого указывается в item
            item = 8;
            Console.WriteLine($"Удалим элемент со значением <{item}>");
            Node removeNode = linkedList.FindNode(item);
            if (removeNode != null)
            {
                Console.WriteLine($"Элемент со значением <{item}> <{linkedList.Item(item).Value}>");
                linkedList.RemoveNode(removeNode);
                // контрольный вывод
                cnt = linkedList.GetCount();
                Console.WriteLine($"Количество элементов списка = {cnt}");
                for (int i = 0; i < cnt; i++)
                {
                    Console.WriteLine($"Элемент списка № {i + 1} = {linkedList.ItemPos(i).Value}");
                }
            }
            else
                Console.WriteLine($"Элемент со значением <{item}> в списке не найден");

            // удаление элемента с индексом index 
            int index = 1;
            Console.WriteLine($"Удалим элемент с индексом <{index}>");
            Console.WriteLine($"Элемент с индексом <{index}> <{linkedList.ItemPos(index-1).Value}>");
            linkedList.RemoveNode(linkedList.ItemPos(index-1));
            // контрольный вывод
            cnt = linkedList.GetCount();
            Console.WriteLine($"Количество элементов списка = {cnt}");
            for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine($"Элемент списка № {i + 1} = {linkedList.ItemPos(i).Value}");
            }

            // добавление элемента item после элемента списка с индексом index
            index = linkedList.GetCount();
            item = 9;
            Console.WriteLine($"Добавим элемент <{item}> после элемента с индексом <{index}>");
            Node afterNode = linkedList.ItemPos(index - 1);
            if (afterNode != null)
            {
                Console.WriteLine($"Элемент с индексом <{index}> <{afterNode.Value}>");
                linkedList.AddNodeAfter(linkedList.ItemPos(index - 1), item);
                // контрольный вывод
                cnt = linkedList.GetCount();
                Console.WriteLine($"Количество элементов списка = {cnt}");
                for (int i = 0; i < cnt; i++)
                {
                    Console.WriteLine($"Элемент списка № {i + 1} = {linkedList.ItemPos(i).Value}");
                }
            }
        }
    }
}
