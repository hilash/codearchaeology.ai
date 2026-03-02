import SocketServer
from pymouse import PyMouse


# parse the data from the android app and to calculate the new cordinates
def dataParser(data):
	m=PyMouse()
	currentPos = m.position()
	dataList = data.split(',')
	parsedData={}
	if dataList[0]=="click":
		parsedData['type']='click'
		parsedData['x']=currentPos[0]
		parsedData['y']=currentPos[1]
	else:
		parsedData['type']='move'
		parsedData['x']=currentPos[0]+0.5*long(dataList[1]) #time^2?
		parsedData['y']=currentPos[1]+0.5*long(dataList[2]) # time^2?
	return parsedData
	
	

# execute the changes requested by the application on the mouse
def applyAction(parsedData):
	m=PyMouse()
	if parsedData['type']=='move':			
		m.move(parsedData['x'],parsedData['y'])
	elif self.data=='press':
		if parsedData['button']=='left':
			m.click(parsedData['x'],parsedData['y'],1)
		elif parsedData['button']=='middle':
			m.click(parsedData['x'],parsedData['y'],2)
		else:
			m.click(parsedData['x'],parsedData['y'],3)


class MyUDPHandler(SocketServer.BaseRequestHandler):
	"""
	This class works similar to the TCP handler class, except that
	self.request consists of a pair of data and client socket, and since
	there is no connection the client address must be given explicitly
	when sending data back via sendto().
	"""
		
	old_data_x = []
	old_data_y = []

	def handle(self):
		data = self.request[0].strip()
		print data
		#socket = self.request[1]
		Ax,Ay,Az = eval(data)
		if Ax < 0.7 or Ay < 0.1:
			return
		self.old_data_x.append(Ax)
		self.old_data_y.append(Ay)
		m=PyMouse()
		currentPos = m.position()
		m.move(int(currentPos[0]+Ax),int(currentPos[1]+Ay))
		print 'max x: %d, max y: %d', max(map(abs, self.old_data_x)), max(map(abs, self.old_data_y))

		#applyAction(dataParser(data))
		# just send back the same data, but upper-cased
		#socket.sendto(data.upper(), self.client_address)

if __name__ == "__main__":
	HOST, PORT = "132.68.34.255", 9999
	server = SocketServer.UDPServer((HOST, PORT), MyUDPHandler)
	server.serve_forever()