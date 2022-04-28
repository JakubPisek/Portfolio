class Node:
    def __init__(self, leaf=False):
        self.keys = []
        self.children = []
        self.leaf = leaf
    
    def __repr__(self):
        if self.leaf:
            return f'{self.keys}'
        else:
            return f'Keys: {self.keys} - Children: {self.children}'

    def n(self):
        return len(self.keys)

class BTree:
    def __init__(self, t):
        self.root = Node(True)
        self.t = t

    def search(self, key, node=None):
        '''O(t logt n)'''
        if node is not None:
            i = 0
            # zjistí pozici/rodiče klíče
            while i < node.n() and key > node.keys[i]:
                i += 1

            # klíč se našel
            if i < node.n() and key == node.keys[i]:
                return node, i
            # klíč se nenašel
            elif node.leaf:
                return None
            # klíč se může nacházet v potomkovi
            else:
                return self.search(key, node.children[i])
        else:
            return self.search(key, self.root)

    def insert_key(self, key):
        '''O(t logt n)'''
        root = self.root

        # jestliže je kořen zaplněný
        if root.n() == 2 * self.t - 1:
            temp = Node()
            self.root = temp
            temp.children.insert(0, root)
            # rozdělení potomka (původní kořen) na dvě části
            self.split_child(temp, 0)
            # potom je rozdělený, takže už přidáváme do neplného uzlu
            self.insert_key_nonfull(temp, key)
        else:
            self.insert_key_nonfull(root, key)

    def split_child(self, node, i):
        t = self.t
        y = node.children[i]
        z = Node(y.leaf)

        # vložení prostředního klíče do nového kořenu
        node.keys.insert(i, y.keys[t - 1])
        # vložení nového potomka
        node.children.insert(i + 1, z)

        # vložení do pravého potomka
        z.keys = y.keys[t:(2 * t - 1)]
        # vložení do levého potomka
        y.keys = y.keys[0:(t - 1)]

        # rozdělení potomků
        if not y.leaf:
            z.children = y.children[t:(2 * t)]
            y.children = y.children[0:t] # t - 1

    def insert_key_nonfull(self, node, key):	
        i = 0
        while i < node.n() and key > node.keys[i]:
            i += 1

        if node.leaf:
            node.keys.insert(i, key)
        else:
            if node.children[i].n() == 2 * self.t - 1:
                self.split_child(node, i)
                if key > node.keys[i]:
                    i += 1

            self.insert_key_nonfull(node.children[i], key)

'''
                [50]
              /      \
      [15, 30]        [60] 
     /    |   \      /    \
[5, 10] [20] [40]  [55] [70, 90]
'''

Tree = BTree(2)
Tree.insert_key(20)
Tree.insert_key(50)
Tree.insert_key(30)
Tree.insert_key(60)
Tree.insert_key(40)
Tree.insert_key(55)
Tree.insert_key(70)
Tree.insert_key(90)
Tree.insert_key(10)
Tree.insert_key(15)
Tree.insert_key(5)

print(Tree.root)

k = 5
l = 80
result1 = Tree.search(k)
result2 = Tree.search(l)

def print_search(result, key):
    if result:
        return print(f'Prvek {key} nalezen - Node: {result[0]}, index: {result[1]}')
    else:
        return print(f'Prvek {key} nenalezen')

print_search(result1, k)
print_search(result2, l)


