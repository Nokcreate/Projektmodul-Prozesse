import cv2
import numpy as np
import serial

def string_to_list(string): 
    matress_position = []
    for i in string: 
        if i != '[' and i!=',' and i!= ' ' and i != ']': 
            i = int(i)
            matress_position.append(i)
    return matress_position
    
def drawposition(position_list, data): 
    # Bild laden
    cv2.destroyAllWindows()
    
    image = cv2.imread(r"C:\Users\elias\OneDrive\Dokumente\AA_Hochschule\INformatik und Design\SOSE\Projektmodul Prozese\bild.jpg")
    center_coordinates =[250,250] 

    for eintrag_nr in range(len(position_list)) : 
        
        if position_list[eintrag_nr] == 1: 
            # Koordinaten des Mittelpunkts des Kreises
            # Radius des Kreises
            radius = 100

            # Farbe des Kreises (BGR-Format)
            color = (0, 0, 255)

            # Dicke der Linie des Kreises (-1 für eine gefüllte Fläche)
            thickness = 2

            # Kreis zeichnen
            image_with_circle = cv2.circle(image, center_coordinates, radius, color, thickness)

            # Bild anzeigen
            cv2.imshow('Kreisbild', image_with_circle)
            cv2.waitKey(1) & 0xFF == ord(data[0]) 

        cv2.imshow('Kreisbild', image)
        cv2.waitKey(1) & 0xFF == ord(data[0])
        

        center_coordinates[1]+= 100
        
        if eintrag_nr // 2 > 0  : 
            center_coordinates[0]+=200 
        if eintrag_nr % 2 == 0 : 
            center_coordinates[1] = 250 
        

def portdecoder(): 
    # Serielle Verbindung herstellen
    ser = serial.Serial('COM11', 115200)  # COM11 durch den entsprechenden seriellen Port ersetzen
    ser.flushInput()

    while True:
        try:
            # Daten von der seriellen Verbindung lesen
            if ser.inWaiting() > 0:
                data = ser.readline().decode().strip()
                

                # Daten auf dem PC anzeigen
                print(data)
                
                position = string_to_list(data)
                drawposition(position, data)
                
        except KeyboardInterrupt:
            break

    # Serielle Verbindung schließen
    ser.close()
portdecoder() 