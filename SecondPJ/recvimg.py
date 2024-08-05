import socket
import cv2

sock = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
svrIP,port='10.10.21.120', 12345

sock.connect((svrIP, port))
print("연결 시도")
def recvImg(self):
    while True:
        try:
            len = self.recvall(self,conn,64)
            len1 = len.decode('utf-8')
            strdata = self.recvall(self,conn,int(len1))
            stime = self.recvall(self.conn, 64)
            print('send time: '+stime.decode('utf-8'))
            now=time.localtime()
            print('receivw time: '+dateime.utcnow().strftime('%Y-%m-%d %H:%M:%S.%f'))
            data = numpy.frombuffer(base64.b64decode(stringData),numpy.uint8)
            decimg=cv2.imdecode(data,1)
            cv2.imshow('image',dscimg)
            cv2.waitKey(1)
            # msg = sock.recv(2000) #맞출 것
            # if not msg:
            #     print("이미지 수신")
            #     break
        except:
            print("종료")
            break
def recvall(self,sock,count):
    buf = b''
    while count:
        newbuf = sock.recv(count)
        if not newbuf: return  None
        buf += newbuf
        count -= len(newbuf)
    return  buf

sock.close()


        