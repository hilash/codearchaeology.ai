from sklearn.metrics import confusion_matrix
from sklearn import tree
import pandas as pd


def DT1():
    df = pd.read_csv('train.csv')
    X_train = df.drop('Outcome', axis=1)
    y_train = df['Outcome']
    model = tree.DecisionTreeClassifier(random_state=0)
    model.fit(X_train, y_train)
    df2 = pd.read_csv('test.csv')
    X_test = df.drop('Outcome', axis=1)
    y_test = df['Outcome']
    y_predict = model.predict(X_test)
    tn, fp, fn, tp = confusion_matrix(y_test, y_predict).ravel()
    print("[[{} {}]".format(tp, fp))
    print("[{} {}]]".format(fn, tn))


if __name__ == '__main__':
    DT1()
