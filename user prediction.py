import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
import pickle


features = pd.read_csv(r'/home/alok/PycharmProjects/Covid19/new_user.csv')
# One-hot encode the data using pandas get_dummies
features = pd.get_dummies(features, drop_first=True)
# Use numpy to convert to arrays
# Labels are the values we want to predict
labels = np.array(features['Covid_condition_Yes'])
# labels = np.array(features.pop('label'))
# Remove the labels from the features
# axis 1 refers to the columns
features = features.drop('Covid_condition_Yes', axis=1)
# Saving feature names for later use
feature_list = list(features.columns)
# Convert to numpy array
features = np.array(features)

with open('Covid19', 'rb') as f:
    rf = pickle.load(f)


prediction = rf.predict(test_features)

if prediction == 0:
    print('Covid-19 condition: Yes')
else:
    print('Covid-19 condition :No')