class Node:
    def __init__(self, key, prev, next):
        self.key = key
        self.prev = prev
        self.next = next

    def __repr__(self):
        return f'Prev: {self.prev.key}, Key: {self.key}, Next: {self.next.key}'

class LinkedList:
    def __init__(self):
        self.sentinel = Node(None, None, None)
        self.sentinel.prev = self.sentinel
        self.sentinel.next = self.sentinel

    def print_ll(self):
        if self.sentinel.next is not self.sentinel:
            current = self.sentinel.next
            n = 0
            while True:
                n += 1
                print(f'{n}: {current}')
                if current.next is self.sentinel:
                    return None
                else:
                    current = current.next

    def search(self, key):
        if self.sentinel.next is not self.sentinel:
            current = self.sentinel.next
            while True:
                if current.key == key:
                    return current
                elif current.next is self.sentinel:
                    return None
                else:
                    current = current.next

    def insert(self, key):
        new_list = Node(key, self.sentinel, self.sentinel.next)
        self.sentinel.next.prev = new_list
        self.sentinel.next = new_list

    def delete(self, key):
        list_to_delete = self.search(key)
        list_to_delete.next.prev = list_to_delete.prev
        list_to_delete.prev.next = list_to_delete.next


Linked_list = LinkedList()
Linked_list.insert(1)
Linked_list.insert(2)
Linked_list.insert(3)
Linked_list.insert(4)
Linked_list.insert(5)

print(f'Sentinel: {Linked_list.sentinel}')
Linked_list.print_ll()
print()
print(Linked_list.search(2))
Linked_list.delete(2)
print(Linked_list.search(2))

