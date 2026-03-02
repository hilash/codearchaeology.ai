from sklearn.metrics import confusion_matrix
from sklearn import tree
import pandas as pd
import csv


def BALANCED():
    df = pd.read_csv('train.csv')
    num_of_positive_samples = df['Outcome'].value_counts()[1]  # sick
    counter = 0
    with open('train.csv', 'r') as inp, open('train_balanced.csv', 'w') as out:
        writer = csv.writer(out)
        for row in csv.reader(inp):
            if row[8] == "0" and counter < num_of_positive_samples:  # healthy, negative
                writer.writerow(row)
                counter += 1
            elif row[8] != "0":
                writer.writerow(row)
    df2 = pd.read_csv('train_balanced.csv')
    X_train = df2.drop('Outcome', axis=1)
    y_train = df2['Outcome']
    model = tree.DecisionTreeClassifier(random_state=0)
    model.fit(X_train, y_train)
    df3 = pd.read_csv('test.csv')
    X_test = df3.drop('Outcome', axis=1)
    y_test = df3['Outcome']
    y_predict = model.predict(X_test)
    tn, fp, fn, tp = confusion_matrix(y_test, y_predict).ravel()
    print("[[{} {}]".format(tp, fp))
    print("[{} {}]]".format(fn, tn))


if __name__ == '__main__':
    BALANCED()
