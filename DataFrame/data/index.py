class Index:
    """Servers as an indexation for a sequence of values"""
    def __init__(self, labels: list, name: str=''):
        """Initializes and instance of Index"""
        if not labels:
            raise ValueError('Labels cannot be empty')
        
        if len(labels) != len(set(labels)):
            raise ValueError('Duplicate label')

        self.labels = list(labels)
        self.name = name

    def __len__(self):
        """Return the length of labels"""
        return len(self.labels)

    def __iter__(self):
        """Returns a generator for iteration"""
        yield from self.labels

    def get_loc(self, key):
        """Returns the position of the given key in labels. If doesn't exist, returns None"""
        try:
            return self.labels.index(key)
        except ValueError:
            raise KeyError('Invalid key')