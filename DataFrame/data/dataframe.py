from pathlib import Path
import csv
from .index import Index
from .series import Series


class DataFrame:
    """Serves as a table representation of data"""    
    def __init__(self, values: list, columns: Index=None):
        """Initializes and instance of DataFrame"""
        if not values:
            raise ValueError('Values cannot be empty')

        if columns is not None and len(columns.labels) != len(values):
            raise ValueError('Length of labels must be equal to the length of values')

        if columns is None:
            columns = Index(range(len(values)))

        self.values = values
        self.columns = columns

    def __len__(self):
        """Return the length of columns"""
        return len(self.columns)

    def __str__(self):
        """Returns a printable representation of the object"""
        return self.__repr__()

    def __repr__(self):
        """Returns a printable representation of the object"""
        return f'DataFrame({len(self.values[0].values)}, {len(self.columns)})'

    def __iter__(self):
        """Returns a generator for iteration"""
        yield from self.columns

    @classmethod
    def from_csv(cls, csv_file: Path, separator: str = ','):
        """Returns a new instance of the class"""
        values = []
        labels = []
        result = []

        with open(csv_file, 'r') as file:
            csv_reader = csv.reader(file, delimiter=separator)
            rows = [row for row in csv_reader]
        
        cols = [label for row in rows[:1] for label in row[1:]]
        raw_result = [row for row in rows[1:]]

        for i in range(len(raw_result)):
            value = []
            for j in range(len(raw_result[i])):
                value.append(raw_result[j][i])

            if i == 0:
                labels = value
            else:
                values.append(value)
        
        for value in values:
            result.append(Series(value, Index(labels)))
        
        return cls(result, Index(cols))

    @property
    def index(self):
        """Returns first instance of Series in values"""
        return self.values[0].index

    @property
    def shape(self):
        """Returns dimensions of the object"""
        return (len(self.values[0].values), len(self.columns))

    def get(self, key):
        """Returns the column of the given key. If doesn't exist, returns None"""
        try:
            index = self.columns.get_loc(key)
            return self.values[index]
        except KeyError:
            return None

    def items(self):
        """Returns a zip iterator, replicates function items() from dictionary"""
        return zip(self, self.values)
