using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Treap
    {
        public int? x;
        public int? y;

        public Treap Left;
        public Treap Right;
        public Treap Parent;


        private Treap(int? x, int? y, Treap left = null, Treap right = null, Treap parent = null)
        {

            this.x = x;
            this.y = y;
            this.Left = left;
            this.Right = right;
            this.Parent = parent;
        }

        public Treap() { }
        public static Treap Treapify(int[] x, int[] y)
        {
            Treap Root = new Treap();
            for (int i = 0; i < x.Length; i++)
            {
                Root = Root.Add(x[i], y[i]);
            }
            return Root;
        }
        public Treap Grow(int x, int y)
        {
            Treap left, right;
            this.Split(x, out left, out right);
            Treap buf = Merge(left, new Treap(x, y));
            Treap result = Merge(buf, right);

            return result;
        }
        public static Treap Merge(Treap left, Treap right)
        {
            if (left == null) return right;
            if (right == null) return left;

            if (left.y > right.y)
            {
                return new Treap(left.x, left.y, left.Left, Merge(left.Right, right));
            }
            else
            {

                return new Treap(right.x, right.y, Merge(left, right.Left), right.Right);
            }
        }
        public void Split(int key, out Treap left, out Treap right)
        {
            Treap NewTree = null;

            if (this.x <= key)
            {
                if (this.Right == null)
                    right = null;
                else
                    this.Right.Split(key, out NewTree, out right);
                if (x != null && y != null)
                    left = new Treap(this.x, this.y, this.Left, NewTree);
                else left = null;
            }
            else
            {
                if (this.Left == null)
                    left = null;
                else
                    this.Left.Split(key, out left, out NewTree);
                if (x != null && y != null)
                    right = new Treap(this.x, this.y, NewTree, this.Right);
                else right = null;
            }
        }

        public Treap Add(int x, int y)
        {
            if (this.x == null && this.y == null)
            {
                this.x = x;
                this.y = y;
                return this;
            }
            Treap l, r;
            Split(x, out l, out r);
            Treap m = new Treap(x, y);
            return Merge(Merge(l, m), r);
        }
        public Treap Remove(int x)
        {
            Treap l, m, r;
            Split(x - 1, out l, out r);
            r.Split(x, out m, out r);
            return Merge(l, r);
        }
    }
}
