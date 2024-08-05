import cv2
import numpy as np
from tensorflow import keras

#학습
model = keras.models.load_model('jgb-model5.keras')
cap = cv2.VideoCapture(0)
#scr = cv2.imread("12.jpg",cv2.COLOR_BGR2RGB)

while(True):
    ret, frame =cap.read()
    frame=frame[0:479,0:479]
    test=cv2.resize(frame,(150,150))
    cv2.imshow('input',test)
    test=cv2.cvtColor(test,cv2.COLOR_BGR2RGB)
    test=test[np.newaxis,:,:,:]
    predict=model(test)
    print(str(predict))
    predict_number=np.argmax(predict)
    print(predict_number)
    predict_number = str(predict_number)
    cv2.putText(frame, predict_number, (150, 150), cv2.QT_FONT_NORMAL, 1.4, (0, 0, 0), 2)
    cv2.imshow('cam', frame)
    if cv2.waitKey(1) == ord('q'):
        break
cap.release()
cv2.destroyAllWindows()