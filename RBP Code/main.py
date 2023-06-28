import time
import digitalio
import board

# assign GPIO pins to variables
left_shoulder = board.GP14
left_middle = board.GP15
left_bottom = board.GP13

right_shoulder = board.GP12 
right_middle = board.GP11

right_bottom = board.GP10



# Specify IO, press and pull
right_bottom_button = digitalio.DigitalInOut(right_bottom)
right_bottom_button.direction = digitalio.Direction.INPUT
right_bottom_button.pull = digitalio.Pull.DOWN





right_middle_button = digitalio.DigitalInOut(right_middle)
right_middle_button.direction = digitalio.Direction.INPUT
right_middle_button.pull = digitalio.Pull.DOWN





right_shoulder_button = digitalio.DigitalInOut(right_shoulder)
right_shoulder_button.direction = digitalio.Direction.INPUT
right_shoulder_button.pull = digitalio.Pull.DOWN


left_bottom_button = digitalio.DigitalInOut(left_bottom)
left_bottom_button.direction = digitalio.Direction.INPUT
left_bottom_button.pull = digitalio.Pull.DOWN


left_shoulder_button = digitalio.DigitalInOut(left_shoulder)
left_shoulder_button.direction = digitalio.Direction.INPUT
left_shoulder_button.pull = digitalio.Pull.DOWN

left_middle_button = digitalio.DigitalInOut(left_middle)
left_middle_button.direction = digitalio.Direction.INPUT
left_middle_button.pull = digitalio.Pull.DOWN


# Define file path and list


# assign buttons to keys
position_inputs = (left_shoulder_button, left_middle_button, left_bottom_button, right_shoulder_button, right_middle_button, right_bottom_button)
vergleich = (0, 0, 0 , 0, 0, 0)

# Create file and write list entri
while True:
    vektor = []
    for entry in position_inputs:
        if entry.value:
            vektor.append(1)
        if entry.value == False:
            vektor.append(0)
    if vergleich != vektor:
        print(vektor)
        vektor = tuple(vektor)

    vergleich = list(vektor)
    time.sleep(0.2)

