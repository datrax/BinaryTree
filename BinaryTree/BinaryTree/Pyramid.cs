using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Pyramid
    {

        public List<int> Ellements { get; set; } = new List<int>();
        public int size
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
            heapSort();
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

                if (leftChild < size && Ellements[leftChild] > Ellements[largestChild])
                {
                    largestChild = leftChild;
                }

                if (rightChild < size && Ellements[rightChild] > Ellements[largestChild])
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
            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(i); 
            }
        }
        public void heapSort()
        {
            //BuildHeap();
            List<int> list = Ellements.GetRange(0,size);     
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
            Ellements[0] = Ellements[size - 1];
            Ellements.RemoveAt(size - 1);
            return result;
        }
        public void add(int value)
        {
            Ellements.Add(value);
            int i = size - 1;
            int parent = (i - 1) / 2;

            while (i > 0 && Ellements[parent] < Ellements[i])
            {
                int temp = Ellements[i];
                Ellements[i] = Ellements[parent];
                Ellements[parent] = temp;

                i = parent;
                parent = (i - 1) / 2;
            }
            heapSort();
        }
        public void Heap_Increase_Key(int index, int changing) 
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

    }
}
