import random

def createSeminarCode (a =None, b = None, c= None):
    if a == None:
        a = random.randint (0, 9)
        b = random.randint (0, 9)
        c = random.randint (0, 9)

    d = (a + b) % 10
    e = (a * b) % 10
    f = (5 + a - b) % 10

    code = a + d + b + e + c + f
    return code

for i in range (36):
    print (createSeminarCode ())