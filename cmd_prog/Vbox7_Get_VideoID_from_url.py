# this program is write on Python 2
import re
import sys
import time
import urllib
import urllib2
import json

api4 = 'http://api.vbox7.com/v4/?action='
token4 = '&app_token=imperia_android_0.1.0_3rG7jk'

url = str(raw_input("Enter URL:"))
url = url.split(":", 2)
url = str(url[2])
url = url.split("?", 1)
url = str(url[0])


print ("Data type of \'id\' = " + str(type(url)))
print ("ID of vidio = " + url)
