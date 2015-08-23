using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Pyramid
    {

        public List<int> Ellements { get; set; } = new List<int>();
        public int Size
        {
            get
            {
                return Ellements.Count;
            }
        }
        public Pyramid()
        {
        }
        public Pyramid(List<int> list)
        {
            Ellements = list;
            BuildHeap();
            HeapSort();
        }
        public void Heapify(int i)
        {
            int leftChild;
            int rightChild;
            int largestChild;

            for (; ;)
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                largestChild = i;

                if (leftChild < Size && Ellements[leftChild] > Ellements[largestChild])
                {
                    largestChild = leftChild;
                }

                if (rightChild < Size && Ellements[rightChild] > Ellements[largestChild])
                {
                    largestChild = rightChild;
                }

                if (largestChild == i)
                {
                    break;
                }

                int temp = Ellements[i];
                Ellements[i] = Ellements[largestChild];
                Ellements[largestChild] = temp;
                i = largestChild;
            }
        }
        public void BuildHeap() 
        {
            for (int i = Size / 2; i >= 0; i--)
            {
                Heapify(i); 
            }
        }
        public void HeapSort()
        {
            //BuildHeap();
            List<int> list = Ellements.GetRange(0,Size);     
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = GetMax();
                Heapify(0);
            }
            Ellements=list;
        }
        public int GetMax()
        {
            int result = Ellements[0];
            Ellements[0] = Ellements[Size - 1];
            Ellements.RemoveAt(Size - 1);
            return result;
        }
        public void Add(int value)
        {
            Ellements.Add(value);
            int i = Size - 1;
            int parent = (i - 1) / 2;

            while (i > 0 && Ellements[parent] < Ellements[i])
            {
                int temp = Ellements[i];
                Ellements[i] = Ellements[parent];
                Ellements[parent] = temp;

                i = parent;
                parent = (i - 1) / 2;
            }
            HeapSort();
        }
        public void HeapIncreaseKey(int index, int changing) 
        {
            Ellements[index] = changing;
            int nod = (index - 1) / 2; 
            while ((Ellements[index] > Ellements[nod]) && (index > 0)) 
            {
                int c = Ellements[index];
                Ellements[index] = Ellements[nod];
                Ellements[nod] = c;
                index = nod;
                nod = (index - 1) / 2;
            }
            Heapify(index);
        }
        public void SplitTree(List<int> tree1, List<int> tree2, int node)
        {                        
            List<int> indexes = new List<int>();
            GetSubTree(tree1,indexes, node);
            for (int i = 0; i < Size; i++)
            {
                if (!indexes.Contains(i))
                {
                    tree2.Add(Ellements[i]);
                }
            }

        }
        public void GetSubTree(List<int> tree1, List<int> indexes,int node)
        {
            tree1.Add(Ellements[node]);
            indexes.Add(node);
           int leftChild = 2 * node + 1;
           int rightChild = 2 * node + 2;
            if (leftChild <= Size - 1)
            {
                GetSubTree(tree1, indexes, leftChild);
            }
            if (rightChild <= Size - 1)
            {
                GetSubTree(tree1, indexes, rightChild);
            }
            //  return answer;
        }

    }
}
