from flask import Flask, render_template, request
import joblib

app = Flask(__name__)

# Load the pre-trained RandomForestRegressor model
loaded_model = joblib.load('hpricer.pkl')

@app.route('/')
def home():
    return render_template('index.html')

@app.route('/predict', methods=['POST'])
def predict():
    if request.method == 'POST':
        # Assuming you have a form with input fields named 'area', 'bedrooms', etc.
        features = [float(request.form[x]) for x in X_train.columns]
        
        # Make prediction using the RandomForestRegressor model
        prediction = model_RFR.predict([features])

        return render_template('index.html', prediction_text=f'Predicted House Price: {prediction[0]:,.2f}')

if __name__ == '__main__':
    app.run(debug=True)