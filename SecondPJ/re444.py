import os
import cv2
import numpy as np
import tensorflow as tf

file_path3= 'dataaz'


for name in os.listdir(file_path3):
    src = os.path.join(file_path3, name)
    for realname in os.listdir(src):
        aaa = os.path.join(src, realname)
        img = cv2.imread(aaa, cv2.IMREAD_COLOR)
        reimg = cv2.resize(img, (150, 150), interpolation=cv2.INTER_LINEAR)
        cv2.imwrite(aaa, reimg)