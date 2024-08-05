import socket

sock = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
address = ('10.10.21.120', 12345)
msg = "message"
sock.bind(address)
sock.listen(5)

while True:
    client, addr = sock.accept()
    print("연결성공")
    client.send(msg.encode()) #데이터 출력
    client.close()