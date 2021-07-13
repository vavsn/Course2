using System;
using System.Collections.Generic;

namespace Tree
{

    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as TreeNode;

            if (node == null)
                return false;

            return node.Value == Value;
        }
    }
    
    public interface ITree
    {
        TreeNode GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
    }

    /// <summary>
    /// переработанный интерфейс
    /// </summary>
    public class Tree : ITree
    {
        public TreeNode _head { get; set; } // корень дерева
        public int _count; // количество веток
        /// <summary>
        /// метод возвращает корень дерева
        /// </summary>
        /// <returns></returns>
        public TreeNode GetRoot()
        {
            return _head;
        }

        /// <summary>
        /// метод добавления листа
        /// </summary>
        /// <param name="value"></param>
        public void AddItem(int value)
        {
            // если дерево пустое, просто создаем корневой узел.
            if (_head == null)
            {
                _head = new TreeNode();
                _head.Value = value;
            }
            // дерево не пустое => ищем правильное место для вставки.
            else
            {
                AddTo(_head, value);
            }

            _count++; // увеличим количество листьев / веток
        }

        /// <summary>
        /// метод добавления ребёнка
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void AddTo(TreeNode node, int value)
        {
            // вставляемое значение меньше значения узла
            if (value.CompareTo(node.Value) < 0)
            {
                // Если нет левого поддерева, добавляем значение в левого ребенка,
                if (node.LeftChild == null)
                {
                    node.LeftChild = new TreeNode();
                    node.LeftChild.Value = value;
                }
                else
                {
                    // в противном случае повторяем для левого поддерева
                    AddTo(node.LeftChild, value);
                }
            }
            // вставляемое значение больше или равно значению узла
            else
            {
                // если нет правого поддерева, добавляем значение в правого ребенка
                if (node.RightChild == null)
                {
                    node.RightChild = new TreeNode();
                    node.RightChild.Value = value;
                }
                else
                {
                    // в противном случае повторяем для правого поддерева
                    AddTo(node.RightChild, value);
                }
            }
        }

        public void RemoveItem(int value)
        {
            TreeNode current, parent;

            // находим удаляемый узел
            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return;
            }

            _count--; // уменьшаем количество листьев / веток

            // если нет детей справа, левый ребенок встает на место удаляемого
            if (current.RightChild == null)
            {
                if (parent == null)
                {
                    _head = current.LeftChild;
                }
                else
                {
                    bool result = parent.Equals(current.Value);
                    if (result)
                    {
                        // если значение родителя больше текущего, левый ребенок текущего узла становится левым ребенком родителя
                        parent.LeftChild = current.LeftChild;
                    }
                    else 
                    { // если значение родителя меньше текущего,  левый ребенок текущего узла становится правым ребенком родителя
                        parent.RightChild = current.LeftChild; 
                    } 
                } 
            } 
            // если у правого ребенка нет детей слева - он занимает место удаляемого узла
            else 
            if (current.RightChild.LeftChild == null)
            { current.RightChild.LeftChild = current.LeftChild; 
                if (parent == null) 
                { 
                    _head = current.RightChild; 
                } 
                else 
                { 
                    bool result = parent.Equals(current.Value); 
                    if (result)
                    {
                        // Если значение родителя больше текущего, правый ребенок текущего узла становится левым ребенком родителя
                        parent.LeftChild = current.RightChild;
                    }
                    else 
                    { // если значение родителя меньше текущего, правый ребенок текущего узла становится правым ребенком родителя
                        parent.RightChild = current.RightChild; 
                    } 
                } 
            } 
            // если у правого ребенка есть дети слева, крайний левый ребенок из правого поддерева заменяет удаляемый узел
            else 
            { // ищем крайний левый узел
                TreeNode leftmost = current.RightChild.LeftChild; 
                TreeNode leftmostParent = current.RightChild; 
                while (leftmost.LeftChild != null) 
                { 
                    leftmostParent = leftmost; 
                    leftmost = leftmost.LeftChild; 
                } // левое поддерево родителя становится правым поддеревом крайнего левого узла
                leftmostParent.LeftChild = leftmost.RightChild; 
                // левый и правый ребенок текущего узла становится левым и правым ребенком крайнего левого
                leftmost.LeftChild = current.LeftChild; 
                leftmost.RightChild = current.RightChild; 
                if (parent == null) 
                { 
                    _head = leftmost; 
                } 
                else 
                { 
                    bool result = parent.Equals(current.Value); 
                    if (result)
                    {
                        // если значение родителя больше текущего, крайний левый узел становится левым ребенком родителя
                        parent.LeftChild = leftmost;
                    }
                    else 
                    {
                        // если значение родителя меньше текущего, крайний левый узел становится правым ребенком родителя
                        parent.RightChild = leftmost;
                    }
                }
                
            }

            return;
        }
        
        /// <summary>
        /// метод находит и возвращает первый узел с заданным значением.
        /// Если значение не найдено, возвращает null.
        /// Кроме того, возвращает родителя найденного узла для использования в методе Remove.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private TreeNode FindWithParent(int value, out TreeNode parent)
        {
            // попробуем найти значение в дереве
            TreeNode current = _head;
            parent = null;

            // до тех пор, пока не нашли...
            while (current != null)
            {
                bool result = current.Equals(value);

                if (result)
                {
                    // если искомое значение меньше, идем налево
                    parent = current;
                    current = current.LeftChild;
                }
                else 
                {
                    // если искомое значение больше, идем направо
                    parent = current;
                    current = current.RightChild;
                }
            }

            return current;
        }
        /// <summary>
        /// метод ищет узел дерева по значению
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeNode GetNodeByValue(int value) 
        {
            // собственно поиск узла осуществляется другим методом
            TreeNode parent;
            return FindWithParent(value, out parent);
        }
        /// <summary>
        /// метод выводит на консоль дерево
        /// </summary>
        public void PrintTree() 
        {
            TreeShow.Show(_head);
        }

    }


    public class NodeInfo
    {
        public int Depth { get; set; }
        public TreeNode Node { get; set; }
    }


    public static class TreeHelper
    {
        public static NodeInfo[] GetTreeInLine(ITree tree)
        {
            var bufer = new Queue<NodeInfo>();
            var returnArray = new List<NodeInfo>();
            var root = new NodeInfo() { Node = tree.GetRoot() };
            bufer.Enqueue(root);

            while (bufer.Count != 0)
            {
                var element = bufer.Dequeue();
                returnArray.Add(element);

                var depth = element.Depth + 1;

                if (element.Node.LeftChild != null)
                {
                    var left = new NodeInfo()
                    {
                        Node = element.Node.LeftChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(left);
                }
                if (element.Node.RightChild != null)
                {
                    var right = new NodeInfo()
                    {
                        Node = element.Node.RightChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(right);
                }
            }

            return returnArray.ToArray();
        }
    }
    /// <summary>
    /// вспомогательный класс для вывода на консоль дерева
    /// </summary>
    public static class TreeShow
    {
        class NodeInfo
        {
            public TreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        /// <summary>
        /// метод форматирует и выводит текст на консоль
        /// </summary>
        /// <param name="root"></param>
        /// <param name="textFormat"></param>
        /// <param name="spacing"></param>
        /// <param name="topMargin"></param>
        /// <param name="leftMargin"></param>
        public static void Show(this TreeNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.Value.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Show(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Show("/", top + 1, item.Left.EndPos);
                        Show("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Show("_", top, item.EndPos, item.Right.StartPos - 1);
                        Show("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }
        /// <summary>
        /// метод выводит строку текста на консоль
        /// </summary>
        /// <param name="s"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void Show(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // формируем дерево
            Tree tree = new Tree();
            tree.AddItem(8);
            tree.AddItem(4);
            tree.AddItem(2);
            tree.AddItem(3);
            tree.AddItem(10);
            tree.AddItem(6);
            tree.AddItem(7);
            tree.AddItem(27);
            tree.AddItem(71);

            // выводим на экран содержимое дерева с указанием уровней
            Console.WriteLine("Диагностический вывод содержимого дерева. depth = уровень узла");
            foreach (var t in TreeHelper.GetTreeInLine(tree))
            {
                Console.WriteLine("Value {0,3:d} depth {1}", t.Node.Value, t.Depth);
            }

            Console.WriteLine();
            Console.WriteLine("Контрольный вывод информации в виде дерева");
            // выводим на экран информацию в виде собственно дерева
            tree.PrintTree();
        }
    }
}
