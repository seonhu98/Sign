import cv2
import mediapipe as mp
import math

cap = cv2.VideoCapture(1)

mpHands = mp.solutions.hands
my_hands = mpHands.Hands()
mpDraw = mp.solutions.drawing_utils

def dist(x1, y1, x2, y2):
    return math.sqrt(math.pow(x1 - x2, 2)) + math.sqrt(math.pow(y1 - y2, 2))

compareIndex =[[18,4], [6,8], [10,12], [14,16], [18,20]]
open = [False,False,False,False,False]
gesture = [[False, False, False, False, False, "0"],
           [False, True, False, False, False, "1"],
           [False, True, True, False, False, "2"],
           [True, True, True, False, False, "3"],
           [False, True, True, True, True, "4"],
           [True, True, True, True, True, "5"],
           [False, True, True, True, False, "6"],
           [False, True, True, False, True, "7"],
           [False, True, False, True, True, "8"],
           [False, False, True, True, True, "9"]]

while True:
    success, img = cap.read()
    h,w,c = img.shape
    imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    results = my_hands.process(imgRGB)
    if results.multi_hand_landmarks:
        for handLms in results.multi_hand_landmarks:
            for i in range(0,5):
                open[i] = dist(handLms.landmark[0].x, handLms.landmark[0].y,
                          handLms.landmark[compareIndex[i][0]].x,handLms.landmark[compareIndex[i][0]].y) < \
                          dist(handLms.landmark[0].x, handLms.landmark[0].y,
                          handLms.landmark[compareIndex[i][1]].x, handLms.landmark[compareIndex[i][1]].y)
            print(open)
            text_x = (handLms.landmark[0].x * w)
            text_y = (handLms.landmark[0].y * h)
            for i in range(0,len(gesture)):
                flag = True
                for j in range(0,5):
                    if(gesture[i][j] != open[j]):
                        flag = False
                if(flag == True):
                    cv2.putText(img, gesture[i][5],(round(text_x) - 50,round(text_y) -250),
                                cv2.FONT_HERSHEY_PLAIN,4,(0,0,0), 4)
            mpDraw.draw_landmarks(img,handLms,mpHands.HAND_CONNECTIONS)

    cv2.imshow("HandTracking",img)
    cv2.waitKey(1)

