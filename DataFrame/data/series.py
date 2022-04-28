from statistics import mean
from pathlib import Path
import operator
import csv
from .index import Index


class Series:
    """Stores a sequence of values indexed by Index class"""
    def __init__(self, values: list, index: Index=None):
        """Initializes and instance of Series"""
        if not values:
            raise ValueError('Values cannot be empty')

        if index is not None and len(index.labels) != len(values):
            raise ValueError('Length of labels must be equal to the length of values')

        if index is None:
            index = Index(range(len(values)))

        self.values = values
        self.index = index
            
    def __len__(self):
        """Return the length of values"""
        return len(self.values)

    def __str__(self):
        """Returns a printable representation of the object"""
        return self.__repr__()

    def __repr__(self):
        """Returns a printable representation of the object"""
        result = []
        for i, value in enumerate(self.values):
            result.append(f'{self.index.labels[i]}\t{value}')

        return '\n'.join(result)

    def __iter__(self):
        """Returns a generator for iteration"""
        yield from self.values

    def __getitem__(self, key):
        """Return a value on the given key"""
        index = self.index.get_loc(key)
        return self.values[index]

    def __add__(self, other):
        """Returns a new instance where on every element of both instances is applied operator add""" 
        return self._apply_operator(other, operator.add)
    
    def __sub__(self, other):
        """Returns a new instance where on every element of both instances is applied operator sub"""
        return self._apply_operator(other, operator.sub)

    def __mul__(self, other):
        """Returns a new instance where on every element of both instances is applied operator multiply"""
        return self._apply_operator(other, operator.mul)

    def __truediv__(self, other):
        """Returns a new instance where on every element of both instances is applied operator divison"""
        return self._apply_operator(other, operator.truediv)

    def __floordiv__(self, other):
        """Returns a new instance where on every element of both instances is applied operator true divison"""
        return self._apply_operator(other, operator.floordiv)

    def __mod__(self, other):
        """Returns a new instance where on every element of both instances is applied operator modulo"""
        return self._apply_operator(other, operator.mod)

    def __pow__(self, other):
        """Returns a new instance where on every element of both instances is applied operator **"""
        return self._apply_operator(other, operator.pow)

    def __round__(self, precision):
        """Returns a new instance where on every element is applied function round"""
        new_values = [round(value, precision) for value in self.values]
        return Series(new_values, self.index)
        
    @classmethod
    def from_csv(cls, csv_file: Path, separator: str = ','):
        """Returns a new instance of the class"""
        with open(csv_file, 'r') as file:
            csv_reader = csv.reader(file, delimiter=separator)
            columns = [row for row in csv_reader]
            labels, values = columns

        return cls(values, Index(labels))
        
    @property
    def shape(self):
        """Returns dimensions of the object"""
        return (len(self.values), )

    def get(self, key):
        """Returns a value on the given key. If doesn't exist, returns None"""
        try:
            return self[key]
        except KeyError:
            return None
            
    def items(self):
        """Returns a zip iterator, replicates function items() from dictionary"""
        return zip(self.index, self.values)

    def sum(self):
        """Returns the sum of the values"""
        return sum(self.values)

    def max(self):
        """Returns the maximum value in values"""
        return max(self.values)

    def min(self):
        """Returns the minimum value in values"""
        return min(self.values)

    def mean(self):
        """Returns the arithmetic mean of the values"""
        return mean(self.values)

    def apply(self, func):
        """Applies the given function on every value in values. Returns a new Series object"""
        new_values = [func(value) for value in self.values]
        return Series(new_values, self.index)

    def _apply_operator(self, other, operator):
        """Returns new instance of Series applying operator to every element of both instances"""
        if not isinstance(other, Series):
            return NotImplemented

        if len(self) != len(other):
            raise ValueError('Objects must be of same length')

        new_values = []
        for own, others in zip(self.values, other.values):
            new_values.append(operator(own, others))

        return Series(new_values, self.index)

    def abs(self):
        """Returns a new Series object with absolute values"""
        return self.apply(abs)
