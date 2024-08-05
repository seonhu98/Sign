import cv2

for i in range(4701,4833):
    i=str(i)
    src = cv2.imread("MYBOX/IMG_"+i+".jpg", cv2.IMREAD_COLOR)

    dst = cv2.resize(src, dsize=(400, 400), interpolation=cv2.INTER_AREA)
    dst2 = cv2.resize(src, dsize=(0, 0), fx=0.3, fy=0.7, interpolation=cv2.INTER_LINEAR)
    cv2.imwrite