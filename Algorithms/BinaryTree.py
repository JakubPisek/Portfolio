class Node:
    def __init__(self, key):
        self.key = key
        self.left = None
        self.right = None
        self.parent = None

    def __repr__(self):
        if self.left is None and self.right is None:
            return f'\nKey: {self.key}, Left: [], Right: []'
        elif self.left is None:
            return f'\nKey: {self.key}, Left: [], Right: [{self.right}]'
        elif self.right is None:
            return f'\nKey: {self.key}, Left: [{self.left}], Right: []'
        else:
            return f'\nKey: {self.key}, Left: [{self.left}], Right: [{self.right}]'

class BinaryTree:
    def __init__(self):
        self.root = None

    def inorder(self, root):
        '''O(n)'''
        if root is not None:
            self.inorder(root.left)
            print(root.key)
            self.inorder(root.right)

    def search(self, key):
        '''O(h)'''
        temp = self.root

        while True:
            if temp is None or temp.key == key:
                return temp
            elif key < temp.key:
                temp = temp.left
            else:
                temp = temp.right

    def insert(self, key):
        '''O(h)'''
        insert_pos = None
        temp = self.root

        while temp is not None:
            insert_pos = temp

            if key < temp.key:
                temp = temp.left
            else:
                temp = temp.right
        
        new_node = Node(key)
        new_node.parent = insert_pos

        if insert_pos is None:
            self.root = new_node
        elif key < insert_pos.key:
            # uzel je levým potomkem
            insert_pos.left = new_node
        else:
            # uzel je pravým potomkem
            insert_pos.right = new_node

    def minimum_key(self, root=None):
        '''O(h)'''
        if root is None:
            min = self.root
        else:
            min = root

        while min.left is not None:
            min = min.left

        return min

    def maximum_key(self, root=None):
        '''O(h)'''
        if root is None:
            max = self.root
        else:
            max = root

        while max.right is not None:
            max = max.right

        return max

    def successor(self, key):
        '''O(h)'''
        temp = self.search(key)
        # směrem dolů ve stromě
        if temp.right is not None:
            return self.minimum_key(temp.right)

        # směrem nahoru ve stromě
        parent = temp.parent
        while parent is not None and temp is parent.right:
            temp = parent
            parent = parent.right
        
        return parent

    def transplant(self, node_to_delete, child):
        '''O(1)'''
        if node_to_delete.parent is None:
            self.root = child
        elif node_to_delete is node_to_delete.parent.left:	
            # uzel je levým potomkem
            node_to_delete.parent.left = child
        else:
            # uzel je pravým potomkem
            node_to_delete.parent.right = child

        if child is not None:
            # předělání rodič pointeru
            child.parent = node_to_delete.parent

    def delete(self, key):
        '''O(1) + O(h) transplant'''
        node_to_delete = self.search(key)

        if node_to_delete.left is None:
            # uzel má pravého potomka (nebo žádného)
            return self.transplant(node_to_delete, node_to_delete.right)
        elif node_to_delete.right is None:
            # uzel má levého potomka (nebo žádného)
            return self.transplant(node_to_delete, node_to_delete.left)
        else:
            # následovník
            temp = self.minimum_key(node_to_delete.right)

            # následovník není přímým potomkem
            if temp.parent is not node_to_delete:
                self.transplant(temp, temp.right)
                temp.right = node_to_delete.right
                temp.right.parent = temp
            
            # následovník je přímým potomkem
            self.transplant(node_to_delete, temp)
            temp.left = node_to_delete.left
            temp.left.parent = temp

'''
      6
   /    \
  3      9
 / \    / \
1   5  7   11
'''

binary_tree = BinaryTree()
#root
binary_tree.insert(6)
#left
binary_tree.insert(3)
binary_tree.insert(1)
binary_tree.insert(5)
#right
binary_tree.insert(9)
binary_tree.insert(7)
binary_tree.insert(11)


print(binary_tree.root)
print(binary_tree.search(9).parent)
binary_tree.inorder(binary_tree.root)

print(binary_tree.minimum_key())
print(binary_tree.maximum_key())

print(binary_tree.search(3))
print(binary_tree.search(20))

binary_tree.delete(3)
print(binary_tree.root)
