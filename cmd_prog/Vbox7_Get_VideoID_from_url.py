#! /usr/bin/env python

try:
    url = str(raw_input("Enter Vbox7 - URL:"))
except:
    url = str(input("Enter Vbox7 - URL:"))
url = url.split(":", 2)
url = str(url[2])
url = url.split("?", 1)
url = str(url[0])

try:
    if url[len(url)-1] == '#':
        url = url[:len(url)-1]
except:
    print("Please input URL: ")

print ("Data type of \'id\' = " + str(type(url)))
print ("ID of vidio = " + url)
