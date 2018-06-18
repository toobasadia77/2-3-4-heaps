# 2-3-4-heaps
In 2-3-4 tree every internal node (other than possibly
the root) has two, three, or four children and all leaves have the same depth. In
this problem, we shall implement 2-3-4 heaps, which support the mergeable-heap
operations.
The 2-3-4 heaps differ from 2-3-4 trees in the following ways. In 2-3-4 heaps,
only leaves store keys, and each leaf x stores exactly one key in the attribute x:key.
The keys in the leaves may appear in any order. Each internal node x contains
a value x:small that is equal to the smallest key stored in any leaf in the subtree
rooted at x. The root r contains an attribute r:height that gives the height of the
tree. Finally, 2-3-4 heaps are designed to be kept in main memory, so that disk
reads and writes are not needed.
a. MINIMUM, which returns a pointer to the leaf with the smallest key: Traverse a path from root to leaf as follows: At a given node, examine the attribute x.smallx.small in each child-node of the current node. Proceed to the child node which minimizes this attribute. If the children of the current node are leaves, then simply return a pointer to the child node with smallest key. Since the height of the tree is O(lgn) and the number of children of any node is at most 44, this has runtime O(lgn).
b. DECREASE-KEY, which decreases the key of a given leaf x to a given value: Decrease the key of x, then traverse the simple path from x to the root by following the parent pointers. At each node yy encountered, check the attribute y.smally.small. If  k<x.small, set x.small=k. Otherwise do nothing and continue on the path.

c. INSERT, which inserts leaf x with key k:Insert works the same as in a B-tree, except that at each node it is assumed that the node to be inserted is 'smaller' than every key stored at that node, so the runtime is inherited. If the root is split, we update the height of the tree. When we reach the final node before the leaves, simply insert the new node as the leftmost child of that node.

d. DELETE, which deletes a given leaf x: In 2-3-4 heap, we'll want to ensure that the tree satisfies the properties of being a 2-3-4 tree after deletion, so we'll need to check that we're never deleting a leaf which only has a single sibling.

e. EXTRACT-MIN, which extracts the leaf with the smallest key: EXTRACT-MIN simply locates the minimum as done in part (a), then deletes it as in part (d).

f. UNION, which unites two 2-3-4 heaps, returning a single 2-3-4 heap and destroying
the input heaps: This can be done by implementing the join operation.

GROUP MEMBERS:
Sadia Shakeel (15B-015-SE)
Tooba Ashraf (15B-025-SE)

