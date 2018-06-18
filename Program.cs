using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class node
    {
        public int key;
        public int degree;
        public node parent;
        public node child1;
        public node child2;
        public node child3;
        public node child4;
        public node(int k = 0, int d = 0, node par = null, node c1 = null, node c2 = null, node c3 = null, node c4 = null)
        {
            this.key = k;
            this.degree = d;
            this.parent = par;
            this.child1 = c1;
            this.child2 = c2;
            this.child3 = c3;
            this.child4 = c4;
        }
    }


    public class node1
    {
        public int height;
        public node root;
        public node1(int h = -1, node roott = null)
        {
            this.height = h;
            this.root = roott;
        }
        public static node1 makeHeap()
        {
            node1 head = new node1();
            return head;
        }
    }
    class Program
    {
        static void swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        public static void remove(node1 head, ref node x) //deletes a given leaf x
        {
            decreaseKey(head, x, -1);
            extractMin(head);
        }
        public static node1 union234(node1 heap1, node1 heap2)
        {
            if (heap1.height == heap2.height)
            {
                node root = new node(Math.Min(heap1.root.key, heap2.root.key), 2, null, heap1.root, heap2.root);
                node1 head = new node1(heap1.height + 1, root);
                return head;
            }
            if (heap2.height > heap1.height)
            {
                swap(ref heap1, ref heap2);
            }
            node p = heap1.root;
            if (p.degree == 4)
            {
                node l = new node(Math.Min(p.child1.key, p.child2.key), 2, null, p.child1, p.child2);
                node r = new node(Math.Min(p.child3.key, p.child4.key), 2, null, p.child3, p.child4);
                node pr = new node(Math.Min(l.key, r.key), 2, null, l, r);
                l.parent = pr;
                r.parent = pr;
                heap1.root = pr;
                heap1.height++;
            }
            p = heap1.root;
            for (int i = 0; i < heap1.height - heap2.height - 1; ++i)
            {
                if (p.degree < 4)
                {
                    if (p.child3 != null && p.child3.degree < 4)
                    {
                        p = p.child3;
                    }
                    else if (p.child2 != null && p.child2.degree < 4)
                    {
                        p = p.child2;
                    }
                    else
                    {
                        p = p.child1;
                    }
                }
                else
                {
                    if (p.parent.child3 == null)
                    {
                        p.parent.child3 = new node(Math.Min(p.child3.key, p.child4.key), 2, p.child3, p.child4);
                        --i;
                    }
                    else
                    {
                        p.parent.child4 = new node(Math.Min(p.child3.key, p.child4.key), 2, p.child3, p.child4);
                        --i;
                    }
                    p.parent.degree++;
                    p.degree -= 2;
                }
            }
            if (p.degree < 4)
            {
                if (p.child3 == null)
                {
                    p.child3 = heap2.root;
                    p.child3.parent = p;
                    p.child3.degree = heap2.root.degree;
                    p.child3.child1 = heap2.root.child1;
                    p.child3.child2 = heap2.root.child2;
                    p.child3.child3 = heap2.root.child3;
                    p.child3.child4 = heap2.root.child4;
                    p.degree++;
                }
                else
                {
                    p.child4 = heap2.root;
                    p.child4.parent = p;
                    p.child4.degree = heap2.root.degree;
                    p.child4.child1 = heap2.root.child1;
                    p.child4.child2 = heap2.root.child2;
                    p.child4.child3 = heap2.root.child3;
                    p.child4.child4 = heap2.root.child4;
                    p.degree++;
                }
            }
            else
            {
                if (p.parent.child3 == null)
                {
                    p.parent.child3 = new node(Math.Min(p.child3.key, p.child4.key), 2, p.child3, p.child4);
                }
                else
                {
                    p.parent.child4 = new node(Math.Min(p.child3.key, p.child4.key), 2, p.child3, p.child4);
                }
                p.parent.degree++;
                p.degree -= 2;
                p.parent.child3.child3 = heap2.root;
                p.parent.child3.child3.degree++;
            }
            p = heap2.root;
            while (p.parent != null)
            {
                if (p.parent.key > p.key)
                {
                    p.parent.key = p.key;
                }
                else
                {
                    break;
                }
            }
            return heap1;
        }
        public static void insert(ref node1 h1, node x)
        {
            if (h1.height == -1)
            {
                h1.root = x;
                h1.height = 0;
            }
            else
            {
                node1 h2 = new node1(0, x);
                h1 = union234(h1, h2);
            }
        }
        public static void display(node root)
        {
            if (root != null)
            {
                Console.Write(root.key);
                Console.Write(' ');
                if (root.child1 != null)
                {
                    Console.Write(" c1 ");
                    display(root.child1);
                }
                if (root.child2 != null)
                {
                    Console.Write(" c2 ");
                    display(root.child2);
                }
                if (root.child3 != null)
                {
                    Console.Write(" c3 ");
                    display(root.child3);
                }
                if (root.child4 != null)
                {
                    Console.Write(" c4 ");
                    display(root.child4);
                }
                Console.Write("\n");
            }
        }
        public static node minimum(node1 head)
        {
            node m = head.root;
            for (int i = 0; i < head.height; ++i)
            {
                if (m.child1.key == m.key)
                {
                    m = m.child1;
                }
                else if (m.child2.key == m.key)
                {
                    m = m.child2;
                }
                else if (m.child3.key == m.key)
                {
                    m = m.child3;
                }
                else if (m.child4.key == m.key)
                {
                    m = m.child4;
                }
            }
            return m;
        }


        public static void decreaseKey(node1 head, node x, int k)
        {
            if (k > x.key)
            {
                Console.Write("error");
            }
            else
            {
                x.key = k;
                node p = x;
                while (p.parent != null && p.parent.key > k)
                {
                    p.parent.key = k;
                    p = p.parent;
                }
            }
        }


        public static node extractMin(node1 head) //extracts a leaf with the smallest key; returns the node with the smallest key
        {
            node mini = minimum(head);
            if (mini == head.root || mini.parent == null)
            {
                head.root = null;
                head.height--;
                return null;
            }
            if (mini.parent == head.root && head.root.degree == 2)
            {
                head.root = new node(Math.Max(head.root.child1.key, head.root.child2.key), 0);
                head.height--;
                return head.root;
            } //simplest cases
            node p = mini;
            node q = p;
            if (mini.parent.degree > 2) //not so complicated case: we can delete it and then aligne
            {
                if (mini == mini.parent.child1)
                {
                    swap(ref mini.parent.child1, ref mini.parent.child2);
                }
                if (mini == mini.parent.child2)
                {
                    swap(ref mini.parent.child2, ref mini.parent.child3);
                }
                if (mini == mini.parent.child3 && mini.parent.child4 != null)
                {
                    swap(ref mini.parent.child3, ref mini.parent.child4);
                }
                mini.parent.degree--;
                if (mini.parent.child4 != null)
                {
                    mini.parent.child4 = null;
                }
                else
                {
                    mini.parent.child3 = null;
                }
            }
            else //its parent has only 2 childs
            {
                p = mini.parent;
                mini = null;
                if (p.child1 == null)
                {
                    swap(ref p.child1, ref p.child2);
                }
                p.key = p.child1.key;
                p.degree--;
                while (true) //we can get til the root
                {
                    if (p.parent == null) //it got to the root
                    {
                        q = head.root = p.child1;
                        head.height--;
                        break;
                    }
                    else
                    {
                        if (p.parent.degree > 2) //we can fix it with 1 step
                        {
                            if (p.parent.child1.degree != 1) // we can get him a sibling form here
                            {
                                if (p.parent.child1.degree == 2) // we move the single leaf
                                {
                                    p.parent.child1.child3 = p.child1;
                                    p.parent.child1.degree++;
                                    p.parent.degree--;
                                    p.parent.child1.key = Math.Min(p.parent.child1.key, p.parent.child1.child3.key);
                                    if (p == p.parent.child1) // we gotta align
                                    {
                                        swap(ref p.parent.child1, ref p.parent.child2);
                                    }
                                    if (p == p.parent.child2)
                                    {
                                        swap(ref p.parent.child2, ref p.parent.child3);
                                    }
                                    if (p == p.parent.child3 && p.parent.child4 != null)
                                    {
                                        swap(ref p.parent.child3, ref p.parent.child4);
                                    }
                                    if (p.parent.child4 != null)
                                    {
                                        p.parent.child4 = null;
                                    }
                                    else
                                    {
                                        p.parent.child3 = null;
                                    }
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key); //refreshing the keys
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child3.key, p.parent.key);
                                    }
                                }
                                else // we get a sibling for him from his uncle
                                {
                                    if (p.parent.child1.child4 != null)
                                    {
                                        p.child2 = p.parent.child1.child4;
                                        p.parent.child1.child4 = null;
                                        p.parent.child1.degree--;
                                        p.degree++;
                                    }
                                    else
                                    {
                                        p.child2 = p.parent.child1.child3;
                                        p.parent.child1.child3 = null;
                                        p.parent.child1.degree--;
                                        p.degree++;
                                    }
                                    p.parent.child1.key = Math.Min(p.parent.child1.child1.key, p.parent.child1.child2.key); //key refreshing
                                    if (p.parent.child1.child3 != null)
                                    {
                                        p.parent.child1.key = Math.Min(p.parent.child1.key, p.parent.child1.child3.key);
                                    }
                                    if (p.parent.child1.child4 != null)
                                    {
                                        p.parent.child1.key = Math.Min(p.parent.child1.key, p.parent.child1.child4.key);
                                    }
                                    p.key = Math.Min(p.child1.key, p.child2.key);
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key);
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child3.key, p.parent.key);
                                    }
                                    if (p.parent.child4 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child4.key, p.parent.key);
                                    }
                                }
                            }
                            else if (p.parent.child2.degree != 1) // we can get him a sibling form here
                            {
                                if (p.parent.child2.degree == 2) // we move teh single leaf
                                {
                                    p.parent.child2.child3 = p.child1;
                                    p.parent.child2.degree++;
                                    p.parent.degree--;
                                    p.parent.child2.key = Math.Min(p.parent.child2.key, p.parent.child2.child3.key);
                                    if (p == p.parent.child1) //aligning
                                    {
                                        swap(ref p.parent.child2, ref p.parent.child2);
                                    }
                                    if (p == p.parent.child2)
                                    {
                                        swap(ref p.parent.child2, ref p.parent.child3);
                                    }
                                    if (p == p.parent.child3 && p.parent.child4 != null)
                                    {
                                        swap(ref p.parent.child3, ref p.parent.child4);
                                    }
                                    if (p.parent.child4 != null)
                                    {
                                        p.parent.child4 = null;
                                    }
                                    else
                                    {
                                        p.parent.child3 = null;
                                    }
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key); //refreshing the keys
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child3.key, p.parent.key);
                                    }
                                }
                                else //we get him a siblin gfrom his uncle
                                {
                                    if (p.parent.child2.child4 != null)
                                    {
                                        p.child2 = p.parent.child2.child4;
                                        p.parent.child2.child4 = null;
                                        p.parent.child2.degree--;
                                        p.degree++;
                                    }
                                    else
                                    {
                                        p.child2 = p.parent.child2.child3;
                                        p.parent.child2.child3 = null;
                                        p.parent.child2.degree--;
                                        p.degree++;
                                    }
                                    p.parent.child2.key = Math.Min(p.parent.child2.child1.key, p.parent.child2.child2.key);
                                    if (p.parent.child1.child3 != null)
                                    {
                                        p.parent.child2.key = Math.Min(p.parent.child2.key, p.parent.child2.child3.key);
                                    }
                                    if (p.parent.child2.child4 != null)
                                    {
                                        p.parent.child2.key = Math.Min(p.parent.child2.key, p.parent.child2.child4.key);
                                    }
                                    p.key = Math.Min(p.child1.key, p.child2.key);
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key);
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child3.key, p.parent.key);
                                    }
                                    if (p.parent.child4 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.child4.key, p.parent.key); //keys resfreshed
                                    }
                                }
                            }
                            q = p;
                            break;
                        }
                        else
                        {
                            if (p.parent.child1.degree != 1)
                            {
                                if (p.parent.child1.degree > 2) //getting him a sibling from his uncle
                                {
                                    if (p.parent.child1.child4 != null)
                                    {
                                        p.child2 = p.parent.child1.child4;
                                        p.parent.child1.child4 = null;
                                    }
                                    else
                                    {
                                        p.child2 = p.parent.child1.child3;
                                        p.parent.child1.child3 = null;
                                    }
                                    p.parent.child1.degree--;
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key); //surely refreshing the keys
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.key, p.parent.child3.key);
                                    }
                                    p.degree++;
                                    q = p;
                                    break;
                                }
                                else // the megaworst case
                                {
                                    p.parent.child1.child3 = p.child1;
                                    p.parent.child1.degree++;
                                    p.parent.degree--;
                                    p.child1 = null;
                                    p = p.parent;
                                    p.child1.key = Math.Min(p.child1.key, p.child1.child3.key);
                                    p.key = p.child1.key;
                                    p.child2 = null;
                                }
                            }
                            else if (p.parent.child2.degree != 1)
                            {
                                if (p.parent.child2.degree > 2) //getting him a sibling from his uncle
                                {
                                    if (p.parent.child2.child4 != null)
                                    {
                                        p.child2 = p.parent.child2.child4;
                                        p.parent.child2.child4 = null;
                                    }
                                    else
                                    {
                                        p.child2 = p.parent.child2.child3;
                                        p.parent.child2.child3 = null;
                                    }
                                    p.parent.child2.degree--;
                                    p.parent.key = Math.Min(p.parent.child1.key, p.parent.child2.key); //surely refreshing the keys
                                    if (p.parent.child3 != null)
                                    {
                                        p.parent.key = Math.Min(p.parent.key, p.parent.child3.key);
                                    }
                                    p.degree++;
                                }
                                else // the meagworst case
                                {
                                    p.parent.child2.child3 = p.child1;
                                    p.parent.child2.degree++;
                                    swap(ref p.parent.child2, ref p);
                                    p.parent.degree--;
                                    p.child1 = null;
                                    p = p.parent;
                                    p.child2 = null;
                                }
                            }
                        }
                    }
                }
            }
            while (q.parent == q.parent) // refreshing the minimum of the whole heap
            {
                q = q.parent;
                q.key = Math.Min(q.child1.key, q.child2.key);
                if (q.child3 == q.child3)
                {
                    q.key = Math.Min(q.key, q.child3.key);
                }
                if (q.child4 == q.child4)
                {
                    q.key = Math.Min(q.key, q.child4.key);
                }
            }
            return minimum(head);
        }
        static void Main(string[] args)
        {
            node1 head = node1.makeHeap();
            node node_1 = new node(2);
            insert(ref head, node_1);
            for (int i = 0; i < 10; i++)
            {
                node node_2 = new node(i + 3);
                insert(ref head, node_2);
            }
            //remove(head,node_2);
            display(head.root);
            Console.Read();
        }
    }
}





